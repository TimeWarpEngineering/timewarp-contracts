# Round 1 — general
**Date:** 2026-11-19
**Scope reviewed:** branch `task/011-refresh-outdated-nu-get-packages-in-time-warp` vs `master` (package pins, three dev-cli endpoints, task kitchen record)

## Summary

The five dev-only pins in `Directory.Packages.props` match the task list, and `source/Directory.Build.props` `<Version>` is unchanged. The six Amuru call sites now use the typed builders, and those builders emit the same arguments as the old `Shell.Builder` lines: `dotnet pack <project> --configuration Release --output <dir>`, `dotnet test --configuration Release --results-directory artifacts/coverage` plus the MTP coverage flags, `gh run list` with one comma-joined `--json`, `gh api` on the same endpoint, `gh run download <id> --name --dir`, and `dotnet nuget push <absolute nupkg> --source --api-key --skip-duplicate`. That mapping is the TimeWarp.Amuru.Tools 3.0.0-beta.10 contract (package XML and the Amuru 3.0.0-beta.10 argument tests). `Git.FindRoot()` returns an absolute root, so dropping the push working directory leaves the absolute package path intact. Recorded gates cover self-install, `./bin/dev build` (0 warnings), and audit. They do not cover a passing `./bin/dev test` after Jaribu.TestingPlatform 1.0.0-beta.16 → 1.0.0-beta.18. No build, test, or audit was re-run for this review.

## Issues

### Issue 1 — Severity: bug
- File: kanban/in-progress/011-refresh-outdated-nu-get-packages-in-time-warp-engineering-timewarp-contracts/task.md:55
- Description: The restatement requires the test project to pass on Jaribu.TestingPlatform 1.0.0-beta.18 (`Directory.Packages.props:7`). `## Results` / `### Gates` record that `./bin/dev build` compiled the test project and that `tests/timewarp-contracts-tests/obj/project.assets.json` restored `TimeWarp.Jaribu.TestingPlatform/1.0.0-beta.18`. The same bullet says CI will run the suite. `### How to validate` lists `./bin/dev test` as a smoke command and states the expected coverage file. Neither section records that the suite was run or that it passed. `artifacts/coverage/` is absent in this worktree. The Jaribu pin moved two betas, and compile-plus-restore does not cover that the suite passes.
- Suggestion: Run `./bin/dev test` from the repo root and record the result (pass count, fail count, and whether `artifacts/coverage/coverage.cobertura.xml` was written) under `## Results`. Fix any failure the run shows. A sentence that CI will run the suite does not close this gap.
- Status: open
