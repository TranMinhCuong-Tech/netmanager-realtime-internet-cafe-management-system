using System.Globalization;
using Microsoft.Data.Sqlite;
using ServerApp.Auth.Contracts;
using ServerApp.Auth.Models;
using ServerApp.Database.Entities;

namespace ServerApp.Database.Repositories;

public sealed class SqliteUserRepository : IUserRepository {
    private readonly NetManagerDatabase _database;

    public SqliteUserRepository(NetManagerDatabase database) {
        _database = database;
    }

    public async Task<UserRecord?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default) {
        if (string.IsNullOrWhiteSpace(username)) {
            return null;
        }

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT Id, Username, PasswordHashBase64, PasswordSaltBase64, Role, MachineId, IsActive, LastLoginAtUtc
            FROM Users
            WHERE Username = @Username
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@Username", username.Trim());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        return await ReadUserAsync(reader, cancellationToken).ConfigureAwait(false);
    }

    public async Task<UserRecord?> GetByIdAsync(string userId, CancellationToken cancellationToken = default) {
        if (string.IsNullOrWhiteSpace(userId)) {
            return null;
        }

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT Id, Username, PasswordHashBase64, PasswordSaltBase64, Role, MachineId, IsActive, LastLoginAtUtc
            FROM Users
            WHERE Id = @Id
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@Id", userId.Trim());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        return await ReadUserAsync(reader, cancellationToken).ConfigureAwait(false);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default) {
        await using var connection = _database.CreateConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM Users;";

        var result = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
        return Convert.ToInt32(result, CultureInfo.InvariantCulture);
    }

    public async Task AddAsync(UserRecord user, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(user);

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using var command = connection.CreateCommand();
        command.CommandText = """
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
        BindUser(command, user);
        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateLastLoginAtAsync(string userId, DateTimeOffset lastLoginAtUtc, CancellationToken cancellationToken = default) {
        if (string.IsNullOrWhiteSpace(userId)) {
            return;
        }

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE Users
            SET LastLoginAtUtc = @LastLoginAtUtc
            WHERE Id = @Id;
            """;
        command.Parameters.AddWithValue("@Id", userId.Trim());
        command.Parameters.AddWithValue("@LastLoginAtUtc", lastLoginAtUtc.UtcDateTime.ToString("O", CultureInfo.InvariantCulture));

        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    private static void BindUser(SqliteCommand command, UserRecord user) {
        command.Parameters.AddWithValue("@Id", user.Id);
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@PasswordHashBase64", user.PasswordHashBase64);
        command.Parameters.AddWithValue("@PasswordSaltBase64", user.PasswordSaltBase64);
        command.Parameters.AddWithValue("@Role", user.Role.ToString());
        command.Parameters.AddWithValue("@MachineId", user.MachineId);
        command.Parameters.AddWithValue("@IsActive", user.IsActive ? 1 : 0);
        command.Parameters.AddWithValue("@LastLoginAtUtc", user.LastLoginAtUtc is null
            ? DBNull.Value
            : user.LastLoginAtUtc.Value.UtcDateTime.ToString("O", CultureInfo.InvariantCulture));
    }

    private static async Task<UserRecord?> ReadUserAsync(SqliteDataReader reader, CancellationToken cancellationToken) {
        if (!await reader.ReadAsync(cancellationToken).ConfigureAwait(false)) {
            return null;
        }

        return new UserRecord {
            Id = reader.GetString(0),
            Username = reader.GetString(1),
            PasswordHashBase64 = reader.GetString(2),
            PasswordSaltBase64 = reader.GetString(3),
            Role = Enum.Parse<UserRole>(reader.GetString(4), ignoreCase: true),
            MachineId = reader.GetString(5),
            IsActive = reader.GetInt32(6) != 0,
            LastLoginAtUtc = ReadDateTimeOffset(reader, 7)
        };
    }

    private static DateTimeOffset? ReadDateTimeOffset(SqliteDataReader reader, int ordinal) {
        if (reader.IsDBNull(ordinal)) {
            return null;
        }

        var value = reader.GetString(ordinal);
        return DateTimeOffset.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
    }
}
