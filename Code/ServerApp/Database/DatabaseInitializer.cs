using System.Globalization;
using Microsoft.Data.Sqlite;

namespace ServerApp.Database;

public sealed class DatabaseInitializer {
    public async Task InitializeAsync(SqliteConnection connection, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(connection);

        await using var command = connection.CreateCommand();
        command.CommandText = """
            PRAGMA foreign_keys = ON;

            CREATE TABLE IF NOT EXISTS Users
            (
                Id TEXT PRIMARY KEY,
                Username TEXT NOT NULL UNIQUE,
                PasswordHashBase64 TEXT NOT NULL,
                PasswordSaltBase64 TEXT NOT NULL,
                Role TEXT NOT NULL,
                MachineId TEXT NOT NULL,
                IsActive INTEGER NOT NULL DEFAULT 1,
                LastLoginAtUtc TEXT
            );

            CREATE TABLE IF NOT EXISTS Machines
            (
                Id TEXT PRIMARY KEY,
                MachineId TEXT NOT NULL UNIQUE,
                MachineName TEXT NOT NULL,
                IpAddress TEXT,
                Status TEXT NOT NULL,
                LastHeartbeatAtUtc TEXT
            );

            CREATE TABLE IF NOT EXISTS Sessions
            (
                Id TEXT PRIMARY KEY,
                UserId TEXT NOT NULL,
                Username TEXT NOT NULL,
                Role TEXT NOT NULL,
                MachineId TEXT NOT NULL,
                State TEXT NOT NULL,
                StartedAtUtc TEXT NOT NULL,
                EndedAtUtc TEXT,
                FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
            );

            CREATE INDEX IF NOT EXISTS IX_Users_Username ON Users(Username);
            CREATE INDEX IF NOT EXISTS IX_Users_MachineId ON Users(MachineId);
            CREATE INDEX IF NOT EXISTS IX_Sessions_UserId_State ON Sessions(UserId, State);
            CREATE INDEX IF NOT EXISTS IX_Machines_MachineId ON Machines(MachineId);
            """;

        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }
}
