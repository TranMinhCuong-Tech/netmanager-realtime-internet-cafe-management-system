# ARCHITECTURE

## Overall System Architecture

NetManager follows a desktop client-server model over TCP in a LAN environment.

- `ServerApp` owns admin control, machine coordination, and persistence.
- `ClientApp` owns local user interaction, command display, and lock screen behavior.
- `Shared` owns packet models, enums, constants, and helper utilities used by both sides.

## Client/Server Flow

1. Client enters server IP and port.
2. Client connects to the TCP server.
3. Client sends a login packet.
4. Server validates credentials and registers the session.
5. Server pushes machine state and admin commands to connected clients.
6. Client updates its UI and local state from received packets.

## Module Responsibilities

### ServerApp

- accept and manage multiple clients
- authenticate users
- maintain machine/session state
- send control packets
- update the admin dashboard

### ClientApp

- connect to server
- send login and status packets
- receive commands and notifications
- show lock screen when requested
- keep UI responsive while networking runs in background threads

### Shared

- define packet types
- define enums and constants
- define simple DTOs for transport
- keep server/client contract consistent

## Folder Structure

Current planned structure:

```text
NetManager/
|-- ServerApp/
|   |-- Forms/
|   |-- Networking/
|   |-- Models/
|   |-- Services/
|   `-- Database/
|-- ClientApp/
|   |-- Forms/
|   |-- Networking/
|   |-- Models/
|   `-- Services/
|-- Shared/
|   |-- Packets/
|   |-- Constants/
|   `-- Utilities/
|-- docs/
`-- ai-docs/
```

## Data Flow

### Admin Control Flow

- admin action on server UI
- server service layer creates a packet
- packet sent through TCP connection
- client receives packet and updates UI/state

### Client Status Flow

- client detects local state change
- client sends status packet
- server updates dashboard and internal machine model

### Authentication Flow

- UI gathers credentials
- auth service checks credentials against SQLite or cached rules
- result is returned to the network layer and UI

## Communication Protocols

- transport: TCP
- payload format: JSON
- update model: event-driven packet exchange
- threading model: socket I/O must not block the UI thread

## Concurrency Rules

- never update WinForms controls directly from network threads
- use background receive loops or async tasks for socket I/O
- marshal UI updates back to the UI thread
- keep per-client state isolated on the server

## Design Constraints

- demo scope is intentionally small
- packet schema should remain stable once integration starts
- keep dependencies light
- favor readability and predictability over clever abstractions
