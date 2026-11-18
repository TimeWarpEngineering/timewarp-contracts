# Round 1 — merged findings
**Date:** 2026-11-19
**Sources:** general

## Counts

| Severity | open | fixed | wontfix |
|----------|------|-------|---------|
| bug | 0 | 1 | 0 |
| suggestion | 0 | 0 | 0 |
| nit | 0 | 0 | 0 |

## Issues

### M1 — Severity: bug — Status: fixed
- File: kanban/in-progress/011-refresh-outdated-nu-get-packages-in-time-warp-engineering-timewarp-contracts/task.md:55
- Description: The restatement requires the test project to pass on Jaribu.TestingPlatform 1.0.0-beta.18 (`Directory.Packages.props:7`). Recorded gates show `./bin/dev build` compiled that project and that `project.assets.json` restored `TimeWarp.Jaribu.TestingPlatform/1.0.0-beta.18`. They do not record an executed `./bin/dev test` or a pass result. `### How to validate` names the command as smoke to run. `artifacts/coverage/` is absent. The pin moved two betas, so a compile record does not cover a passing suite.
- Suggestion: Run `./bin/dev test` from the repo root and record the result under `## Results`, including whether `artifacts/coverage/coverage.cobertura.xml` was written. Fix any failure that run shows.
- Source: general
- Disposition notes: Fixed 2026-11-19. `./bin/dev test` ran the suite on Jaribu.TestingPlatform 1.0.0-beta.18: 22 total, 22 passed, 0 failed, 0 skipped. It wrote `artifacts/coverage/coverage.cobertura.xml`. Recorded under `## Results` / `### Gates` in task.md. No failures, so no code change.

## Duplicates / conflicts

- One reviewer. Nothing to collapse.
