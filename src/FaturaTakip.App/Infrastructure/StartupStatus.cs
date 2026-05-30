using System.IO;

namespace FaturaTakip.App.Infrastructure;

public sealed record StartupStatus(
    string RootDirectory,
    string DatabasePath,
    DateTimeOffset InitializedAt,
    IReadOnlyList<DirectoryStatus> Directories,
    IReadOnlyList<string> AppliedMigrations)
{
    public bool DatabaseExists => File.Exists(DatabasePath);

    public string DatabaseState => DatabaseExists ? "Hazır" : "Eksik";

    public string MigrationState => AppliedMigrations.Count == 0
        ? "Güncel"
        : $"{AppliedMigrations.Count} migration uygulandı";
}
