using System.IO;
using FaturaTakip.App.Data;

namespace FaturaTakip.App.Infrastructure;

public sealed class ApplicationBootstrapper
{
    private readonly DatabaseInitializer _databaseInitializer = new();

    public StartupStatus Initialize()
    {
        var paths = AppPaths.Resolve();
        var directories = EnsureDirectories(paths.RequiredDirectories);
        var databaseResult = _databaseInitializer.Initialize(paths.DatabasePath);

        return new StartupStatus(
            paths.RootDirectory,
            paths.DatabasePath,
            DateTimeOffset.Now,
            directories,
            databaseResult.AppliedMigrations);
    }

    private static IReadOnlyList<DirectoryStatus> EnsureDirectories(
        IEnumerable<DirectoryDefinition> directories)
    {
        var statuses = new List<DirectoryStatus>();

        foreach (var directory in directories)
        {
            var existedBefore = Directory.Exists(directory.FullPath);
            Directory.CreateDirectory(directory.FullPath);

            statuses.Add(new DirectoryStatus(
                directory.Name,
                directory.FullPath,
                Created: !existedBefore));
        }

        return statuses;
    }
}
