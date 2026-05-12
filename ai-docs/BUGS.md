# BUGS

## Known Bugs

- No application source tree has been committed yet, so runtime bugs cannot be observed in code.
- Existing roadmap and architecture docs describe intended behavior, but implementation status is still pending.

## Unresolved Issues

- Final source folder layout is not yet present in the repository.
- Exact packet schema implementation has not been committed.
- SQLite schema and auth storage details still need implementation.
- Reconnect behavior is only documented, not validated in code.

## Temporary Fixes

- None.

## Debugging Notes

- Treat the current repository as documentation-first until implementation files appear.
- When code is added, record only actionable bugs here.
- Keep each bug entry concise and tied to a reproduction step.

## Bug Tracking Format

- title
- affected module
- reproduction steps
- expected behavior
- actual behavior
- severity
- temporary workaround if any

## Severity Guidance

- High: blocks login, connection, or core demo flow
- Medium: affects usability but has a workaround
- Low: cosmetic or non-blocking issue
