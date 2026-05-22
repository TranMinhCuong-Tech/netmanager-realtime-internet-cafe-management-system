using Microsoft.Data.Sqlite;

namespace ServerApp.Database;

public sealed class DatabaseManager {
    private readonly NetManagerDatabaseOptions _options;

    public DatabaseManager() : this(null) {
    }

    public DatabaseManager(string? databasePath) {
        _options = new NetManagerDatabaseOptions(databasePath);
    }

    public SqliteConnection GetConnection() => new(_options.ConnectionString);
}
