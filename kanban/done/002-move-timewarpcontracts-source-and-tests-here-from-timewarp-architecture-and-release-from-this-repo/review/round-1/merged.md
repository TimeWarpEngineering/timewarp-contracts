# Round 1 — merged findings
**Date:** 2026-11-01
**Sources:** general

## Counts

| Severity | open | fixed | wontfix |
|----------|------|-------|---------|
| bug | 0 | 0 | 0 |
| suggestion | 0 | 0 | 1 |
| nit | 0 | 0 | 1 |

## Issues

### M1 — Severity: suggestion — Status: wontfix
- File: .github/workflows/workflow.yml:11
- Description: `push.paths` leaves out `assets/**`, `msbuild/**`, and `BannedSymbols.txt`. A merge
  that touches only those gets no master CI run, so `dev release` refuses until someone dispatches a merge run.
- Suggestion: Add those paths, or fix the list in the `ganda repo init` scaffold.
- Source: general
- Disposition notes: wontfix on this task. Decided by the review oracle. The list comes from the
  scaffold and is the same in every repo built from it, so the fix belongs in `ganda repo init`.
  The gap fails safe: nothing publishes, and the `dev release` refusal names the path. The task's
  own paths (`source/**`, `tests/**`, `tools/**`, `global.json`, slnx) are covered.

### M2 — Severity: nit — Status: wontfix
- File: .github/workflows/workflow.yml:29
- Description: The `workflow_dispatch` `mode` description still says "merge = build and test only". Merge mode now packs too.
- Suggestion: Reword to "merge = build, test, and pack (default)".
- Source: general
- Disposition notes: wontfix on this task. Decided by the review oracle. The text is scaffold-owned
  and does not change behavior. The header comment in the same file states the merge pipeline
  correctly. A fix round only for this wording costs more than it is worth.

## Duplicates / conflicts

- None (single reviewer).
