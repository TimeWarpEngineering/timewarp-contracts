# Update TimeWarp.Mediator to 15.0.0-beta.10

## Description

`ganda nuget outdated` on master (2026-11-17) lists one package behind: TimeWarp.Mediator
15.0.0-beta.9, latest 15.0.0-beta.10. Mediator is the library dependency of the published
TimeWarp.Contracts package, so the update changes what consumers restore.

## Requirements

- Update TimeWarp.Mediator to 15.0.0-beta.10 in `Directory.Packages.props`
  (`ganda nuget outdated --update --force`).
- Bump `<Version>` in `source/Directory.Build.props` to 2.0.0-beta.34 (beta.33 is released).
- Update the Mediator version named in `readme.md` and `source/timewarp-contracts/readme.md`.
- If beta.10 changes target frameworks or the handler API, fix the call sites in this task.

## Checklist

- [ ] Update TimeWarp.Mediator to 15.0.0-beta.10
- [ ] Bump version to 2.0.0-beta.34
- [ ] Update both readmes
- [ ] `dev build`, `dev test`, `dev pack`, `dev check-version`
- [ ] `ganda repo audit` passes
- [ ] `ganda nuget outdated` reports everything up to date

## Session

- Created: 1f6a6c8d-4b41-41b4-9d61-a6a74e75bf5f (2026-11-17)

## Notes

- Previous Mediator bump (task 007, beta.8 -> beta.9) dropped net10.0; TimeWarp.Contracts is net11.0 only since 2.0.0-beta.33.
- Cutting the release is a separate explicit ask.
