using ServerApp.Auth.Models;

namespace ServerApp.Database.Entities;

public sealed record UserRecord {
    public required string Id { get; init; }

    public required string Username { get; init; }

    public required string PasswordHashBase64 { get; init; }

    public required string PasswordSaltBase64 { get; init; }

    public required UserRole Role { get; init; }

    public required string MachineId { get; init; }

    public required bool IsActive { get; init; }

    public DateTimeOffset? LastLoginAtUtc { get; init; }
}
