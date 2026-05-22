namespace ServerApp.Database.Entities;

public sealed record MachineRecord {
    public required string Id { get; init; }

    public required string MachineId { get; init; }

    public required string MachineName { get; init; }

    public string? IpAddress { get; init; }

    public required string Status { get; init; }

    public DateTimeOffset? LastHeartbeatAtUtc { get; init; }
}
