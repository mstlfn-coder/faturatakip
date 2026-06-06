using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FaturaTakip.App.Infrastructure;

public sealed record BackupHistoryItem(
    string FileName,
    string FullPath,
    DateTime LastWriteTime,
    long FileBytes)
{
    public string DisplayText =>
        $"{LastWriteTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("tr-TR"))} - {FileName}";
}

public static class BackupFileCatalog
{
    public static IReadOnlyList<BackupHistoryItem> GetRecentBackups(string rootDirectory, int take = 5)
    {
        if (string.IsNullOrWhiteSpace(rootDirectory))
        {
            throw new InvalidOperationException("Kok klasor bos olamaz.");
        }

        var backupsDir = Path.Combine(rootDirectory, "backups");
        Directory.CreateDirectory(backupsDir);

        return new DirectoryInfo(backupsDir)
            .GetFiles("backup_*.zip")
            .OrderByDescending(file => file.LastWriteTimeUtc)
            .ThenByDescending(file => file.Name, StringComparer.OrdinalIgnoreCase)
            .Take(Math.Max(1, take))
            .Select(file => new BackupHistoryItem(
                file.Name,
                file.FullName,
                file.LastWriteTime,
                file.Length))
            .ToList();
    }
}
