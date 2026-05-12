# CONTRIBUTIONS

## Member Responsibilities

### Member 1 - Team Leader / System Architect

- own overall scope and architecture
- keep milestones aligned with the demo goal
- review integration risk

### Member 2 - Network Engineer

- implement TCP server/client behavior
- manage packet handling and reconnect logic
- keep networking off the UI thread

### Member 3 - Server GUI Developer

- build the admin dashboard
- show machine status in realtime
- provide lock, unlock, timer, and notification controls

### Member 4 - Client App Developer

- build the client connection flow
- show lock screen and notifications
- handle realtime client-side updates

### Member 5 - Database & Authentication

- implement SQLite storage
- validate login credentials
- manage session and configuration persistence

### Member 6 - Tester & Documentation

- test core flows
- report bugs
- maintain user-facing documentation and demo notes

## Completed Contributions

- documented the project architecture and intended module split
- documented team roles and deliverables
- documented commit and branch conventions
- created the `/ai-docs` project memory set

## Commit Conventions

Use the repository rule:

```text
type(scope): short message
```

Recommended examples:

- `feat(network): add tcp client loop`
- `fix(gui): prevent ui freeze`
- `docs(architecture): update packet contract`

Allowed types:

- `feat`
- `fix`
- `refactor`
- `docs`
- `test`
- `chore`

Allowed scopes:

- `chat`
- `server`
- `network`
- `gui`
- `database`
- `lock`
- `docs`
- `test`
- `auth`
- `shared`

## Branch Conventions

Suggested branch flow:

- `main`
- `develop`
- `feature/<area>`

Recommended examples:

- `feature/network-core`
- `feature/gui-dashboard`
- `feature/database-auth`
- `feature/lock-system`

## Collaboration Notes

- Keep one main responsibility per contributor when possible.
- Do not overwrite another contributor’s unfinished work.
- Update `PROJECT_STATE.md` and `TASKS.md` after each completed session.
- Add new implementation notes to the appropriate memory file instead of scattering them in chat.
