# Round 1 — general
**Date:** 2026-11-17
**Scope reviewed:** branch `task/010-update-time-warp-source-generators-to-1-0-0-beta` vs `master`, product commit `16ee491`, plus the implementer `## Results` and `### How to validate` record. Gates were not re-run.

## Summary

The change pins `TimeWarp.SourceGenerators` at 1.0.0-beta.17 in `Directory.Packages.props`. No other package version moved. `Directory.Build.props` references `TimeWarp.SourceGenerators` with `PrivateAssets="all"` and no inline version, so central package management supplies the pin. `.editorconfig` sets `dotnet_diagnostic.TW0007.filename` and `dotnet_diagnostic.globalusingsanalyzer0001.filename` to `global-usings.cs`; neither setting carries a package version. `source/Directory.Build.props` still has `<Version>2.0.0-beta.34</Version>`. The packed nuspec at `source/timewarp-contracts/obj/Release/TimeWarp.Contracts.2.0.0-beta.34.nuspec` depends only on OneOf 3.0.271 and TimeWarp.Mediator 15.0.0-beta.10. Existing restore output under `source/timewarp-contracts/obj` and `tests/timewarp-contracts-tests/obj` resolves `TimeWarp.SourceGenerators` 1.0.0-beta.17. The recorded results cover build (0 warnings, 0 errors), tests (22/22), pack, `ganda nuget outdated`, and `ganda repo audit` (exit 0, one pre-existing advisory). `### How to validate` names the smoke commands and the expected pin, build, test count, and outdated message. The implementer restatement matches the original requirements and checklist. Risk is low. No findings.

## Issues

None.
