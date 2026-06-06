using System;
using System.IO;
using System.Linq;
using System.IO.Compression;

namespace FaturaTakip.App.Infrastructure;

public static class BackupRestoreService
{
    public sealed record RestoreResult(
        string ZipPath,
        string TargetRoot,
        string Message);

    public sealed record RestoreTargetAssessment(
        string? TargetRoot,
        bool Exists,
        bool IsEmpty,
        bool CanRestore,
        string Message);

    public static RestoreTargetAssessment EvaluateTargetRoot(string? targetRoot)
    {
        if (string.IsNullOrWhiteSpace(targetRoot))
        {
            return new RestoreTargetAssessment(null, Exists: false, IsEmpty: false, CanRestore: false, Message: "Hedef klasor secilmedi.");
        }

        try
        {
            var normalizedTargetRoot = Path.GetFullPath(targetRoot);
            if (!Directory.Exists(normalizedTargetRoot))
            {
                return new RestoreTargetAssessment(normalizedTargetRoot, Exists: false, IsEmpty: true, CanRestore: true, Message: "Klasor henuz yok. Geri yukleme sirasinda olusturulacak.");
            }

            var isEmpty = !Directory.EnumerateFileSystemEntries(normalizedTargetRoot).Any();
            if (isEmpty)
            {
                return new RestoreTargetAssessment(normalizedTargetRoot, Exists: true, IsEmpty: true, CanRestore: true, Message: "Klasor bos. Geri yukleme icin uygun.");
            }

            return new RestoreTargetAssessment(normalizedTargetRoot, Exists: true, IsEmpty: false, CanRestore: false, Message: "Klasor bos degil. Guvenlik icin kullanilamaz.");
        }
        catch
        {
            return new RestoreTargetAssessment(null, Exists: false, IsEmpty: false, CanRestore: false, Message: "Hedef klasor yolu gecersiz.");
        }
    }

    public static RestoreResult RestoreToEmptyRoot(string zipPath, string targetRoot)
    {
        if (string.IsNullOrWhiteSpace(zipPath))
        {
            throw new InvalidOperationException("Yedek zip yolu boş olamaz.");
        }

        if (!File.Exists(zipPath))
        {
            throw new FileNotFoundException("Yedek zip dosyası bulunamadı.", zipPath);
        }

        if (string.IsNullOrWhiteSpace(targetRoot))
        {
            throw new InvalidOperationException("Hedef klasör boş olamaz.");
        }

        var assessment = EvaluateTargetRoot(targetRoot);
        if (!assessment.CanRestore)
        {
            throw new InvalidOperationException(assessment.Message);
        }

        targetRoot = assessment.TargetRoot!;
        targetRoot = Path.GetFullPath(targetRoot);

        if (Directory.Exists(targetRoot) &&
            Directory.EnumerateFileSystemEntries(targetRoot).Any())
        {
            throw new InvalidOperationException("Güvenlik için sadece boş bir klasöre geri yükleme yapılabilir.");
        }

        Directory.CreateDirectory(targetRoot);

        var tempDir = Path.Combine(Path.GetTempPath(), "FaturaTakip.Restore", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            ZipFile.ExtractToDirectory(zipPath, tempDir, overwriteFiles: true);

            var baseDir = new DirectoryInfo(tempDir).GetDirectories()
                .OrderByDescending(d => d.LastWriteTimeUtc)
                .FirstOrDefault();

            if (baseDir is null)
            {
                throw new InvalidOperationException("Yedek zip içeriği boş görünüyor.");
            }

            // Validate minimal structure
            var dbPath = Path.Combine(baseDir.FullName, "database", "fatura_takip.db");
            if (!File.Exists(dbPath))
            {
                throw new InvalidOperationException("Yedek içinde veritabanı bulunamadı: database/fatura_takip.db");
            }

            CopyDirectoryIfExists(Path.Combine(baseDir.FullName, "database"), Path.Combine(targetRoot, "database"));
            CopyDirectoryIfExists(Path.Combine(baseDir.FullName, "attachments"), Path.Combine(targetRoot, "attachments"));
            CopyDirectoryIfExists(Path.Combine(baseDir.FullName, "exports"), Path.Combine(targetRoot, "exports"));

            var message = "Yedek geri yüklendi (boş klasöre): " + targetRoot;
            return new RestoreResult(zipPath, targetRoot, message);
        }
        finally
        {
            try
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, recursive: true);
                }
            }
            catch
            {
            }
        }
    }

    private static void CopyDirectoryIfExists(string sourceDir, string destinationDir)
    {
        if (!Directory.Exists(sourceDir))
        {
            return;
        }

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
