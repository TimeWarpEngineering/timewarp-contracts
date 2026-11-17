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

## Goal (implementer restatement)

Bump the dev-only analyzer/generator package TimeWarp.SourceGenerators from 1.0.0-beta.16 to
1.0.0-beta.17 in central package management, and leave the repo building clean.

Acceptance:
- `Directory.Packages.props` pins TimeWarp.SourceGenerators 1.0.0-beta.17; no other pin (e.g. a
  `global-usings` audit tie) left on the old version.
- `dev build`, `dev test`, `dev pack` succeed with no new warnings/errors; anything the new
  generator surfaces is fixed here.
- `ganda repo audit` passes; `ganda nuget outdated` reports nothing behind.
- `<Version>` is not bumped unless the packed TimeWarp.Contracts output changes (it is PrivateAssets=all,
  so it is not expected to).

## Checklist

- [x] Update TimeWarp.SourceGenerators to 1.0.0-beta.17
- [x] `dev build`, `dev test`, `dev pack`
- [x] `ganda repo audit` passes
- [x] `ganda nuget outdated` reports everything up to date

## Results

- `Directory.Packages.props`: TimeWarp.SourceGenerators 1.0.0-beta.16 -> 1.0.0-beta.17 (via
  `ganda nuget outdated --update --force`). No other pin needed: the only related config is
  `dotnet_diagnostic.TW0007.filename` in `.editorconfig`, which is version-free.
- The generator update surfaced no new warnings or errors; no code changes.
- No `<Version>` bump: the packed `TimeWarp.Contracts.2.0.0-beta.34.nuspec` lists only OneOf and
  TimeWarp.Mediator as dependencies (SourceGenerators is `PrivateAssets="all"`), so the published
  package does not change.

### Gates

| Gate | Command | Result |
|------|---------|--------|
| Build | `./bin/dev build` | Build succeeded, 0 warnings, 0 errors |
| Test | `./bin/dev test` | Passed, 22/22 |
| Pack | `./bin/dev pack` | `TimeWarp.Contracts.2.0.0-beta.34.nupkg`, packed set verified |
| Outdated | `ganda nuget outdated` | All packages are up to date |
| Audit | `ganda repo audit` | Exit 0, repository passes; 1 pre-existing advisory warning (`vscode-window-icon`: `.vscode/settings.json` missing), non-blocking and unrelated to this change |

### How to validate

Smoke:
```bash
grep -o 'TimeWarp.SourceGenerators" Version="[^"]*"' Directory.Packages.props
./bin/dev build && ./bin/dev test
ganda nuget outdated
```

Expect: version `1.0.0-beta.17`; build with 0 warnings / 0 errors; 22 tests pass;
`All packages are up to date!`.

## Session

- Created: 1f6a6c8d-4b41-41b4-9d61-a6a74e75bf5f (2026-11-17)
- Implement oracle: implemented by implementer-claude (claude, vendor anthropic, model claude-opus-5-5), session not reported, max-turns 500, Done — 2026-11-17T16:05:43Z

## Notes

- Source version is 2.0.0-beta.34 (unreleased); latest release is v2.0.0-beta.33.
- Cutting a release is a separate explicit ask.
