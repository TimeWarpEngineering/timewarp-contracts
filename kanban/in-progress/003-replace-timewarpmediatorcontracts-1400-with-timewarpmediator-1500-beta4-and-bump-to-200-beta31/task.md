# Task 003: Replace TimeWarp.Mediator.Contracts 14.0.0 with TimeWarp.Mediator 15.0.0-beta.4 and bump to 2.0.0-beta.31

## Description

TimeWarp.Contracts 2.0.0-beta.30 depends on `TimeWarp.Mediator.Contracts` 14.0.0. Mediator 15
(15.0.0-beta.4 on nuget.org since 2026-10-31) publishes only `TimeWarp.Mediator` and
`TimeWarp.Mediator.Generators`; the contracts package stops at 14.0.0 and its types (`IRequest`,
`IQuery<T>`, `ICommand`, ...) now live in `TimeWarp.Mediator` under the same namespace. A host that
references both gets type collisions, which is why timewarp-nuru task 481 held Mediator at 14.0.0
with the note "TimeWarp.Contracts (2.0.0-beta.29) still depends on TimeWarp.Mediator.Contracts
14.0.0". This task moves Contracts to Mediator 15 so Nuru (and every other consumer) can follow.

Contracts' own code uses no Mediator type; the reference exists so consumers get the marker
interfaces (`IQuery<T>`, `ICommand`, `ICommand<T>`, `IIdempotentCommand`, `IIdempotentCommand<T>`)
transitively, and the comments in `operation-kind.cs` already name `TimeWarp.Mediator.IQuery`1`.
Steven's direction (2026-10-31): TimeWarp is the primary consumer, no compatibility with 14 held.

## Goal (implementer restatement)

TimeWarp.Contracts 2.0.0-beta.30 pulls `TimeWarp.Mediator.Contracts` 14.0.0 so a host gets the request marker interfaces. Mediator 15 moved those types into `TimeWarp.Mediator` 15.0.0-beta.4 and stopped the contracts package. A host that references both assemblies sees duplicate types. This walk points Contracts at `TimeWarp.Mediator` 15 and ships that as `2.0.0-beta.31`. There is no Mediator 14 compatibility shim.

This walk is done when:

- `Directory.Packages.props` and `source/timewarp-contracts/timewarp-contracts.csproj` reference `TimeWarp.Mediator` 15.0.0-beta.4. `TimeWarp.Mediator.Contracts` is gone from both. `TimeWarp.Mediator.Generators` is not added, because this library has no handlers and must not emit a dispatcher.
- The library builds clean for `net10.0` and `net11.0`. A TWM001–TWM004 diagnostic is fixed at the cause and recorded, never suppressed. CS8032 or CS9057 from that analyzer is fixed by moving `global.json` to SDK `11.0.100-rc.3.26522.148`, and that move is recorded.
- `readme.md` and `source/timewarp-contracts/readme.md` name dependencies `TimeWarp.Mediator` (15.0.0-beta.4) and `OneOf`, and say the marker interfaces a request implements (`IQuery<T>`, `ICommand`, `IIdempotentCommand`, ...) come from `TimeWarp.Mediator`.
- `source/Directory.Build.props` `<Version>` is `2.0.0-beta.31`, and `./bin/dev check-version` says that version is not published and is safe to release.
- The packed `TimeWarp.Contracts.2.0.0-beta.31.nupkg` nuspec lists `TimeWarp.Mediator` 15.0.0-beta.4 and `OneOf` in both TFM dependency groups, and does not list `TimeWarp.Mediator.Contracts`. Those groups are pasted under Results.
- `ganda nuget outdated` is pasted under Results. Only the Mediator dependency moves. Any other outdated package is listed and left at its current pin.
- `ganda repo audit --fix` changes are committed, and `./bin/dev workflow` is green.
- No git tag and no GitHub Release. The cut stays `./bin/dev release` from master after merge.

## Requirements

- `Directory.Packages.props`: remove `TimeWarp.Mediator.Contracts` 14.0.0; add
  `TimeWarp.Mediator` 15.0.0-beta.4. Do not add `TimeWarp.Mediator.Generators`: Contracts is a
  library with no handlers, so it must not emit a dispatcher. `TimeWarp.Mediator` 15 has no
  package dependencies of its own on net10.0 or net11.0 (verified from its nuspec).
- `source/timewarp-contracts/timewarp-contracts.csproj`: `PackageReference` swap to
  `TimeWarp.Mediator`.
- `TimeWarp.Mediator` 15 packs the TWM001–TWM004 analyzer. Contracts declares no requests or
  handlers, so it should be silent. If any TWM diagnostic fires, record it under Results and fix
  the cause; do not suppress it. If the build reports CS8032/CS9057 from that analyzer, the SDK
  compiler in `global.json` (`11.0.100-rc.2.26504.105`) is older than the Roslyn the analyzer
  references (5.9.0); the fix is to move `global.json` to `11.0.100-rc.3.26522.148` (installed on
  this machine) and say so in Results, not to pin Mediator back.
- Readmes: the "Dependencies are `TimeWarp.Mediator.Contracts` and `OneOf`" sentence in
  `readme.md` and in `source/timewarp-contracts/readme.md` becomes `TimeWarp.Mediator`
  (15.0.0-beta.4) and `OneOf`, with one added clause that the marker interfaces a request
  implements (`IQuery<T>`, `ICommand`, `IIdempotentCommand`, ...) come from `TimeWarp.Mediator`.
- Version: `source/Directory.Build.props` `<Version>` from `2.0.0-beta.30` to `2.0.0-beta.31`.
  `./bin/dev check-version` must say 2.0.0-beta.31 is not published and safe to release.
- Packed output: after `./bin/dev workflow`, read the nuspec out of the packed
  `TimeWarp.Contracts.2.0.0-beta.31.nupkg` (`unzip -p <nupkg> '*.nuspec'`) and paste the
  dependency groups under Results. Both TFM groups must list `TimeWarp.Mediator` 15.0.0-beta.4 and
  `OneOf`, and no `TimeWarp.Mediator.Contracts`.
- `ganda nuget outdated`: run it and paste the summary under Results. Only the Mediator row moves
  in this task; anything else outdated is listed, not bumped.
- `ganda repo audit --fix`, commit whatever it changes. `./bin/dev workflow` green.
- Do not tag or create a GitHub Release. The cut is `./bin/dev release` from master after merge
  (human `tw-release` step).

## Checklist

- [x] `TimeWarp.Mediator.Contracts` gone from props and csproj; `TimeWarp.Mediator` 15.0.0-beta.4 in both
- [x] No `TimeWarp.Mediator.Generators` reference
- [x] Build clean on net10.0 and net11.0; any TWM or CS8032/CS9057 outcome recorded
- [x] Both readmes name `TimeWarp.Mediator` as the dependency
- [x] `<Version>` 2.0.0-beta.31; `check-version` safe to release
- [x] Packed nuspec dependency groups pasted; no Mediator.Contracts
- [x] `ganda nuget outdated` summary pasted; audit --fix committed; workflow green

## Notes

- Downstream: timewarp-nuru `Directory.Packages.props` holds `TimeWarp.Contracts` 2.0.0-beta.29
  and `TimeWarp.Mediator.Contracts` / `.Generators` 14.0.0 (task 481 comment). Nuru's move to
  Contracts 2.0.0-beta.31 + Mediator 15 is a Nuru task; its second blocker (15 removed
  `ICommandHandler`/`IQueryHandler`, the Nuru endpoint API) is Nuru's own migration, not this task.
- Mediator 15 migration mapping: `documentation/migration.md` in timewarp-mediator
  (`TimeWarp.Mediator.Contracts` → `TimeWarp.Mediator`; `IBaseRequest` → `IRequest`).

## Results

`Directory.Packages.props` and `source/timewarp-contracts/timewarp-contracts.csproj` now reference `TimeWarp.Mediator` 15.0.0-beta.4. `TimeWarp.Mediator.Contracts` is gone from both. `TimeWarp.Mediator.Generators` is not referenced. `<Version>` is `2.0.0-beta.31`. The version comment in `source/Directory.Build.props` names the published floor `2.0.0-beta.30`. Both readmes name dependencies `TimeWarp.Mediator` (15.0.0-beta.4) and `OneOf`, and say the marker interfaces a request implements come from `TimeWarp.Mediator`.

`./bin/dev build` and the workflow rebuild reported 0 warnings and 0 errors for `net10.0` and `net11.0`. A rebuild with `-v:n` put `TimeWarp.Mediator.Analyzers.dll` on the `csc` command line. No TWM001–TWM004 diagnostic fired. No CS8032 or CS9057 fired, so `global.json` stays at `11.0.100-rc.2.26504.105`. The compiler that ran was `11.0.100-rc.3.26522.148`, because `rollForward` is `latestFeature` and that SDK is installed.

`./bin/dev check-version` printed: Version in source `2.0.0-beta.31`. Latest NuGet version `2.0.0-beta.30`. Source is 1 prerelease increment ahead of `v2.0.0-beta.30`. Packages checked: `TimeWarp.Contracts`. `Version in source is new — safe to release.`

`./bin/dev workflow` with `GITHUB_EVENT_NAME` unset ran mode Pr (clean, build, test, pack) and printed `Pipeline SUCCEEDED`. Tests: total 22, failed 0, succeeded 22. Pack wrote `artifacts/packages/TimeWarp.Contracts.2.0.0-beta.31.nupkg` and verified the set. No tag and no GitHub Release.

Nuspec dependency groups (`unzip -p artifacts/packages/TimeWarp.Contracts.2.0.0-beta.31.nupkg '*.nuspec'`):

```xml
<group targetFramework="net10.0">
  <dependency id="OneOf" version="3.0.271" exclude="Build,Analyzers" />
  <dependency id="TimeWarp.Mediator" version="15.0.0-beta.4" exclude="Build,Analyzers" />
</group>
<group targetFramework="net11.0">
  <dependency id="OneOf" version="3.0.271" exclude="Build,Analyzers" />
  <dependency id="TimeWarp.Mediator" version="15.0.0-beta.4" exclude="Build,Analyzers" />
</group>
```

Neither group lists `TimeWarp.Mediator.Contracts`. `exclude="Build,Analyzers"` is the packer's default for a library dependency. Compile and runtime assets still flow, so a host gets the marker interfaces. The analyzer runs when this repository builds Contracts. It does not flow through the packed dependency.

`ganda nuget outdated` checked 12 packages. All are up to date on nuget.org, including `TimeWarp.Mediator` 15.0.0-beta.4. No other package is outdated, and no other pin moved.

`ganda repo audit --fix` reported every fixable check already passing and changed no files. The audit that followed passed 34, failed 0, skipped 3.

### How to validate

Smoke: `./bin/dev check-version`, then `./bin/dev workflow`, then `unzip -p artifacts/packages/TimeWarp.Contracts.2.0.0-beta.31.nupkg '*.nuspec'`.

Expect: check-version prints `Version in source is new — safe to release.` and names `2.0.0-beta.31`. Workflow prints `0 Warning(s)`, `0 Error(s)`, `succeeded: 22`, and `Pipeline SUCCEEDED`. The nuspec groups for `net10.0` and `net11.0` each list `OneOf` 3.0.271 and `TimeWarp.Mediator` 15.0.0-beta.4, and neither group lists `TimeWarp.Mediator.Contracts`.

## Session

- Implementation: swapped the dependency to `TimeWarp.Mediator` 15.0.0-beta.4 and set the package version to `2.0.0-beta.31` (2026-11-01).
