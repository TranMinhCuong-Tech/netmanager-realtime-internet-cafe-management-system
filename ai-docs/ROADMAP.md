# ROADMAP

## Project Vision

NetManager is a LAN-based desktop client-server system for managing internet cafe machines in real time. The project focuses on reliable TCP communication, responsive Windows Forms UIs, and a small but complete demo feature set:

- multi-client connection
- admin dashboard control
- client lock/unlock
- session timer
- realtime notification
- basic chat
- SQLite-backed authentication

The long-term goal is to keep the system simple, stable, and easy for future AI sessions to extend without re-deriving the architecture.

## Development Phases

### Phase 0 - Project Bootstrap

- establish project memory files
- confirm scope and demo boundaries
- document architecture, contracts, and team responsibilities

### Phase 1 - Networking Foundation

- build TCP server and client connection flow
- standardize JSON packet contracts
- support multi-client handling and reconnect basics

### Phase 2 - Authentication and Session Core

- implement login validation
- add session tracking
- connect server state to machine/user state

### Phase 3 - GUI Integration

- server admin dashboard
- client connection/login screens
- realtime status rendering without UI blocking

### Phase 4 - Realtime Control Features

- notification
- lock/unlock
- timer sync
- basic chat

### Phase 5 - Persistence and Hardening

- SQLite integration
- error handling
- reconnect and disconnect resilience
- logging and stability fixes

### Phase 6 - Testing and Demo Readiness

- multi-client tests
- integration checks
- bug triage
- documentation cleanup

## Milestones

- `v0.1-bootstrap`: docs, scope, and folder conventions established
- `v0.2-network-core`: TCP connection and packet baseline working
- `v0.3-auth-session`: login and session flow stable
- `v0.4-gui-core`: server and client UI connected to network events
- `v0.5-lock-system`: lock/unlock flow working end to end
- `v0.6-database`: SQLite auth and config persistence integrated
- `v0.7-test-ready`: main flows validated with multi-client tests
- `v1.0-final-release`: demo-ready build and documentation complete

## Future Features

- stronger session management
- richer machine monitoring
- search/filter in admin dashboard
- improved reconnect behavior
- structured logging
- optional reporting and analytics

## Current Phase

Phase 0 - Project Bootstrap and Contract Definition.

The repository is currently documentation-first, so the immediate work is to keep the project memory files accurate and ready for implementation sessions.
