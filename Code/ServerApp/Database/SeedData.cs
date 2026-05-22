using System.Globalization;
using Microsoft.Data.Sqlite;
using ServerApp.Auth.Models;
using ServerApp.Auth.Services;

namespace ServerApp.Database;

public sealed class SeedData {
    private static readonly SeedAccount[] DemoAccounts = {
        new("admin01", "123456", "SERVER", UserRole.Admin),
        new("pc01", "123456", "PC-01", UserRole.Client),
        new("pc02", "123456", "PC-02", UserRole.Client),
        new("pc03", "123456", "PC-03", UserRole.Client)
    };

    private static readonly (string Id, string MachineId, string MachineName, string? IpAddress, string Status)[] DemoMachines = {
        ("machine-server", "SERVER", "Server", null, "Online"),
        ("machine-pc01", "PC-01", "Computer 01", null, "Offline"),
        ("machine-pc02", "PC-02", "Computer 02", null, "Offline"),
        ("machine-pc03", "PC-03", "Computer 03", null, "Offline")
    };

    public async Task SeedAsync(SqliteConnection connection, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(connection);

        foreach (var account in DemoAccounts) {
            await EnsureUserAsync(connection, account, cancellationToken).ConfigureAwait(false);
        }

        foreach (var machine in DemoMachines) {
            await EnsureMachineAsync(connection, machine, cancellationToken).ConfigureAwait(false);
        }
    }

    private static async Task EnsureUserAsync(SqliteConnection connection, SeedAccount account, CancellationToken cancellationToken) {
        await using var checkCommand = connection.CreateCommand();
        checkCommand.CommandText = """
            SELECT 1
            FROM Users
            WHERE Username = @Username
            LIMIT 1;
            """;
        checkCommand.Parameters.AddWithValue("@Username", account.Username);

        var exists = await checkCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false) is not null;
        if (exists) {
            return;
        }

        var passwordHash = PasswordHasher.Hash(account.Password);

        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText = """
            INSERT INTO Users
            (
                Id,
                Username,
                PasswordHashBase64,
                PasswordSaltBase64,
                Role,
                MachineId,
                IsActive,
                LastLoginAtUtc
            )
            VALUES
            (
                @Id,
                @Username,
                @PasswordHashBase64,
                @PasswordSaltBase64,
                @Role,
                @MachineId,
                @IsActive,
                @LastLoginAtUtc
            );
            """;
        insertCommand.Parameters.AddWithValue("@Id", $"user-{account.Username}");
        insertCommand.Parameters.AddWithValue("@Username", account.Username);
        insertCommand.Parameters.AddWithValue("@PasswordHashBase64", passwordHash.HashBase64);
        insertCommand.Parameters.AddWithValue("@PasswordSaltBase64", passwordHash.SaltBase64);
        insertCommand.Parameters.AddWithValue("@Role", account.Role.ToString());
        insertCommand.Parameters.AddWithValue("@MachineId", account.MachineId);
        insertCommand.Parameters.AddWithValue("@IsActive", account.IsActive ? 1 : 0);
        insertCommand.Parameters.AddWithValue("@LastLoginAtUtc", DBNull.Value);

        await insertCommand.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    private static async Task EnsureMachineAsync(
        SqliteConnection connection,
        (string Id, string MachineId, string MachineName, string? IpAddress, string Status) machine,
        CancellationToken cancellationToken) {
        await using var checkCommand = connection.CreateCommand();
        checkCommand.CommandText = """
            SELECT 1
            FROM Machines
            WHERE MachineId = @MachineId
            LIMIT 1;
            """;
        checkCommand.Parameters.AddWithValue("@MachineId", machine.MachineId);

        var exists = await checkCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false) is not null;
        if (exists) {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText = """
            INSERT INTO Machines
            (
                Id,
                MachineId,
                MachineName,
                IpAddress,
                Status,
                LastHeartbeatAtUtc
            )
            VALUES
            (
                @Id,
                @MachineId,
                @MachineName,
                @IpAddress,
                @Status,
                @LastHeartbeatAtUtc
            );
            """;
        insertCommand.Parameters.AddWithValue("@Id", machine.Id);
        insertCommand.Parameters.AddWithValue("@MachineId", machine.MachineId);
        insertCommand.Parameters.AddWithValue("@MachineName", machine.MachineName);
        insertCommand.Parameters.AddWithValue("@IpAddress", (object?)machine.IpAddress ?? DBNull.Value);
        insertCommand.Parameters.AddWithValue("@Status", machine.Status);
        insertCommand.Parameters.AddWithValue("@LastHeartbeatAtUtc", DBNull.Value);

        await insertCommand.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }
}
