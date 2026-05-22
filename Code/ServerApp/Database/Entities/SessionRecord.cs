using ServerApp.Auth.Models;

namespace ServerApp.Database.Entities;

public sealed record SessionRecord {
    public required string Id { get; init; }

    public required string UserId { get; init; }

    public required string Username { get; init; }

    public required UserRole Role { get; init; }

    public required string MachineId { get; init; }

    public required SessionState State { get; init; }

    public required DateTimeOffset StartedAtUtc { get; init; }

    public DateTimeOffset? EndedAtUtc { get; init; }
}
