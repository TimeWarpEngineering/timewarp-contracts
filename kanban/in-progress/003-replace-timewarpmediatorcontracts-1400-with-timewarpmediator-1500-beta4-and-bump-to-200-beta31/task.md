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

- [ ] `TimeWarp.Mediator.Contracts` gone from props and csproj; `TimeWarp.Mediator` 15.0.0-beta.4 in both
- [ ] No `TimeWarp.Mediator.Generators` reference
- [ ] Build clean on net10.0 and net11.0; any TWM or CS8032/CS9057 outcome recorded
- [ ] Both readmes name `TimeWarp.Mediator` as the dependency
- [ ] `<Version>` 2.0.0-beta.31; `check-version` safe to release
- [ ] Packed nuspec dependency groups pasted; no Mediator.Contracts
- [ ] `ganda nuget outdated` summary pasted; audit --fix committed; workflow green

## Notes

- Downstream: timewarp-nuru `Directory.Packages.props` holds `TimeWarp.Contracts` 2.0.0-beta.29
  and `TimeWarp.Mediator.Contracts` / `.Generators` 14.0.0 (task 481 comment). Nuru's move to
  Contracts 2.0.0-beta.31 + Mediator 15 is a Nuru task; its second blocker (15 removed
  `ICommandHandler`/`IQueryHandler`, the Nuru endpoint API) is Nuru's own migration, not this task.
- Mediator 15 migration mapping: `documentation/migration.md` in timewarp-mediator
  (`TimeWarp.Mediator.Contracts` → `TimeWarp.Mediator`; `IBaseRequest` → `IRequest`).

## Results

(implementer fills in)
