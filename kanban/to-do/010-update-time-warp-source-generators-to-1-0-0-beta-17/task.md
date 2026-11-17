# Update TimeWarp.SourceGenerators to 1.0.0-beta.17

## Description

`ganda nuget outdated` on master (2026-11-17) lists one package behind: TimeWarp.SourceGenerators
1.0.0-beta.16, latest 1.0.0-beta.17. It is marked dev-only, so the published TimeWarp.Contracts
package should not change.

## Requirements

- Update TimeWarp.SourceGenerators to 1.0.0-beta.17 in `Directory.Packages.props`
  (`ganda nuget outdated --update --force`), plus any pin the `global-usings` audit check ties to it.
- Fix any new build errors or warnings the generator update surfaces, in this task.
- No `<Version>` bump unless the packed TimeWarp.Contracts package actually changes; if it does, say so in Results.

## Checklist

- [ ] Update TimeWarp.SourceGenerators to 1.0.0-beta.17
- [ ] `dev build`, `dev test`, `dev pack`
- [ ] `ganda repo audit` passes
- [ ] `ganda nuget outdated` reports everything up to date

## Session

- Created: 1f6a6c8d-4b41-41b4-9d61-a6a74e75bf5f (2026-11-17)

## Notes

- Source version is 2.0.0-beta.34 (unreleased); latest release is v2.0.0-beta.33.
- Cutting a release is a separate explicit ask.
