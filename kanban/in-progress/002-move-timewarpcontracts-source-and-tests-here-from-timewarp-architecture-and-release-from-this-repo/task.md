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

## Goal (implementer restatement)

TimeWarp.Contracts has to ship from this repository, on its own prerelease line, so Nuru and Roslynk stop taking a package that is built inside the architecture template.

This walk is done when:

- The architecture tree `source/foundation/timewarp-contracts/` is copied to `source/timewarp-contracts/` with the same namespaces, `PackageId`, `RootNamespace`, Purpose/Design regions, and public API. The commit message names the architecture commit the files came from.
- The project file is this repo's baseline: `net10.0` and `net11.0`, `IsAotCompatible`, and only `TimeWarp.Mediator.Contracts` plus `OneOf`, pinned at the versions architecture uses at copy time. The convention-analyzer `ProjectReference` and the rest of architecture's foundation props stay behind.
- Jaribu tests that touch only `TimeWarp.Contracts` types move to `tests/timewarp-contracts-tests/` and pass. Anything that needs `TimeWarp.Foundation.*` stays in architecture, including `shared-problem-details-forward-tests.cs`.
- `source/Directory.Build.props` carries a prerelease above the highest `TimeWarp.Contracts` version on nuget.org. That highest version is `2.0.0-beta.29`, so the line here is `2.0.0-beta.30`.
- The packed nupkg contains `lib/net10.0` and `lib/net11.0`.
- The repo root readme describes the package.
- `dev build`, the tests, and `ganda repo audit` are clean.
- Pull-request and merge CI pack the library, and release mode promotes that artifact to nuget.org through the OIDC login already in `workflow.yml`. Cutting the release stays the human `tw-release` step after merge. The nuget.org trusted-publishing policy for this repo is probed; creating it needs the TimeWarp.Enterprises account on nuget.org when the probe says it is missing.
- timewarp-architecture is not changed from this task.

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

- [x] Source copied to `source/timewarp-contracts/`; source SHA in commit message
- [x] csproj trimmed to this repo's baseline; net10.0 and net11.0 build
- [x] Contracts-only tests moved to `tests/timewarp-contracts-tests/` and passing
- [x] Version set above the highest published `TimeWarp.Contracts`
- [x] Packed nupkg has `lib/net10.0` and `lib/net11.0` (check with `unzip -l`)
- [x] Root readme written
- [x] `dev build`, tests, `ganda repo audit` green
- [ ] After merge: released; nuget.org URL in Results; timewarp-architecture task told the version

## Notes

- Do not change timewarp-architecture from this task. Its removal and package switch is its own
  task.
- Consumers outside this repo (Nuru, Roslynk) bump their pin when the version appears. Their
  agents do that, not this task.
- Analyzers in timewarp-architecture and Nuru match `[Operation]`, `[Destructive]`, and
  `[OpenWorld]` by metadata name, so the full type names must not change.

## Results

Copied `TimeWarp.Contracts` from timewarp-architecture `ef2da4f8f7c1108eb639634bce319ed590d03e9b` into `source/timewarp-contracts/`. The project file drops the convention-analyzer `ProjectReference`. Dependencies stay `TimeWarp.Mediator.Contracts` 14.0.0 and `OneOf` 3.0.271. Six Jaribu files that only use `TimeWarp.Contracts` types moved to `tests/timewarp-contracts-tests/`. Foundation tests, including `shared-problem-details-forward-tests.cs`, stayed in architecture.

Version is `2.0.0-beta.30`. nuget.org's highest published `TimeWarp.Contracts` is `2.0.0-beta.29`. The package is not on the org GitHub Packages feed.

`./bin/dev build` reported 0 warnings and 0 errors. `./bin/dev test` reported 22 passed. `dotnet run --file tools/dev-cli/dev.cs -- pack` wrote `artifacts/packages/TimeWarp.Contracts.2.0.0-beta.30.nupkg`. `unzip -l` shows `lib/net10.0/timewarp-contracts.dll` and `lib/net11.0/timewarp-contracts.dll`. `ganda repo audit` passed 34, failed 0, skipped 3.

Pull-request and merge CI now pack. Release mode promotes the `Packages-*` artifact and pushes to nuget.org with the OIDC key. Cutting the release stays the human `tw-release` step after merge.

Trusted publishing is missing for this repo. Probe run [41240948605](https://github.com/TimeWarpEngineering/timewarp-contracts/actions/runs/41240948605) failed at NuGet login: HTTP 401, no matching trust policy owned by `TimeWarp.Enterprises`. timewarp-architecture was not changed. After the package is on nuget.org, architecture task 459 needs the version `2.0.0-beta.30`.

### How to validate

Smoke: `./bin/dev build`, then `./bin/dev test`, then `dotnet run --file tools/dev-cli/dev.cs -- pack`, then `unzip -l artifacts/packages/TimeWarp.Contracts.2.0.0-beta.30.nupkg`.

Expect: the build line `0 Warning(s)` and `0 Error(s)`, the test summary `succeeded: 22`, and zip entries `lib/net10.0/timewarp-contracts.dll` and `lib/net11.0/timewarp-contracts.dll`.

## Session

- Created: 8c10267a-9869-468f-94f3-f663f4e20b06 (2026-10-31)
- Implementation: source copy, tests, version 2.0.0-beta.30, pack and promote workflow (2026-11-01). Trusted-publishing probe 41240948605 failed; policy is not something this walk can create.
