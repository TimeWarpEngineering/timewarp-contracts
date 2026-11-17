# Review framework — task 009

**Date:** 2026-11-17
**Host task:** kanban/in-progress/009-update-time-warp-mediator-to-15-0-0-beta-10/
**Diff scope:** branch `task/009-update-time-warp-mediator-to-15-0-0-beta-10` vs `master` (merge-base). Product commit `7e1d5bf`. Files: `Directory.Packages.props`, `source/Directory.Build.props`, `readme.md`, `source/timewarp-contracts/readme.md`. Kitchen edits to `task.md` are not product scope. Branch is 3 ahead and 1 behind; the behind commit `87cd2b7` is a 0-line kanban land of this task.
**Plan / brief:** Update TimeWarp.Mediator 15.0.0-beta.9 to 15.0.0-beta.10, bump TimeWarp.Contracts to 2.0.0-beta.34, and name the new Mediator version in both readmes. Fix call sites only if beta.10 changes target frameworks or the handler API.
**Effort:** 1 (general only)
**Reviewer roster:** general
**Session IDs:** implementer-claude session e783c598-f6c3-472f-89d4-2da41248a6d1; review oracle grok session 01a1e957-a876-70b2-88dc-8837c0dcb92a

## Budget (by-diff)

- Lines changed: 118
- Effort: 1
- TCB hits: none
- Roster axes: general
- Turn cap: 80 (--max-turns; cursor uncapped)

## Ground rules

- Reviewers are read-only on product code; they write only under `review/round-N/`
- Severity: bug | suggestion | nit — Status starts as open
- Do not invent issues to fill space; zero issues is a valid outcome
- Address the diff and surrounding call sites; re-verify falsifiable claims by reading the diff and the kitchen `## Results` and `### How to validate` record
- Do not re-run a full build, a full test run, an audit, or the hooks; those belong to the implementer's recorded results, the host audit node, and CI
- Exception: one targeted command may check one suspected bug; the finding names the command and the reason. A full build, full test run, or audit is never that command
- A missing or thin recorded result is a finding, not a reason to run the gates
- Prior rounds are immutable; new work goes in `round-(N+1)/`
