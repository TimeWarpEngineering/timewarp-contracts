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

## Goal (implementer restatement)

Move TimeWarp.Contracts onto TimeWarp.Mediator 15.0.0-beta.10 and ready a 2.0.0-beta.34 package.

Acceptance criteria:
- `Directory.Packages.props` pins TimeWarp.Mediator 15.0.0-beta.10.
- `source/Directory.Build.props` `<Version>` is 2.0.0-beta.34.
- Both readmes (and the csproj comment naming the Mediator version) say 15.0.0-beta.10.
- Any target-framework or handler-API break from beta.10 is fixed here.
- `dev build`, `dev test`, `dev pack`, `dev check-version` and `ganda repo audit` pass;
  `ganda nuget outdated` reports nothing behind.

## Checklist

- [x] Update TimeWarp.Mediator to 15.0.0-beta.10
- [x] Bump version to 2.0.0-beta.34
- [x] Update both readmes
- [x] `dev build`, `dev test`, `dev pack`, `dev check-version`
- [x] `ganda repo audit` passes
- [x] `ganda nuget outdated` reports everything up to date

## Session

- Created: 1f6a6c8d-4b41-41b4-9d61-a6a74e75bf5f (2026-11-17)
- Implement oracle: implemented by implementer-claude (claude, vendor anthropic, model claude-opus-5-5, response model claude-opus-5-5), session e783c598-f6c3-472f-89d4-2da41248a6d1, max-turns 500, Done — 2026-11-17T11:49:50Z

## Results

- `Directory.Packages.props`: TimeWarp.Mediator 15.0.0-beta.9 -> 15.0.0-beta.10 (via `ganda nuget outdated --update --force`).
- `source/Directory.Build.props`: `<Version>` 2.0.0-beta.33 -> 2.0.0-beta.34.
- `readme.md` and `source/timewarp-contracts/readme.md` name Mediator 15.0.0-beta.10.
- beta.10 still ships `lib/net11.0` only (same as beta.9), and the build has no API breaks, so no call-site changes.
  The csproj comment "TimeWarp.Mediator 15.0.0-beta.9 dropped net10.0" stays as history.
- Packed nuspec depends on `TimeWarp.Mediator` 15.0.0-beta.10.

### Gates

- `./bin/dev build` — succeeded, 0 warnings, 0 errors.
- `./bin/dev test` — passed, 22/22.
- `./bin/dev pack` — `TimeWarp.Contracts.2.0.0-beta.34.nupkg` created, packed set verified.
- `./bin/dev check-version` — 2.0.0-beta.34 is new (NuGet latest 2.0.0-beta.33), safe to release.
- `ganda nuget outdated` — all packages up to date.
- `ganda repo audit` — exit 0, passes with 1 advisory warning: `vscode-window-icon` (`.vscode/settings.json` missing).
  Not fixed: that file is gitignored on purpose (commit 183a632, local Peacock output), so it is per-worktree local state, not a repo defect.

### How to validate

Smoke:
```bash
./bin/dev build && ./bin/dev test && ./bin/dev pack
unzip -p artifacts/packages/TimeWarp.Contracts.2.0.0-beta.34.nupkg '*.nuspec' | grep Mediator
ganda nuget outdated
```

Expect: build and 22 tests pass; the nuspec lists `TimeWarp.Mediator` version `15.0.0-beta.10`;
`ganda nuget outdated` prints "All packages are up to date!".

## Notes

- Previous Mediator bump (task 007, beta.8 -> beta.9) dropped net10.0; TimeWarp.Contracts is net11.0 only since 2.0.0-beta.33.
- Cutting the release is a separate explicit ask.
