# Refresh outdated NuGet packages in TimeWarpEngineering/timewarp-contracts

## Description

Refresh outdated NuGet packages in TimeWarpEngineering/timewarp-contracts.
Filter: (all)
Outdated:
  TimeWarp.Nuru 4.0.0-beta.16 -> 4.0.0-beta.17 (dev-only: tools/dev-cli/Directory.Build.props)
  TimeWarp.Nuru.DevCli 4.0.0-beta.16 -> 4.0.0-beta.17 (dev-only: tools/dev-cli/Directory.Build.props)
Every outdated pin is dev-only. Bump them. Do not change <Version> in source/Directory.Build.props: the wave merges this task and does not release this repo.
Edit Directory.Packages.props in this repo only.

## Goal (implementer restatement)

Bump the two dev-only pins TimeWarp.Nuru and TimeWarp.Nuru.DevCli from 4.0.0-beta.16 to
4.0.0-beta.17 in the root Directory.Packages.props (they feed tools/dev-cli).

Acceptance criteria:
- Directory.Packages.props pins both packages at 4.0.0-beta.17; no other pin changes.
- <Version> in source/Directory.Build.props is unchanged (no release).
- `./bin/dev build` passes and `ganda repo audit` passes.

## Session

- Minted by ganda task wave run 20261126T021837Z-0cb631
- Implement oracle: implemented by implementer-claude (claude, vendor anthropic, model claude-opus-5-5, response model claude-opus-5-5), session f2aa4144-a4de-4a60-a879-85d709138c06, max-turns 500, Done — 2026-11-26T03:04:19Z
- Reviewer: grok session 01a215cf-c2ec-7501-8844-ecbe86968b98 (2026-11-26), effort 1, roster general, round 1, turn cap 80
- Review oracle: review by reviewer-grok (grok, vendor xai, model grok-4.7, response model grok-4.7), session 01a215cf-c2ec-7501-8844-ecbe86968b98, max-turns 80, artifacts general.md, merged.md — 2026-11-26
- Review oracle: review by reviewer-grok (grok, vendor xai, model grok-4.7, response model grok-4.7-build), session 01a215cf-c2ec-7501-8844-ecbe86968b98, max-turns 80, artifacts general.md, merged.md, disposition.md — 2026-11-26T03:08:24Z

## Results

- Directory.Packages.props: TimeWarp.Nuru and TimeWarp.Nuru.DevCli 4.0.0-beta.16 -> 4.0.0-beta.17
  (dev-only, consumed by tools/dev-cli). source/Directory.Build.props <Version> unchanged.
- Boy-scout audit fix: `dev-cli-telemetry` (Error) failed on the first audit; tools/dev-cli/dev.cs
  now calls `.UseTelemetry(options => options.ServiceName = "timewarp-contracts-dev")`.
  `vscode-window-icon` (Warning) fixed by `ganda repo audit --fix` (writes the gitignored
  .vscode/settings.json, so no tracked change).

### Gates

- `./bin/dev build` — passed, 0 warnings, 0 errors.
- `dotnet run --file tools/dev-cli/dev.cs -- self-install` — passed (dev CLI compiles against
  Nuru beta.17 and with UseTelemetry); `./bin/dev --version` runs.
- Test files: none added or edited; the change touches no production type under test (package
  pins and the dev CLI entry point only). CI runs the full suite.
- `ganda repo audit` — first run failed on dev-cli-telemetry (pre-existing); after the fix and
  `ganda repo audit --fix`: "Repository passes all audit checks."

### How to validate

Smoke: `grep -o 'TimeWarp.Nuru[.A-Za-z]*" Version="[^"]*"' Directory.Packages.props && dotnet run --file tools/dev-cli/dev.cs -- self-install && ./bin/dev --help && ganda repo audit`
Expect: both pins read 4.0.0-beta.17, the dev CLI installs and prints its help, and the audit
ends with "Repository passes all audit checks."

### Review

Rounds: 1. Roster: general. Effort: 1. Turn cap: 80.

Round 1 (`review/round-1/merged.md`) raised no findings. The goal restatement matches the original task (same two pins, same versions, `Directory.Packages.props` only for the pins, `<Version>` unchanged). No misread under `## Learnings`.

Final counts: bug 0 open, 0 fixed, 0 wontfix; suggestion 0; nit 0.

Disposition: clean (`review/disposition.md`). Framework: `review/review-framework.md`. Last ledger: `review/round-1/merged.md`.

## Review

- Round 1: Done
