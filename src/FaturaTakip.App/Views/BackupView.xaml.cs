using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using FaturaTakip.App.Infrastructure;

namespace FaturaTakip.App.Views;

public partial class BackupView : UserControl
{
    public BackupView()
    {
        InitializeComponent();
        Refresh();
    }

    public void Refresh()
    {
        try
        {
            var paths = AppPaths.Resolve();
            var backupsDir = Path.Combine(paths.RootDirectory, "backups");
            Directory.CreateDirectory(backupsDir);

            var latest = new DirectoryInfo(backupsDir)
                .GetFiles("backup_*.zip")
                .OrderByDescending(f => f.LastWriteTimeUtc)
                .FirstOrDefault();

            if (latest is null)
            {
                BackupStatusText.Text = "HenÃ¼z yedek yok. Ã–neri: gÃ¼nde 1 kez yedek alÄ±n.";
                return;
            }

            BackupStatusText.Text = $"Son yedek: {latest.Name} ({latest.LastWriteTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("tr-TR"))})";
        }
        catch (Exception ex)
        {
            BackupStatusText.Text = $"Yedek durumu okunamadÄ±: {ex.Message}";
        }
    }

    private void CreateBackupButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var options = new BackupService.BackupOptions(
                IncludeAttachments: IncludeAttachmentsCheck.IsChecked == true,
                IncludeExports: IncludeExportsCheck.IsChecked == true);

            var result = BackupService.CreateBackup(options);
            BackupStatusText.Text = $"{result.Message} ({FormatBytes(result.ZipBytes)})";
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Yedek oluÅŸturulamadÄ±:\n{ex.Message}",
                "Yedekleme",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void OpenBackupsFolderButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var paths = AppPaths.Resolve();
            var backupsDir = Path.Combine(paths.RootDirectory, "backups");
            Directory.CreateDirectory(backupsDir);

            Process.Start(new ProcessStartInfo
            {
                FileName = backupsDir,
                UseShellExecute = true,
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"KlasÃ¶r aÃ§Ä±lamadÄ±:\n{ex.Message}",
                "Yedekleme",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private static string FormatBytes(long bytes)
    {
        if (bytes < 1024) return $"{bytes} B";
        var kb = bytes / 1024d;
        if (kb < 1024) return $"{kb:N1} KB";
        var mb = kb / 1024d;
        if (mb < 1024) return $"{mb:N1} MB";
        var gb = mb / 1024d;
        return $"{gb:N2} GB";
    }
}

