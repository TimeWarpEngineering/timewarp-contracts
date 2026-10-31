# Move TimeWarp.Contracts source and tests here from timewarp-architecture and release from this repo

## Description

`TimeWarp.Contracts` (transport-neutral operation contracts for HTTP, CLI, and MCP) is built
today in timewarp-architecture at `source/foundation/timewarp-contracts/` and versions with that
repository. TimeWarp.Nuru (since v4.0.0-beta.1) and Roslynk consume it, so neither should depend
on a package built from the architecture template. timewarp-architecture task 384 recorded the
plan: start there, extract to its own repo before Nuru's first release that references it. That
trigger has passed.

This task brings the package source and its own tests into this repository, gives it an
independent version line, and releases it from here. timewarp-architecture removes its copy and
consumes the package in its own task ("consume TimeWarp.Contracts from timewarp-contracts"),
which waits for the release from this task.

## Requirements

- Source: copy every file in timewarp-architecture `source/foundation/timewarp-contracts/` to
  `source/timewarp-contracts/` here (csproj, `.cs` files, `readme.md`). Keep namespaces,
  `PackageId`, `RootNamespace`, Purpose/Design regions, and the public API unchanged. Record the
  timewarp-architecture commit SHA the copy was taken from in the commit message.
- Package metadata: `PackageId` `TimeWarp.Contracts`, `IsAotCompatible`, `TargetFrameworks`
  `net10.0;net11.0`. Dependencies stay `TimeWarp.Mediator.Contracts` and `OneOf` only, pinned in
  this repo's `Directory.Packages.props` at the versions timewarp-architecture uses at copy time.
- Drop architecture-only plumbing from the csproj: the convention-analyzers `ProjectReference`
  workaround (tasks 387 and 403), and anything inherited from architecture's
  `source/foundation/Directory.Build.props`. Use this repo's `ganda repo init` baseline. Do not
  add a dependency on timewarp-architecture or its analyzer packages.
- Tests: move the Jaribu tests that exercise only `TimeWarp.Contracts` types from
  timewarp-architecture `tests/foundation/foundation-contracts-tests/` to
  `tests/timewarp-contracts-tests/` here. Candidates: `operation-attribute-group-tests.cs`,
  `operation-attribute-name-tests.cs`, `operation-attribute-title-tests.cs`,
  `operation-catalog-group-tests.cs`, `operation-catalog-tool-hint-tests.cs`,
  `tool-hint-attribute-tests.cs`. Check each one; a test that needs `TimeWarp.Foundation.*` stays
  in architecture. `shared-problem-details-forward-tests.cs` stays in architecture.
- Version: an independent version line in this repo. The first release must be above the highest
  `TimeWarp.Contracts` version already published (2.0.0-beta.28 when this spec was written;
  check nuget.org at implement time) and stays prerelease.
- Release: the release path follows the TimeWarp CI pattern already scaffolded here
  (`.github/workflows/workflow.yml`, `dev release`, nuget.org via OIDC trusted publishing). Set up
  the nuget.org trusted-publishing policy for this repo if it is missing. Publishing is the human
  `tw-release` step after merge.
- `readme.md` (repo root) describes the package. Replace the placeholder text.
- `dev build` is 0 warnings and 0 errors. Tests pass. `ganda repo audit` is clean.

## Checklist

- [ ] Source copied to `source/timewarp-contracts/`; source SHA in commit message
- [ ] csproj trimmed to this repo's baseline; net10.0 and net11.0 build
- [ ] Contracts-only tests moved to `tests/timewarp-contracts-tests/` and passing
- [ ] Version set above the highest published `TimeWarp.Contracts`
- [ ] Packed nupkg has `lib/net10.0` and `lib/net11.0` (check with `unzip -l`)
- [ ] Root readme written
- [ ] `dev build`, tests, `ganda repo audit` green
- [ ] After merge: released; nuget.org URL in Results; timewarp-architecture task told the version

## Notes

- Do not change timewarp-architecture from this task. Its removal and package switch is its own
  task.
- Consumers outside this repo (Nuru, Roslynk) bump their pin when the version appears. Their
  agents do that, not this task.
- Analyzers in timewarp-architecture and Nuru match `[Operation]`, `[Destructive]`, and
  `[OpenWorld]` by metadata name, so the full type names must not change.

## Session

- Created: 8c10267a-9869-468f-94f3-f663f4e20b06 (2026-10-31)
