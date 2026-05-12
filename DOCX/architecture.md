# NetManager System Architecture

## 1. Overview

NetManager is a LAN-based desktop client-server system for managing internet cafe machines in real time.

### Core goals

- Manage many clients at the same time.
- Sync machine status in real time.
- Support admin actions such as notification, lock/unlock, and timer control.
- Keep the UI responsive by separating network work from UI work.
- Keep the protocol simple enough for a classroom demo.

### Non-goals

- File transfer
- Voice call
- Video streaming
- Complex reporting

---

## 2. High-Level Architecture

```text
+-------------------+         TCP / JSON         +----------------------+
|   ServerApp       | <----------------------->  |      ClientApp       |
|  Admin Dashboard   |                           |   Machine Desktop    |
|  Machine Control   |                           |   Lock Screen        |
|  Session Manager   |                           |   Chat / Timer View  |
+---------+---------+                           +-----------+----------+
          |                                                 |
          | SQLite                                           | Local UI state
          v                                                 v
   +--------------+                               +----------------------+
   |   Database   |                               |   Shared Contracts   |
   |  Users, etc. |                               | Packet, Enum, Utils  |
   +--------------+                               +----------------------+
```

### Main modules

- `ServerApp`: accepts client connections, authenticates users, sends commands, updates machine status.
- `ClientApp`: connects to server, receives commands, shows UI, handles lock screen and timer.
- `Shared`: packet definitions, enums, constants, and helper utilities used by both sides.

---

## 3. Layered Design

### Presentation layer

- Windows Forms UI.
- Admin dashboard on the server.
- Connect/login/chat/timer/lock UI on the client.

### Application layer

- Orchestrates use cases.
- Converts UI actions into network operations.
- Handles incoming events from server or client.

### Network layer

- TCP socket communication.
- Packet framing and parsing.
- Async receive/send loop.

### Domain layer

- Machine state.
- Session state.
- Chat message.
- Command and notification models.

### Data layer

- SQLite for accounts and basic configuration.
- Simple repository or service classes.

---

## 4. Server Architecture

### Server responsibilities

- Accept and manage multiple client connections.
- Authenticate admin and client accounts.
- Maintain machine list and status.
- Send commands such as lock, unlock, notification, and timer updates.
- Receive chat and heartbeat/status packets from clients.

### Suggested server internals

```text
ServerApp
|-- Forms
|   |-- LoginForm
|   `-- DashboardForm
|-- Networking
|   |-- TcpServer
|   |-- ClientConnection
|   |-- PacketDispatcher
|   `-- PacketParser
|-- Services
|   |-- AuthService
|   |-- MachineService
|   |-- NotificationService
|   |-- LockService
|   `-- TimerService
|-- Models
|   |-- Machine
|   |-- SessionInfo
|   `-- ChatMessage
`-- Database
    |-- UserRepository
    `-- SessionRepository
```

### Server data flow

1. Client connects to server.
2. Server creates a connection session.
3. Client sends `LOGIN`.
4. Server validates credentials.
5. Server stores client state and updates dashboard.
6. Admin action sends packet back to client.

---

## 5. Client Architecture

### Client responsibilities

- Connect to the server by IP/Port.
- Authenticate client account.
- Receive commands and update UI.
- Show lock screen when requested.
- Display timer and notification.
- Send chat and status updates.

### Suggested client internals

```text
ClientApp
|-- Forms
|   |-- ConnectForm
|   |-- LoginForm
|   |-- MainForm
|   `-- LockForm
|-- Networking
|   |-- TcpClientService
|   |-- PacketReceiver
|   |-- PacketSender
|   `-- PacketParser
|-- Services
|   |-- SessionService
|   |-- ChatService
|   |-- LockService
|   `-- TimerService
`-- Models
    |-- Notification
    |-- ChatMessage
    `-- ClientState
```

### Client data flow

1. User enters server IP/Port.
2. Client connects to server.
3. Client sends `LOGIN`.
4. Client receives state, timer, and admin commands.
5. Client updates UI or shows lock screen.

---

## 6. Shared Contract

The `Shared` project is the contract between server and client.

### Shared contents

- Packet models
- Enums
- Constants
- Utility functions

### Recommended shared types

- `PacketType`
- `MachineStatus`
- `CommandType`
- `AuthResult`
- `ChatMessage`
- `NotificationMessage`
- `TimerInfo`

### Why shared matters

- Prevents mismatch between server and client packet schema.
- Makes refactoring safer.
- Keeps protocol definitions in one place.

---

## 7. Packet Architecture

### Transport format

JSON over TCP.

### Basic packet structure

```json
{
  "type": "LOGIN",
  "source": "client01",
  "target": "server",
  "payload": {}
}
```

### Key packet groups

- Authentication: `LOGIN`, `LOGOUT`
- Machine state: `STATUS`
- Control: `LOCK`, `UNLOCK`
- Notify: `NOTIFICATION`
- Time: `TIMER`
- Chat: `CHAT`

### Packet rules

- Every packet must have a `type`.
- Payload fields must be stable and versioned by convention.
- Invalid packets should fail gracefully, not crash the app.

---

## 8. State Model

### Machine state

- `Offline`
- `Online`
- `Locked`
- `Playing`

### Session state

- `Disconnected`
- `Connecting`
- `Authenticated`
- `Active`
- `Locked`

### UI state

- `Idle`
- `Busy`
- `Updating`
- `Error`

---

## 9. Concurrency Model

### Server side

- One accept loop for incoming connections.
- One receive loop per client connection or equivalent async task.
- UI updates marshaled to the main thread.

### Client side

- One socket receive loop.
- UI thread kept free from blocking calls.
- Background tasks for reconnect and timer updates.

### Rules

- Never block the UI thread with socket I/O.
- Never update WinForms controls from non-UI threads directly.
- Keep socket read/write operations thread-safe.

---

## 10. Database Architecture

### Recommended tables

- `Users`
- `Sessions`
- `Machines`

### Minimum data stored

- Username
- Password hash
- Role
- Machine identifier
- Last login
- Session state

### Database role

- Used for authentication and simple configuration persistence.
- Not used for heavy analytics in the demo scope.

---

## 11. Deployment Model

### Local LAN setup

- Server runs on one machine.
- Multiple clients run on separate machines or separate app instances.
- All machines connect over the same LAN.

### Demo setup

- One admin dashboard.
- Two or more client instances.
- One SQLite database file on server side.

---

## 12. Failure Handling

### Common failure cases

- Client disconnects unexpectedly.
- Server restarts.
- Packet is malformed.
- Socket timeout.
- UI update race condition.

### Required behavior

- Fail gracefully.
- Show user-friendly message.
- Keep remaining clients running.
- Allow reconnect where possible.

---

## 13. Recommended Sequence Flows

### Login flow

```text
Client -> Connect -> Send LOGIN -> Server validate -> Auth result -> UI update
```

### Lock flow

```text
Admin -> Click Lock -> Server sends LOCK -> Client shows LockForm -> Status updated
```

### Timer flow

```text
Admin -> Set timer -> Server sends TIMER -> Client updates countdown -> Server syncs state
```

### Notification flow

```text
Admin -> Send notification -> Server broadcasts -> Client displays message
```

---

## 14. Design Principles

- Keep the protocol small.
- Keep UI responsive.
- Keep packet and state names consistent.
- Prefer simple service classes over deeply nested logic.
- Integrate early and test often.

---

## 15. Summary

The system architecture is intentionally simple:

- `ServerApp` owns connections, control, and persistence.
- `ClientApp` owns local UI, command handling, and display.
- `Shared` keeps the contract stable.

This structure is enough for a reliable demo and still leaves room for later expansion.
