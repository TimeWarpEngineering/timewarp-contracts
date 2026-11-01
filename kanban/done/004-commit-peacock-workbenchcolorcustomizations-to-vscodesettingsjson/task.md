# Commit Peacock workbench.colorCustomizations to .vscode/settings.json

## Description

`ganda repo init` (vscode-window-icon fix) writes only `peacock.color` / `peacock.remoteColor`.
Opening the repo in VS Code makes the Peacock extension write the derived
`workbench.colorCustomizations` block, which dirties the origin-home checkout and makes every
`ganda kanban` command refuse.

Resolution: stop tracking `.vscode/settings.json` and gitignore it. Committing Peacock's block
only lasts until Peacock changes its output, and ganda can't write that block itself because
Peacock derives it from `peacock.color`.

## Checklist

- [x] Move Peacock's change from origin-home into the task worktree
- [x] ~~Commit the full `workbench.colorCustomizations` block~~ (replaced by gitignore)
- [x] Gitignore `.vscode/settings.json` and remove it from the index
- [x] `ganda repo audit` passes (vscode-window-icon still passes on the local file)
- [x] PR opened (#4)

## Session

- Created: e43d9506-d81a-4053-b690-84559e7dc1d0 (2026-11-01)
- Implementation: e43d9506-d81a-4053-b690-84559e7dc1d0 (2026-11-01)

## Notes

- After merge, existing checkouts keep their local `settings.json`; fresh clones and new task
  worktrees have none until `ganda repo audit --fix` (vscode-window-icon) writes one.
- Possible ganda follow-up: let the kanban dirty check ignore tracked changes that the
  fast-forward does not touch.
