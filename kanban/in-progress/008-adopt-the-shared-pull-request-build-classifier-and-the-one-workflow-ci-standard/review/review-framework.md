# Review framework — task 008

**Date:** 2026-11-16
**Host task:** kanban/in-progress/008-adopt-the-shared-pull-request-build-classifier-and-the-one-workflow-ci-standard/
**Diff scope:** branch `task/008-adopt-the-shared-pull-request-build-classifier-and` vs `master` (commits through `82cb28c`). Product files: `.timewarp/dev.jsonc`, `Directory.Packages.props`, `tools/dev-cli/endpoints/workflow-command.cs`. `workflow.yml` is in scope as a requirement and is unchanged.
**Plan / brief:** Adopt `PullRequestBuildClassifier` from `TimeWarp.Nuru.DevCli` 4.0.0-beta.15 so a pull request that touches only non-build paths reports CI and skips build and test. Declare packed `assets/logo.png` via `pullRequestBuild.buildPaths`. Keep the push path filter.
**Effort:** 1 (general only)
**Reviewer roster:** general
**Session IDs:** review Grok `01a1e54b-f9d0-7190-b54f-ec20c925f725`; implement Claude `302845a0-1f0e-4a3b-baca-e3f539212f3d`

## Budget (by-diff)

- Lines changed: 110
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
