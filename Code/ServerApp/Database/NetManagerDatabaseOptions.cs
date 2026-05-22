namespace ServerApp.Database;

public sealed record NetManagerDatabaseOptions {
    public string DatabasePath { get; }

    public string ConnectionString { get; }

    public NetManagerDatabaseOptions(string? databasePath = null) {
        DatabasePath = ResolveDatabasePath(databasePath);
        ConnectionString = $"Data Source={DatabasePath}";
    }

    private static string ResolveDatabasePath(string? databasePath) {
        var path = string.IsNullOrWhiteSpace(databasePath)
            ? Path.Combine(AppContext.BaseDirectory, "AppData", "netmanager.db")
            : databasePath.Trim();

        return Path.IsPathRooted(path)
            ? path
            : Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, path));
    }
}
