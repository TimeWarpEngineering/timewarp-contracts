# Review framework

## Budget (by-diff)

- Lines changed: 2185
- Effort: 3
- TCB hits: none
- Roster axes: general
- Turn cap: 200 (--max-turns; cursor uncapped)

## Framework — task 002

**Date:** 2026-11-01
**Host task:** kanban/in-progress/002-move-timewarpcontracts-source-and-tests-here-from-timewarp-architecture-and-release-from-this-repo/
**Diff scope:** branch `task/002-move-timewarpcontracts-source-and-tests-here-from` at `bfd0ced` vs merge base `99ef169` with `master` (master's 4 commits since are kanban-only "land kanban 002").
**Plan / brief:** `## Goal (implementer restatement)` and `## Requirements` in `task.md`. Copy `TimeWarp.Contracts` from timewarp-architecture `ef2da4f`, trim the csproj, move the contracts-only Jaribu tests, version `2.0.0-beta.30`, pack in PR/merge CI, promote in release mode.
**Effort:** 3 (by-diff budget), roster axes: general
**Reviewer roster:** general
**Session IDs:** review oracle claude-opus-5-5, session 48226d17-40eb-4751-b225-197878743327

## Ground rules

- Reviewers are read-only on product code; they write only under `review/round-N/`
- Severity: bug | suggestion | nit — Status starts as open
- Do not invent issues to fill space; zero issues is a valid outcome
- Address the diff and surrounding call sites; re-verify falsifiable claims against the repo
- Prior rounds are immutable; new work goes in `round-(N+1)/`
