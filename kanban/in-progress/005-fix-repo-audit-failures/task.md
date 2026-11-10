# Fix repo audit failures

## Description

`ganda repo audit` on master reported 7 failures (5 blocking errors, 2 warnings).
Clear all of them so the repo attests cleanly.

## Checklist

- [x] Run `ganda repo audit --fix` (analyzer-suppressions, nuru, timewarp-signals-gitignore, memsearch-scaffold)
- [x] workflow-pwsh-shell: convert the bash steps in `.github/workflows/workflow.yml` to pwsh
- [x] source-file-line-cap: split `tools/dev-cli/endpoints/workflow-command.cs` (678 lines) by responsibility
- [x] ci-code-coverage: collect cobertura coverage in `dev test` and upload it from CI
- [x] Port dev-CLI handlers to the TimeWarp.Nuru beta.12 / Mediator handler shape (the `--fix` bump broke the build)
- [x] `dev build`, `dev test` (22/22) and `ganda repo audit` (43 passed, 0 failed) in the task worktree
- [ ] Open PR and confirm CI is green

## Session

- Created: 1f6a6c8d-4b41-41b4-9d61-a6a74e75bf5f (2026-11-10)
- Implementation: 1f6a6c8d-4b41-41b4-9d61-a6a74e75bf5f (2026-11-11)

## Results

- `--fix` seeded `.timewarp/suppression-baseline.json`, refreshed the five `.githooks/*.cs` hooks, added the
  `/.timewarp/signals/` ignore, and bumped TimeWarp.Nuru/Nuru.DevCli to 4.0.0-beta.12, TimeWarp.Amuru/Amuru.Tools
  to 3.0.0-beta.2 and TimeWarp.Terminal to 2.0.0-beta.1.
- Those bumps removed `ICommandHandler<,>`; all five dev-CLI handlers now implement
  `IRequestHandler<TCommand, Unit>` returning `ValueTask<Unit>`, the shape shown in the Nuru beta.12 readme.
- `workflow-command.cs` is split into `workflow-command.cs` (143 lines, merge pipeline),
  `workflow-release-command.cs` (275, release orchestration) and `workflow-release-steps.cs` (292, release gate
  steps) as partial classes. Method bodies moved verbatim.
- `dev test` now calls `dotnet test` through `Shell.Builder` with the MTP coverage flags, because Amuru's
  `DotNet.Test()` builder has no pass-through for them. Added `Microsoft.Testing.Extensions.CodeCoverage` 18.12.0.
  CI uploads `artifacts/coverage` as the `coverage` artifact.

## Notes

- The pwsh `Run CI Pipeline` step ends with `exit $LASTEXITCODE` so a failing pipeline still fails the job.
- The release and break-glass paths of the workflow were not exercised locally; only `--help`, `build` and `test` ran.
