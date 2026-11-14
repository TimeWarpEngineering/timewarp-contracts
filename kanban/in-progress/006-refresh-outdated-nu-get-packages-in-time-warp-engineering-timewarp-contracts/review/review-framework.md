# Review framework — task 006

**Date:** 2026-11-14
**Host task:** kanban/in-progress/006-refresh-outdated-nu-get-packages-in-time-warp-engineering-timewarp-contracts/
**Diff scope:** branch `task/006-refresh-outdated-nu-get-packages-in-time-warp` vs merge-base `35395e631a12b636c87cc74138e3ddc89e760900` (`master`). Product files: `Directory.Packages.props`, `source/Directory.Build.props`, and Purpose regions in five `.githooks/*.cs` files.
**Plan / brief:** Refresh the six outdated TimeWarp pins in `Directory.Packages.props` and set `<Version>` in `source/Directory.Build.props` to `2.0.0-beta.32`.
**Effort:** 1 (general only)
**Reviewer roster:** general
**Session IDs:** implementer-claude session 2fe5fe84-1c6a-4e31-ac87-c168318035fd; review oracle grok session 01a1d994-245e-7bc0-a311-47ae6b1cf23c

## Budget (by-diff)

- Lines changed: 106
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
