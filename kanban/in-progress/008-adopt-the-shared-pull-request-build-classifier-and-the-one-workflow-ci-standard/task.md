# Adopt the shared pull request build classifier and the one-workflow CI standard

## Description

Bring timewarp-contracts to the org CI standard (ganda 476 / 479 / 565 / 587, audited by ganda 655): `workflow.yml` has
no `paths` / `paths-ignore` filter on `pull_request`, so every pull request reports `CI`, and `dev workflow`
calls the shared classifier `PullRequestBuildClassifier` from `TimeWarp.Nuru.DevCli` 4.0.0-beta.15
(timewarp-nuru 595) to skip build and test on a pull request that touches no build path (kanban, docs, ...).

Tracked by timewarp-ganda 656 (architecture reposet rollout).

Survey 2026-11-16 for this repo: no `pull_request` paths filter; `dev workflow` does not classify. Work: add the classifier call.

## Requirements

1. Run `ganda repo audit --fix` first and commit what it changes.
2. Bump the `TimeWarp.Nuru.DevCli` pin to `4.0.0-beta.15` (and `TimeWarp.Nuru` to the same version if pinned).
3. In the `workflow` command's pull-request path, call `PullRequestBuildClassifier.DecideAsync` first. On skip,
   print its message and exit 0. Push and release paths are unchanged.
4. Remove `paths` / `paths-ignore` from the `pull_request` trigger of `.github/workflows/workflow.yml`. Keep any
   `push` filter and add a comment saying why (a kanban-only merge must not rebuild and upload packages).
5. If this repo builds from roots outside the classifier defaults (`source`, `tests`, `tools`, `msbuild`), declare
   them through the classifier's config (`PullRequestBuildConfig`), not by copying or editing the rule.
6. No local copy of the classification rule remains in `tools/dev-cli/`.
7. `ganda repo audit` passes, including both CI-standard checks from ganda 655.

## Goal (implementer restatement)

Make a pull request that only touches non-build paths (kanban, docs, ...) report a green `CI` run
without building, using the shared `PullRequestBuildClassifier` from `TimeWarp.Nuru.DevCli` 4.0.0-beta.15
instead of a workflow `paths` filter or a local copy of the rule.

Acceptance:

- `TimeWarp.Nuru` and `TimeWarp.Nuru.DevCli` pinned at `4.0.0-beta.15`.
- `dev workflow`'s PR/merge path calls `PullRequestBuildClassifier.DecideAsync` first, prints its
  message, and on skip returns with exit 0. Release path untouched.
- `workflow.yml` keeps no `paths` filter on `pull_request` (already true) and keeps the `push` filter
  with a why comment (already true).
- Packed files outside the protected roots (`assets/logo.png` is the package icon) are declared in
  `.timewarp/dev.jsonc` under `pullRequestBuild.buildPaths`; the rule is not copied.
- `./bin/dev build`, `./bin/dev test`, and `ganda repo audit` (including `dev-workflow-classifier`) pass.

## Checklist

- [x] `ganda repo audit --fix` run and committed
- [x] DevCli pin at 4.0.0-beta.15
- [x] `dev workflow` calls the shared classifier on pull requests; skip exits 0 with its message
- [x] No `paths` filter on `pull_request`; `push` filter kept with a why comment
- [x] Extra build roots (if any) declared through config
- [x] `ganda repo audit` clean
- [ ] This task's pull request shows a `CI` run (record the URL in Results)

## Notes


After merge, the ganda 656 tracker proves a kanban-only pull request here reports `CI` success and merges with
plain `ganda pr merge` (no `--allow-no-ci`).

## Session

- Created: 64677b22-d705-4b25-b537-4b53732bcb92 (2026-11-16)
- Implement (2026-11-16): merged origin/master (kanban land), bumped Nuru + DevCli to 4.0.0-beta.15,
  added the classifier call to `RunPrAsync`, added `.timewarp/dev.jsonc` with `assets/logo.png` as a build path.

## Results

What changed:

- `Directory.Packages.props`: `TimeWarp.Nuru` and `TimeWarp.Nuru.DevCli` 4.0.0-beta.14 -> 4.0.0-beta.15.
- `tools/dev-cli/endpoints/workflow-command.cs`: the PR/merge path finds the repo root and calls
  `PullRequestBuildClassifier.DecideAsync` first, prints `decision.Message`, and on `!RequiresBuild`
  returns with exit code 0 before clean/build/test/pack. Release path unchanged. No local copy of the rule.
- `.timewarp/dev.jsonc` (new): `pullRequestBuild.buildPaths = ["assets/logo.png"]` because
  `source/Directory.Build.props` packs it as the package icon and the default rule treats `assets/` as non-build.
  The packed readme is `source/timewarp-contracts/readme.md`, already under a protected root.
- `.github/workflows/workflow.yml`: already compliant (no `pull_request` paths filter; `push` filter has the
  why comment). No change.
- `ganda repo audit --fix` made no changes.

### How to validate

Smoke:

```bash
./bin/dev self-install   # or: dotnet run --file tools/dev-cli/dev.cs -- self-install
echo '{"pull_request":{"base":{"sha":"509ab96"},"head":{"sha":"502fa83"}}}' > /tmp/e.json
GITHUB_EVENT_NAME=pull_request GITHUB_EVENT_PATH=/tmp/e.json ./bin/dev workflow; echo "exit=$?"
```

Expect: `Pull request changes only declared non-build paths. Skipping build and test. ... (1 changed paths)`,
`Pipeline SUCCEEDED (build and test skipped)`, `exit=0`. (509ab96...502fa83 changes only this kitchen.)
Without `GITHUB_EVENT_NAME=pull_request` the classifier prints a build message and the full pipeline runs.

### Gates

- `./bin/dev build`: Build succeeded, 0 warnings, 0 errors.
- `./bin/dev test`: Passed, 22/22.
- `ganda repo audit --fix`: Repository passes all audit checks (exit 0); `dev-workflow-classifier`, which failed
  on the post-merge attest before this change, now passes. No fixes applied.
- CI run on this task's pull request: recorded after the host opens the PR (open-pr is a later host node).

