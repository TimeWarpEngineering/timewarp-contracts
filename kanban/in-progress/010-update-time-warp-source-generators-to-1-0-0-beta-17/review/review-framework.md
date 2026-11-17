# Review framework — task 010

**Date:** 2026-11-17
**Host task:** kanban/in-progress/010-update-time-warp-source-generators-to-1-0-0-beta-17/
**Diff scope:** branch `task/010-update-time-warp-source-generators-to-1-0-0-beta` vs `master` (merge-base `10ee5f7`). Product commit `16ee491`. Product file: `Directory.Packages.props`. Kitchen edits to `task.md` are not product scope. Branch is 3 ahead and 1 behind; the behind commit `3d574a4` is a 0-line kanban land of this task.
**Plan / brief:** Update the dev-only package TimeWarp.SourceGenerators from 1.0.0-beta.16 to 1.0.0-beta.17 in central package management, including any pin the global-usings audit check ties to that package. Fix warnings or errors the new generator surfaces. Do not bump `<Version>` unless the packed TimeWarp.Contracts package changes.
**Effort:** 1 (general only)
**Reviewer roster:** general
**Session IDs:** implementer-claude session not reported; review oracle grok session 01a1ea41-ed08-7890-9719-6f636156ffb4

## Budget (by-diff)

- Lines changed: 109
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
