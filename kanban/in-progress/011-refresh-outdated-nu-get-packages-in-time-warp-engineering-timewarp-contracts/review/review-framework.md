# Review framework — task 011

**Date:** 2026-11-19
**Host task:** kanban/in-progress/011-refresh-outdated-nu-get-packages-in-time-warp-engineering-timewarp-contracts/
**Diff scope:** branch `task/011-refresh-outdated-nu-get-packages-in-time-warp` vs `master` — `Directory.Packages.props`, `tools/dev-cli/endpoints/pack-command.cs`, `tools/dev-cli/endpoints/test-command.cs`, `tools/dev-cli/endpoints/workflow-release-steps.cs`, and this task's `task.md`. Untracked `review/` is this round.
**Plan / brief:** Bump the five outdated dev-only pins. Amuru 3.0.0-beta.10 analyzer AMURU0001 required typed-builder migrations at six dev-cli call sites.
**Effort:** 1 (general only)
**Reviewer roster:** general
**Session IDs:** grok 01a1f00d-5a45-73b2-b452-1710dbc3522d (2026-11-19). Implementer session was not reported.

## Budget (by-diff)

- Lines changed: 85
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

## Round 2 scope

**Date:** 2026-11-19
**Diff scope:** `191903b819f1a534245380049d9c8d7d8ff0b6d8`..`1aa1137e1eb3e0fc2100b4ecfc8ae8a1216efa1c` — kitchen only (`task.md`, `review/review-framework.md`, `review/round-1/`). Re-review of M1. No product path in the delta. The 85-line budget above is this delta.
**Session IDs:** grok 01a1f014-be18-76f2-862c-298241009bbf (2026-11-19), effort 1, roster general, turn cap 80
