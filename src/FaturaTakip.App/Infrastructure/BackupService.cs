using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using FaturaTakip.App.Data;
using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Infrastructure;

public static class BackupService
{
    public sealed record BackupResult(
        DateTime CreatedAt,
        string ZipPath,
        long ZipBytes,
        string Message);

    public sealed record BackupOptions(
        bool IncludeAttachments = true,
        bool IncludeExports = true);

    public static BackupResult CreateBackup(BackupOptions options)
    {
        var paths = AppPaths.Resolve();
        Directory.CreateDirectory(Path.Combine(paths.RootDirectory, "backups"));

        var createdAt = DateTime.Now;
        var stamp = createdAt.ToString("yyyyMMdd_HHmmss");
        var tempDirName = $"backup_{stamp}";
        var tempDirPath = Path.Combine(paths.RootDirectory, "backups", tempDirName);
        Directory.CreateDirectory(tempDirPath);

        try
        {
            // 1) DB backup (safe copy via SQLite backup API)
            var dbOutDir = Path.Combine(tempDirPath, "database");
            Directory.CreateDirectory(dbOutDir);
            var dbOutPath = Path.Combine(dbOutDir, "fatura_takip.db");
            BackupSqliteDatabase(paths.DatabasePath, dbOutPath);

            // 2) Optional folders
            if (options.IncludeAttachments)
            {
                var attachmentsSrc = Path.Combine(paths.RootDirectory, "attachments");
                var attachmentsDst = Path.Combine(tempDirPath, "attachments");
                if (Directory.Exists(attachmentsSrc))
                {
                    CopyDirectory(attachmentsSrc, attachmentsDst);
                }
            }

            if (options.IncludeExports)
            {
                var exportsSrc = Path.Combine(paths.RootDirectory, "exports");
                var exportsDst = Path.Combine(tempDirPath, "exports");
                if (Directory.Exists(exportsSrc))
                {
                    CopyDirectory(exportsSrc, exportsDst);
                }
            }

            // 3) Manifest
            var manifest = new
            {
                version = 1,
                createdAt = createdAt,
                includes = new
                {
                    database = true,
                    attachments = options.IncludeAttachments,
                    exports = options.IncludeExports,
                }
            };
            File.WriteAllText(
                Path.Combine(tempDirPath, "backup.json"),
                JsonSerializer.Serialize(manifest, new JsonSerializerOptions { WriteIndented = true }));

            // 4) Zip and cleanup temp dir
            var zipFileName = $"{tempDirName}.zip";
            var zipPath = Path.Combine(paths.RootDirectory, "backups", zipFileName);
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            ZipFile.CreateFromDirectory(tempDirPath, zipPath, CompressionLevel.Optimal, includeBaseDirectory: true);
            var zipInfo = new FileInfo(zipPath);

            Directory.Delete(tempDirPath, recursive: true);

            return new BackupResult(
                CreatedAt: createdAt,
                ZipPath: zipPath,
                ZipBytes: zipInfo.Length,
                Message: $"Yedek oluşturuldu: backups/{zipFileName}");
        }
        catch
        {
            // Keep temp dir for troubleshooting if something goes wrong.
            throw;
        }
    }

    private static void BackupSqliteDatabase(string sourceDbPath, string destinationDbPath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(destinationDbPath)!);

        using var source = SqliteConnectionFactory.Create(sourceDbPath);
        using var destination = SqliteConnectionFactory.Create(destinationDbPath);

        source.Open();
        destination.Open();

        // Copies the entire DB safely even if it's being used (SQLite handles the snapshot).
        source.BackupDatabase(destination);
    }

    private static void CopyDirectory(string sourceDir, string destinationDir)
    {
        Directory.CreateDirectory(destinationDir);

        foreach (var dir in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
        {
            var rel = Path.GetRelativePath(sourceDir, dir);
            Directory.CreateDirectory(Path.Combine(destinationDir, rel));
        }

        foreach (var file in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
        {
            var rel = Path.GetRelativePath(sourceDir, file);
            var dst = Path.Combine(destinationDir, rel);
            Directory.CreateDirectory(Path.GetDirectoryName(dst)!);
            File.Copy(file, dst, overwrite: true);
        }
    }
}
