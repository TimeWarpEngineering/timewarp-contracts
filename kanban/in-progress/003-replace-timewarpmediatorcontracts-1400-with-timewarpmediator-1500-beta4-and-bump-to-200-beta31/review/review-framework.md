# Review framework

## Budget (by-diff)

- Lines changed: 206
- Effort: 2
- TCB hits: none
- Roster axes: general
- Turn cap: 120 (--max-turns; cursor uncapped)

## Scope

**Date:** 2026-11-01
**Host task:** kanban/in-progress/003-replace-timewarpmediatorcontracts-1400-with-timewarpmediator-1500-beta4-and-bump-to-200-beta31/
**Diff scope:** branch `task/003-replace-timewarpmediatorcontracts-1400-with-timewa` vs `master` merge base (product commit `fa79263`)
**Plan / brief:** swap `TimeWarp.Mediator.Contracts` 14.0.0 for `TimeWarp.Mediator` 15.0.0-beta.4, bump to `2.0.0-beta.31`, update readmes
**Effort:** 2
**Reviewer roster:** general
**Session IDs:** review oracle (claude, model claude-opus-5-5), ganda task work headless

## Ground rules

- Reviewers are read-only on product code; they write only under `review/round-N/`
- Severity: bug | suggestion | nit — Status starts as open
- Do not invent issues to fill space; zero issues is a valid outcome
- Address the diff and surrounding call sites; re-verify falsifiable claims against the repo
- Prior rounds are immutable; new work goes in `round-(N+1)/`
