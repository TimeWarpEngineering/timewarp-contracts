# Round 1 — general
**Date:** 2026-11-01
**Scope reviewed:** `99ef169..bfd0ced` (all non-kanban paths), checked against timewarp-architecture `ef2da4f`.

## Summary

The change copies `TimeWarp.Contracts` into `source/timewarp-contracts/`, moves six contracts-only
Jaribu files to `tests/timewarp-contracts-tests/`, sets version `2.0.0-beta.30`, and replaces the
verify-only `workflow` command with the pack-and-promote pipeline. Risk is low. The copy is faithful
and every falsifiable claim in Results held when re-run. Two small CI-trigger and doc gaps remain,
both inherited from the scaffold.

## Verification (re-run by reviewer)

- Source: every `.cs` file is byte-identical to `ef2da4f:source/foundation/timewarp-contracts/`.
  Only `readme.md` (home and host wording) and the csproj (analyzer `ProjectReference` removed,
  `IsPackable` added) differ. Namespaces (`TimeWarp.Contracts`, plus `TimeWarp.Foundation.Types` for
  `SharedProblemDetails`), `PackageId`, `RootNamespace`, and Purpose/Design regions are unchanged.
- Tests: the six test files are byte-identical to architecture. `global-usings.cs` drops only the
  three `TimeWarp.Foundation.*` usings. `shared-problem-details-forward-tests.cs` and the
  Foundation-dependent tests stayed behind.
- Pins: `TimeWarp.Mediator.Contracts` 14.0.0, `OneOf` 3.0.271, Jaribu 1.0.0-beta.15, and Shouldly
  4.3.0 match architecture's `Directory.Packages.props` at `ef2da4f`.
- nuget.org flat container: the highest `TimeWarp.Contracts` is `2.0.0-beta.29`, so `2.0.0-beta.30` is correct.
- `./bin/dev build` gave 0 warnings and 0 errors. `./bin/dev test` gave total 22, succeeded 22.
- `dotnet run --file tools/dev-cli/dev.cs -- pack` verified the set. The nupkg has
  `lib/net10.0/timewarp-contracts.dll`, `lib/net11.0/timewarp-contracts.dll`, `readme.md`, and
  `logo.png`. The nuspec lists only `OneOf` and `TimeWarp.Mediator.Contracts` for both TFMs.
- `ganda repo audit`: "Repository passes all audit checks."
- `workflow.yml` adds .NET 11 preview setup, uploads `Packages-${run_number}` outside release mode,
  and passes the `nuget/login` OIDC key only in release and break-glass mode. timewarp-architecture
  is untouched.

## Issues

### Issue 1 — Severity: suggestion
- File: .github/workflows/workflow.yml:11
- Description: The `push.paths` filter leaves out paths that change the build or the package:
  `assets/**` (the packed `logo.png`), `msbuild/**` (`repository.props`), and `BannedSymbols.txt`.
  A merge that touches only these gets no CI run on master. `dev release` then refuses, because the
  commit is not kanban-only and has no green run, until someone dispatches a merge run.
- Suggestion: Add `assets/**`, `msbuild/**`, and `BannedSymbols.txt` to `push.paths`, or fix the
  list in the `ganda repo init` scaffold so every repo gets it.
- Status: open

### Issue 2 — Severity: nit
- File: .github/workflows/workflow.yml:29
- Description: The `workflow_dispatch` `mode` input still says "merge = build and test only". This
  change makes merge mode pack too and upload `Packages-*`, as the updated header comment says.
- Suggestion: Reword to "merge = build, test, and pack (default)".
- Status: open
