# Refresh outdated NuGet packages in TimeWarpEngineering/timewarp-contracts

## Description

Refresh outdated NuGet packages in TimeWarpEngineering/timewarp-contracts.
Filter: (all)
Outdated:
  TimeWarp.Nuru 4.0.0-beta.15 -> 4.0.0-beta.16 (dev-only: tools/dev-cli/Directory.Build.props)
  TimeWarp.Nuru.DevCli 4.0.0-beta.15 -> 4.0.0-beta.16 (dev-only: tools/dev-cli/Directory.Build.props)
  TimeWarp.Amuru 3.0.0-beta.5 -> 3.0.0-beta.10 (dev-only: tools/dev-cli/Directory.Build.props)
  TimeWarp.Amuru.Tools 3.0.0-beta.5 -> 3.0.0-beta.10 (dev-only: tools/dev-cli/Directory.Build.props)
  TimeWarp.Jaribu.TestingPlatform 1.0.0-beta.16 -> 1.0.0-beta.18 (dev-only: tests/timewarp-contracts-tests/timewarp-contracts-tests.csproj)
Every outdated pin is dev-only. Bump them. Do not change <Version> in source/Directory.Build.props: the wave merges this task and does not release this repo.
Edit Directory.Packages.props in this repo only.

## Session

- Minted by ganda task wave run 20261118T170809Z-b6e473
