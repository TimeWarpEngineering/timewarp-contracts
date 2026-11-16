#region Purpose
// Mode-aware CI/CD pipeline for TimeWarp.Contracts.
#endregion
#region Design
// PR/merge: clean → build → test → pack. Pack writes the IsPackable set to
// artifacts/packages and verifies file names with CiRunPromotion, including
// lib/net11.0.
// Release: tag-gate → check-version → locate-run → download-artifact → verify → push.
// Release promotes the CI Packages-* artifact (Nuru 458 build-once/promote) — no rebuild.
// Push target is nuget.org via the OIDC key workflow.yml passes as --api-key.
// workflow_dispatch without --mode stays merge and does not publish.
// Handlers run in-process so CI needs no ./bin/dev. dev release remains the human
// tag and GitHub Release cut; this command is what Actions calls.
// The PR/merge path first calls the shared PullRequestBuildClassifier
// (TimeWarp.Nuru.DevCli, timewarp-nuru 595). On a pull request whose diff
// touches only declared non-build paths (kanban, documentation, ...) it prints
// the classifier's message and exits 0 without clean, build, test, or pack.
// Push, release, dispatch, and a local run always build. workflow.yml has no
// paths filter on pull_request, so every pull request reports CI. This repo's
// additions to the rule (assets/logo.png is the packed icon) live in
// .timewarp/dev.jsonc under pullRequestBuild; the rule is not copied here.
#endregion

namespace DevCli.Commands;

[NuruRoute("workflow", Description = "Execute full CI/CD pipeline (mode-aware)")]
public sealed partial class WorkflowCommand : ICommand<Unit>
{
  private const string NuGetOrgSource = "https://api.nuget.org/v3/index.json";

  [Option("mode", "m", Description = "CI mode: pr, merge, or release (auto-detected from GITHUB_EVENT_NAME)")]
  public string? Mode { get; set; }

  [Option("api-key", "k", Description = "NuGet API key for publishing (from OIDC Trusted Publishing)")]
  public string? ApiKey { get; set; }

  internal sealed partial class Handler : IRequestHandler<WorkflowCommand, Unit>
  {
    private readonly ITerminal Terminal;
    private readonly IRepoCleanService RepoCleanService;
    private readonly NuGetVersionService NuGetVersionService;
    private readonly IRepoConfigService RepoConfigService;
    private readonly IPackableProjectService PackableProjectService;
    private CancellationToken Ct;
    private string? ApiKey;

    public Handler
    (
      ITerminal terminal,
      IRepoCleanService repoCleanService,
      NuGetVersionService nuGetVersionService,
      IRepoConfigService repoConfigService,
      IPackableProjectService packableProjectService
    )
    {
      Terminal = terminal;
      RepoCleanService = repoCleanService;
      NuGetVersionService = nuGetVersionService;
      RepoConfigService = repoConfigService;
      PackableProjectService = packableProjectService;
    }

    public async ValueTask<Unit> Handle(WorkflowCommand command, CancellationToken ct)
    {
      Ct = ct;
      ApiKey = command.ApiKey;

      string? eventName = Environment.GetEnvironmentVariable("GITHUB_EVENT_NAME");
      CiMode mode;
      try
      {
        mode = CiModeDetector.DetermineMode(command.Mode, eventName);
      }
      catch (ArgumentException exception)
      {
        Terminal.WriteErrorLine(exception.Message);
        Environment.ExitCode = 1;
        return Unit.Value;
      }

      if (string.IsNullOrEmpty(command.Mode))
      {
        string displayEventName = eventName ?? "(not set)";
        Terminal.WriteLine($"Detected GITHUB_EVENT_NAME: {displayEventName} -> Mode: {mode}");
      }

      Terminal.WriteLine($"\nCI/CD Pipeline — Mode: {mode}\n");

      if (mode == CiMode.Release)
      {
        await RunReleaseAsync();
      }
      else
      {
        await RunPrAsync();
      }

      return Unit.Value;
    }

    private async Task RunPrAsync()
    {
      Environment.ExitCode = 0;

      string? repoRoot = Git.FindRoot();
      if (repoRoot is null)
      {
        AbortPipeline("could not find the git repository root");
        return;
      }

      PullRequestBuildDecision decision = await PullRequestBuildClassifier.DecideAsync(repoRoot, Ct);
      Terminal.WriteLine(decision.Message);
      if (!decision.RequiresBuild)
      {
        Terminal.WriteLine("\nPipeline SUCCEEDED (build and test skipped)".Green());
        return;
      }

      Terminal.WriteLine("Pipeline: clean → build → test → pack\n");

      if (!await RunStepAsync("Clean", new CleanCommand.Handler(Terminal, RepoCleanService).Handle(new CleanCommand(), Ct)))
      {
        return;
      }

      if (!await RunStepAsync("Build", new BuildCommand.Handler(Terminal).Handle(new BuildCommand(), Ct)))
      {
        return;
      }

      if (!await RunStepAsync("Test", new TestCommand.Handler(Terminal).Handle(new TestCommand(), Ct)))
      {
        return;
      }

      if (!await RunStepAsync("Pack", new PackCommand.Handler(Terminal, PackableProjectService).Handle(new PackCommand(), Ct)))
      {
        return;
      }

      Terminal.WriteLine("\nPipeline SUCCEEDED".Green());
    }

    private async Task<bool> RunStepAsync(string stepName, ValueTask<Unit> step)
    {
      await step;

      if (Environment.ExitCode != 0)
      {
        Terminal.WriteErrorLine($"\nPipeline FAILED — {stepName} failed".Red());
        return false;
      }

      return true;
    }

    private void AbortPipeline(string reason)
    {
      Terminal.WriteLine("");
      Terminal.WriteLine("===============================================================================");
      Terminal.WriteLine($"  Pipeline ABORTED — {reason}");
      Terminal.WriteLine("===============================================================================");
      Environment.ExitCode = 1;
    }
  }
}
