# Update outdated NuGet packages

## Description

`ganda nuget outdated` listed six packages behind on master. Update all of them.

## Checklist

- [x] `ganda nuget outdated --update --force` (Nuru, Nuru.DevCli, Amuru, Amuru.Tools, Terminal, Mediator)
- [x] Drop net10.0 from TimeWarp.Contracts (Mediator 15.0.0-beta.9 is net11.0 only)
- [x] Bump version to 2.0.0-beta.33
- [x] `dev build`, `dev test` (22/22), `dev pack`, `dev check-version`, `ganda repo audit` (43 passed, 0 failed)
- [ ] PR open and CI green

## Session

- Created: 1f6a6c8d-4b41-41b4-9d61-a6a74e75bf5f (2026-11-16)
- Implementation: 1f6a6c8d-4b41-41b4-9d61-a6a74e75bf5f (2026-11-16)

## Results

- TimeWarp.Nuru and Nuru.DevCli 4.0.0-beta.13 -> beta.14, TimeWarp.Amuru and Amuru.Tools 3.0.0-beta.2 -> beta.5,
  TimeWarp.Terminal 2.0.0-beta.1 -> beta.2, TimeWarp.Mediator 15.0.0-beta.8 -> beta.9.
- Mediator beta.9 ships only `lib/net11.0`, so restore failed with NU1202 for the net10.0 target. Steve chose
  (2026-11-16) to drop net10.0: TimeWarp.Contracts is now net11.0 only. This is a breaking change for net10 hosts.
- `dev pack` layout check, readmes, and the CI workflow (no .NET 10 setup step) follow the single target.
- `ganda repo audit --fix` added `.githooks/prepare-commit-msg` and its `.cs` source.
- No dev-CLI code changes were needed for the Nuru, Amuru, or Terminal bumps.
