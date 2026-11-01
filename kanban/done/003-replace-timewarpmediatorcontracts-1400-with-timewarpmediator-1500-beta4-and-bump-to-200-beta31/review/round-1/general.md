# Round 1 — general
**Date:** 2026-11-01
**Scope reviewed:** branch `task/003-replace-timewarpmediatorcontracts-1400-with-timewa` vs `master` (merge base), product paths: `Directory.Packages.props`, `source/timewarp-contracts/timewarp-contracts.csproj`, `source/Directory.Build.props`, `readme.md`, `source/timewarp-contracts/readme.md`. Kanban kitchen moves were not reviewed as product.

## Summary

The change replaces the central pin and the `PackageReference` for `TimeWarp.Mediator.Contracts` 14.0.0 with `TimeWarp.Mediator` 15.0.0-beta.4, does not add `TimeWarp.Mediator.Generators`, bumps `<Version>` to `2.0.0-beta.31` and its floor comment to `2.0.0-beta.30`, and updates both readmes with the dependency and marker-interface sentence the task asked for. Risk is low. The library uses no Mediator type in code. The doc comments in `operation-kind.cs` already name `TimeWarp.Mediator.*` and still match the 15 package.

## Verification (re-run by reviewer)

- `grep` across the tree (outside `kanban/`) finds no `TimeWarp.Mediator.Contracts` and no `TimeWarp.Mediator.Generators`.
- `dotnet build tests/timewarp-contracts-tests/timewarp-contracts-tests.csproj` → `Build succeeded`, 0 Warning(s), 0 Error(s) for both TFMs. No TWM001–TWM004 and no CS8032/CS9057. `global.json` unchanged (`11.0.100-rc.2.26504.105`, `rollForward: latestFeature`), which matches the Requirements: the move is conditional on the CS8032/CS9057 error.
- `dotnet list … package --include-transitive` resolves `TimeWarp.Mediator` 15.0.0-beta.4 and no Mediator.Contracts.
- `TimeWarp.Mediator` 15.0.0-beta.4 nuspec has empty `net10.0` and `net11.0` dependency groups. `lib/` has both TFMs, and the analyzer ships under `analyzers/dotnet/cs`. The lib assembly defines `IQuery`1`, `ICommand`, `ICommand`1`, `IIdempotentCommand`, `IIdempotentCommand`1`, `IRequest`, so the `operation-kind.cs` docs stay accurate.
- Packed `artifacts/packages/TimeWarp.Contracts.2.0.0-beta.31.nupkg` nuspec: both TFM groups list `OneOf` 3.0.271 and `TimeWarp.Mediator` 15.0.0-beta.4 (`exclude="Build,Analyzers"`, the default for a library dependency). No `TimeWarp.Mediator.Contracts`. This matches the Results paste.
- `master` is 2 commits ahead of the merge base. Both are kanban-only (`land kanban 003`), so nothing in product conflicts.
- `tools/dev-cli` imports `TimeWarp.Mediator` through TimeWarp.Nuru. It has no project reference to Contracts, so the swap does not affect it.

## Goal restatement vs task

The `## Goal (implementer restatement)` matches the Description and Requirements, including the conditional `global.json` move, no Generators, the readme clause, check-version, the nuspec paste, `nuget outdated`, audit --fix, and no tag or release. No misread.

## Issues

None.
