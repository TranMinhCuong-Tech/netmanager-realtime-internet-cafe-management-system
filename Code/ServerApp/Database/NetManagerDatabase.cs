using Microsoft.Data.Sqlite;

namespace ServerApp.Database;

public sealed class NetManagerDatabase {
    public NetManagerDatabaseOptions Options { get; }

    public NetManagerDatabase(NetManagerDatabaseOptions options) {
        Options = options;
    }

    public SqliteConnection CreateConnection() => new(Options.ConnectionString);

    public async Task InitializeAsync(CancellationToken cancellationToken = default) {
        EnsureDatabaseDirectory();

        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        var initializer = new DatabaseInitializer();
        await initializer.InitializeAsync(connection, cancellationToken).ConfigureAwait(false);

        var seeder = new SeedData();
        await seeder.SeedAsync(connection, cancellationToken).ConfigureAwait(false);
    }

    private void EnsureDatabaseDirectory() {
        var directory = Path.GetDirectoryName(Options.DatabasePath);
        if (!string.IsNullOrWhiteSpace(directory)) {
            Directory.CreateDirectory(directory);
        }
    }
}
