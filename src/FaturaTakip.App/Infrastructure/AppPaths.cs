using System.IO;

namespace FaturaTakip.App.Infrastructure;

public sealed class AppPaths
{
    private AppPaths(string rootDirectory)
    {
        RootDirectory = rootDirectory;
        DatabaseDirectory = Path.Combine(RootDirectory, "database");
        DatabasePath = Path.Combine(DatabaseDirectory, "fatura_takip.db");

        RequiredDirectories =
        [
            new DirectoryDefinition("database", DatabaseDirectory),
            new DirectoryDefinition("attachments", Path.Combine(RootDirectory, "attachments")),
            new DirectoryDefinition("attachments/invoices", Path.Combine(RootDirectory, "attachments", "invoices")),
            new DirectoryDefinition("attachments/payments", Path.Combine(RootDirectory, "attachments", "payments")),
            new DirectoryDefinition("backups", Path.Combine(RootDirectory, "backups")),
            new DirectoryDefinition("logs", Path.Combine(RootDirectory, "logs")),
            new DirectoryDefinition("exports", Path.Combine(RootDirectory, "exports")),
        ];
    }

    public string RootDirectory { get; }

    public string DatabaseDirectory { get; }

    public string DatabasePath { get; }

    public IReadOnlyList<DirectoryDefinition> RequiredDirectories { get; }

    public static AppPaths Resolve()
    {
        var rootDirectory = FindProjectRoot(AppContext.BaseDirectory) ?? AppContext.BaseDirectory;
        return new AppPaths(rootDirectory);
    }

    private static string? FindProjectRoot(string startDirectory)
    {
        var directory = new DirectoryInfo(startDirectory);
        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "FaturaTakip.sln")) ||
                Directory.Exists(Path.Combine(directory.FullName, ".git")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        return null;
    }
}
