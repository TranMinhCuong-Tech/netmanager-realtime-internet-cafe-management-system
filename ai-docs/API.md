# API

## Socket Events

Planned packet/event names:

- `LOGIN`
- `LOGOUT`
- `STATUS`
- `LOCK`
- `UNLOCK`
- `NOTIFICATION`
- `TIMER`
- `CHAT`

## Request/Response Formats

### Generic Packet Shape

```json
{
  "type": "LOGIN",
  "source": "client01",
  "target": "server",
  "payload": {}
}
```

### Login Packet

```json
{
  "type": "LOGIN",
  "username": "client01",
  "password": "123456"
}
```

### Chat Packet

```json
{
  "type": "CHAT",
  "sender": "PC01",
  "message": "May em bi lag"
}
```

### Lock Packet

```json
{
  "type": "LOCK"
}
```

### Notification Packet

```json
{
  "type": "NOTIFICATION",
  "message": "Server restart after 10 minutes"
}
```

## API Contracts

- every packet must include a `type`
- shared field names must stay stable across server and client
- invalid packets should fail gracefully
- packet handlers should route by `type`
- transport is expected to be TCP with JSON payloads

## Packet Structures

### Suggested Shared Models

- `PacketType`
- `AuthResult`
- `MachineStatus`
- `CommandType`
- `NotificationMessage`
- `ChatMessage`
- `TimerInfo`
- `SessionInfo`

## Database Schema Summaries

### Users

Suggested fields:

- `Id`
- `Username`
- `PasswordHash`
- `Role`
- `MachineId`
- `IsActive`
- `LastLogin`

### Sessions

Suggested fields:

- `Id`
- `UserId`
- `MachineId`
- `Status`
- `StartTime`
- `EndTime`

### Machines

Suggested fields:

- `Id`
- `MachineName`
- `IpAddress`
- `Status`
- `LastSeen`

## Notes

- This API document is the contract reference for future implementation work.
- If implementation changes the schema, update this file immediately to avoid server/client drift.
