# PROJECT_STATE

## Main Memory

This file is the primary memory for future AI sessions.

## Completed Features

- Project scope documented for a realtime internet cafe management system
- Core architecture described for `ServerApp`, `ClientApp`, and `Shared`
- Team roles and responsibilities documented
- Commit rules and general development conventions documented
- Roadmap broken into eight high-level development phases
- Member responsibility guides expanded into end-to-end implementation checklists
- `ai-docs` landing page created for daily project continuity
- Leader flow document added to coordinate who does what and in what order

## Current Feature Being Developed

- Persistent AI memory system under `/ai-docs`
- Repository-level development tracking documentation
- End-to-end member workflow guides for implementation ownership
- Leader coordination flow for sequencing work and dependencies

## Next Planned Tasks

1. Start implementation planning for the networking core.
2. Define the exact packet contract for login, status, lock, unlock, timer, chat, and notification.
3. Confirm the source tree for `ServerApp`, `ClientApp`, and `Shared` once code folders are introduced.
4. Track the first working TCP connection flow.
5. Add implementation notes as code is introduced.

## Important Implementation Notes

- Keep the demo scope narrow and stable.
- Prefer TCP + JSON over more complex transport or serialization schemes.
- Keep UI work separate from socket work to avoid freezing Windows Forms.
- Preserve a shared contract between server and client to prevent packet drift.
- Update this file after every completed coding session or feature change.

## Technologies Used

- C#
- Windows Forms
- TCP sockets
- JSON packets
- async/await
- multithreading
- SQLite
- GitHub

## Current Project Structure

Current repository state is documentation-first.

Observed top-level items:

- `README.md`
- `docs/`
- `ai-docs/`

Planned runtime structure from the architecture notes:

- `ServerApp/`
- `ClientApp/`
- `Shared/`

## Latest Updates Summary

- 2026-05-12: created the `/ai-docs` memory system and seeded it with roadmap, state tracking, architecture, API, bugs, decisions, tasks, and contribution conventions.
- 2026-05-12: confirmed the project is still in the documentation and planning stage, with no application source tree present yet.
- 2026-05-12: expanded the member guides so each role now has phase-by-phase responsibilities from project start to completion.
- 2026-05-12: added a leader flow document describing task order, ownership, dependencies, and handoff sequence.
