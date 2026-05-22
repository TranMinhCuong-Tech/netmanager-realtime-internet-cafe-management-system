using System.Globalization;
using Microsoft.Data.Sqlite;
using ServerApp.Auth.Contracts;
using ServerApp.Auth.Models;
using ServerApp.Database.Entities;

namespace ServerApp.Database.Repositories;

public sealed class SqliteSessionRepository : ISessionRepository {
    private readonly NetManagerDatabase _database;

    public SqliteSessionRepository(NetManagerDatabase database) {
        _database = database;
    }

    public async Task AddAsync(SessionRecord session, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(session);

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO Sessions
            (
                Id,
                UserId,
                Username,
                Role,
                MachineId,
                State,
                StartedAtUtc,
                EndedAtUtc
            )
            VALUES
            (
                @Id,
                @UserId,
                @Username,
                @Role,
                @MachineId,
                @State,
                @StartedAtUtc,
                @EndedAtUtc
            );
            """;
        BindSession(command, session);
        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<SessionRecord?> GetActiveByUserIdAsync(string userId, CancellationToken cancellationToken = default) {
        if (string.IsNullOrWhiteSpace(userId)) {
            return null;
        }

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT Id, UserId, Username, Role, MachineId, State, StartedAtUtc, EndedAtUtc
            FROM Sessions
            WHERE UserId = @UserId AND State = @State
            ORDER BY StartedAtUtc DESC
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@UserId", userId.Trim());
        command.Parameters.AddWithValue("@State", SessionState.Active.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        return await ReadSessionAsync(reader, cancellationToken).ConfigureAwait(false);
    }

    public async Task<SessionRecord?> GetByIdAsync(string sessionId, CancellationToken cancellationToken = default) {
        if (string.IsNullOrWhiteSpace(sessionId)) {
            return null;
        }

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT Id, UserId, Username, Role, MachineId, State, StartedAtUtc, EndedAtUtc
            FROM Sessions
            WHERE Id = @Id
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@Id", sessionId.Trim());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        return await ReadSessionAsync(reader, cancellationToken).ConfigureAwait(false);
    }

    public async Task RevokeActiveSessionsByUserIdAsync(string userId, CancellationToken cancellationToken = default) {
        if (string.IsNullOrWhiteSpace(userId)) {
            return;
        }

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE Sessions
            SET State = @State,
                EndedAtUtc = @EndedAtUtc
            WHERE UserId = @UserId AND State = @ActiveState;
            """;
        command.Parameters.AddWithValue("@UserId", userId.Trim());
        command.Parameters.AddWithValue("@State", SessionState.Revoked.ToString());
        command.Parameters.AddWithValue("@ActiveState", SessionState.Active.ToString());
        command.Parameters.AddWithValue("@EndedAtUtc", DateTimeOffset.UtcNow.UtcDateTime.ToString("O", CultureInfo.InvariantCulture));

        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateStateAsync(string sessionId, SessionState state, DateTimeOffset endedAtUtc, CancellationToken cancellationToken = default) {
        if (string.IsNullOrWhiteSpace(sessionId)) {
            return;
        }

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE Sessions
            SET State = @State,
                EndedAtUtc = @EndedAtUtc
            WHERE Id = @Id;
            """;
        command.Parameters.AddWithValue("@Id", sessionId.Trim());
        command.Parameters.AddWithValue("@State", state.ToString());
        command.Parameters.AddWithValue("@EndedAtUtc", endedAtUtc.UtcDateTime.ToString("O", CultureInfo.InvariantCulture));

        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    private static void BindSession(SqliteCommand command, SessionRecord session) {
        command.Parameters.AddWithValue("@Id", session.Id);
        command.Parameters.AddWithValue("@UserId", session.UserId);
        command.Parameters.AddWithValue("@Username", session.Username);
        command.Parameters.AddWithValue("@Role", session.Role.ToString());
        command.Parameters.AddWithValue("@MachineId", session.MachineId);
        command.Parameters.AddWithValue("@State", session.State.ToString());
        command.Parameters.AddWithValue("@StartedAtUtc", session.StartedAtUtc.UtcDateTime.ToString("O", CultureInfo.InvariantCulture));
        command.Parameters.AddWithValue("@EndedAtUtc", session.EndedAtUtc is null
            ? DBNull.Value
            : session.EndedAtUtc.Value.UtcDateTime.ToString("O", CultureInfo.InvariantCulture));
    }

    private static async Task<SessionRecord?> ReadSessionAsync(SqliteDataReader reader, CancellationToken cancellationToken) {
        if (!await reader.ReadAsync(cancellationToken).ConfigureAwait(false)) {
            return null;
        }

        return new SessionRecord {
            Id = reader.GetString(0),
            UserId = reader.GetString(1),
            Username = reader.GetString(2),
            Role = Enum.Parse<UserRole>(reader.GetString(3), ignoreCase: true),
            MachineId = reader.GetString(4),
            State = Enum.Parse<SessionState>(reader.GetString(5), ignoreCase: true),
            StartedAtUtc = DateTimeOffset.Parse(reader.GetString(6), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
            EndedAtUtc = ReadNullableDateTimeOffset(reader, 7)
        };
    }

    private static DateTimeOffset? ReadNullableDateTimeOffset(SqliteDataReader reader, int ordinal) {
        if (reader.IsDBNull(ordinal)) {
            return null;
        }

        return DateTimeOffset.Parse(reader.GetString(ordinal), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
    }
}
