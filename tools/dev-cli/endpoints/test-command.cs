#region Purpose
// Run the test suite
#endregion
#region Design
// Executes dotnet test for all tests in the repository
// Handler stores Command and Ct as fields so private methods are zero-parameter
// Collects cobertura coverage to artifacts/coverage/coverage.cobertura.xml (Microsoft.Testing.Extensions.CodeCoverage)
// Streams test output via Amuru RunAsync by default; --quiet uses CaptureAsync
#endregion

namespace DevCli.Commands;

[NuruRoute("test", Description = "Run the test suite")]
public sealed class TestCommand : ICommand<Unit>
{
  [Option("quiet", "q", Description = "Hide test output unless the command fails")]
  public bool Quiet { get; set; }

  internal sealed class Handler : IRequestHandler<TestCommand, Unit>
  {
    private readonly ITerminal Terminal;
    private TestCommand Command = null!;
    private CancellationToken Ct;
    private string RepoRoot = null!;

    public Handler(ITerminal terminal)
    {
      Terminal = terminal;
    }

    public async ValueTask<Unit> Handle(TestCommand command, CancellationToken ct)
    {
      Command = command;
      Ct = ct;

      if (!FindRepoRoot()) return Unit.Value;
      if (!await TestAsync()) return Unit.Value;

      Terminal.WriteLine("\nTests completed successfully!".Green());
      return Unit.Value;
    }

    private bool FindRepoRoot()
    {
      string? root = Git.FindRoot();
      if (root is null)
      {
        Terminal.WriteErrorLine("Error: could not find repository root.");
        Environment.ExitCode = 1;
        return false;
      }
      RepoRoot = root;
      Terminal.WriteLine("Running test suite...");
      return true;
    }

    private async Task<bool> TestAsync()
    {
      // DotNet.Test() has no pass-through for MTP flags (--coverage), so use the raw shell builder.
      CommandResult command = Shell.Builder("dotnet")
        .WithArguments(
          "test",
          "--configuration", "Release",
          "--results-directory", "artifacts/coverage",
          "--coverage",
          "--coverage-output-format", "cobertura",
          "--coverage-output", "coverage.cobertura.xml")
        .WithWorkingDirectory(RepoRoot)
        .WithNoValidation()
        .Build();

      if (Command.Quiet)
      {
        CommandOutput result = await command.CaptureAsync(Ct);
        if (!result.Success)
        {
          Terminal.WriteErrorLine(result.Combined);
          Terminal.WriteErrorLine("Tests failed!".Red());
          Environment.ExitCode = 1;
          return false;
        }

        return true;
      }

      int exitCode = await command.RunAsync(Ct);
      if (exitCode != 0)
      {
        Terminal.WriteErrorLine("Tests failed!".Red());
        Environment.ExitCode = exitCode;
        return false;
      }

      return true;
    }
  }
}