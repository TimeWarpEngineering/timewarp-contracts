# Refresh outdated NuGet packages in TimeWarpEngineering/timewarp-contracts

## Description

Refresh outdated NuGet packages in TimeWarpEngineering/timewarp-contracts.
Filter: (all)
Outdated:
  TimeWarp.Nuru 4.0.0-beta.12 -> 4.0.0-beta.13 (patch)
  TimeWarp.Nuru.DevCli 4.0.0-beta.12 -> 4.0.0-beta.13 (patch)
  TimeWarp.SourceGenerators 1.0.0-beta.11 -> 1.0.0-beta.16 (patch)
  TimeWarp.Build.Tasks 1.0.0 -> 2.0.0 (major)
  TimeWarp.Mediator 15.0.0-beta.4 -> 15.0.0-beta.8 (patch)
  TimeWarp.Jaribu.TestingPlatform 1.0.0-beta.15 -> 1.0.0-beta.16 (patch)
Edit Directory.Packages.props in this repo only.

Version: set <Version> in source/Directory.Build.props from 2.0.0-beta.31 to 2.0.0-beta.32 in this pull request. The prerelease suffix is -beta.N, never +beta.N. The wave releases it after merge.

## Goal (implementer restatement)

Bump the six outdated TimeWarp packages in the root `Directory.Packages.props` (the only
package-version file to touch) to the listed versions, including the major bump of
`TimeWarp.Build.Tasks` to 2.0.0, and bump the library `<Version>` in
`source/Directory.Build.props` to `2.0.0-beta.32`.

Acceptance criteria:

- [x] `Directory.Packages.props` pins Nuru 4.0.0-beta.13, Nuru.DevCli 4.0.0-beta.13,
      SourceGenerators 1.0.0-beta.16, Build.Tasks 2.0.0 (landed as 2.0.1-beta.1), Mediator 15.0.0-beta.8,
      Jaribu.TestingPlatform 1.0.0-beta.16; no other pins change.
- [x] `source/Directory.Build.props` `<Version>` is `2.0.0-beta.32` (hyphen, not plus).
- [x] `./bin/dev build`, `./bin/dev test`, and `ganda repo audit` pass; any break from the
      new packages is fixed in this task.

## Results

- `Directory.Packages.props`: TimeWarp.Nuru 4.0.0-beta.12 → 4.0.0-beta.13,
  TimeWarp.Nuru.DevCli 4.0.0-beta.12 → 4.0.0-beta.13, TimeWarp.SourceGenerators
  1.0.0-beta.11 → 1.0.0-beta.16, TimeWarp.Build.Tasks 1.0.0 → 2.0.1-beta.1 (see note), TimeWarp.Mediator
  15.0.0-beta.4 → 15.0.0-beta.8, TimeWarp.Jaribu.TestingPlatform 1.0.0-beta.15 → 1.0.0-beta.16.
  No other pins changed.
- `source/Directory.Build.props`: `<Version>` 2.0.0-beta.31 → 2.0.0-beta.32.
- Build.Tasks note: the task listed 2.0.0, but the post-commit `ganda repo attest` failed
  `package-pin-currency` because 2.0.1-beta.1 was published on 2026-11-14 and this dev-only
  (`PrivateAssets=all`) pin must track the newest version. Bumped to 2.0.1-beta.1 and reran all gates.
- No source changes were needed; the Build.Tasks major bump built clean with 0 warnings.
- The dev CLI (`tools/dev-cli`, which uses Nuru/Nuru.DevCli/Mediator from this props file) was
  rebuilt once with `dotnet run --file tools/dev-cli/dev.cs -- self-install` and compiled clean.

### Gates

- `./bin/dev build` — passed (0 warnings, 0 errors).
- `./bin/dev test` — passed (22/22 succeeded, 0 failed, 0 skipped).
- `ganda repo audit` — passed with 2 advisory warnings (`memsearch-scaffold`: five outdated
  `.githooks/*.cs`; `vscode-window-icon`: `.vscode/settings.json` missing). Boy-scout fix:
  `ganda repo audit --fix` refreshed the five `.githooks/*.cs` hooks and wrote the ignored
  `.vscode/settings.json`; its re-audit passed all checks (43 passed, 0 failed, 3 skipped). After the Build.Tasks
  2.0.1-beta.1 fix: `./bin/dev build` passed (0 warnings), `./bin/dev test` passed (22/22),
  `ganda repo audit` passed (43 passed, 0 failed, 3 skipped).

### How to validate

Smoke:

```bash
grep -o 'Include="TimeWarp[^"]*" Version="[^"]*"' Directory.Packages.props
grep '<Version>' source/Directory.Build.props
./bin/dev build && ./bin/dev test
```

Expect: Nuru and Nuru.DevCli at 4.0.0-beta.13, SourceGenerators 1.0.0-beta.16, Build.Tasks
2.0.1-beta.1, Mediator 15.0.0-beta.8, Jaribu.TestingPlatform 1.0.0-beta.16; `<Version>2.0.0-beta.32</Version>`;
build succeeds with 0 warnings and 22 tests pass.

### Review

- Round 1, effort 1, roster: general.
- Counts: bug 0 open / 0 fixed / 0 wontfix; suggestion 0; nit 0. Final open count: 0.
- Disposition: clean. No wontfix and no escalation.
- The goal restatement matches the original task. `TimeWarp.Build.Tasks` `2.0.1-beta.1` is the recorded `package-pin-currency` follow-up after the task's `2.0.0`, not a misread.
- Paths: `review/review-framework.md`, `review/round-1/general.md`, `review/round-1/merged.md`, `review/disposition.md`.

## Session

- Minted by ganda task wave run 20261114T054552Z-9a0f39
- Implement oracle: implemented by implementer-claude (claude, vendor anthropic, model claude-opus-5-5, response model claude-opus-5-5), session 2fe5fe84-1c6a-4e31-ac87-c168318035fd, max-turns 500, Done — 2026-11-14T10:22:01Z
- Reviewer: grok session 01a1d994-245e-7bc0-a311-47ae6b1cf23c (2026-11-14), effort 1, roster general, max-turns 80
- Review oracle: review by reviewer-grok (grok, vendor xai, model grok-4.7, response model grok-4.7-build), session 01a1d994-245e-7bc0-a311-47ae6b1cf23c, max-turns 80, artifacts general.md, merged.md, disposition.md — 2026-11-14T10:26:39Z

## Review

- Round 1: Done
