# Commit Peacock workbench.colorCustomizations to .vscode/settings.json

## Description

`ganda repo init` (vscode-window-icon fix) writes only `peacock.color` / `peacock.remoteColor`.
Opening the repo in VS Code makes the Peacock extension write the derived
`workbench.colorCustomizations` block, which dirties the origin-home checkout and makes every
`ganda kanban` command refuse. Commit the block Peacock writes so the file matches what the
extension expects.

## Checklist

- [x] Move Peacock's change from origin-home into the task worktree
- [x] Commit `.vscode/settings.json` with the full `workbench.colorCustomizations` block
- [x] `ganda repo audit` passes (34 passed, 0 failed, 3 skipped)
- [ ] PR opened

## Session

- Created: e43d9506-d81a-4053-b690-84559e7dc1d0 (2026-11-01)
- Implementation: e43d9506-d81a-4053-b690-84559e7dc1d0 (2026-11-01)

## Notes

Upstream follow-up (ganda, not this repo): have the vscode-window-icon fix write the same
`workbench.colorCustomizations` Peacock derives, so new repos never drift on first open.
