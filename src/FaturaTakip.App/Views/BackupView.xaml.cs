using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
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
                BackupStatusText.Text = "HenÃƒÂ¼z yedek yok. Ãƒâ€“neri: gÃƒÂ¼nde 1 kez yedek alÃ„Â±n.";
                return;
            }

            BackupStatusText.Text = $"Son yedek: {latest.Name} ({latest.LastWriteTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("tr-TR"))})";

            // Convenience: prefill restore zip with the latest backup.
            if (string.IsNullOrWhiteSpace(RestoreZipPathText.Text))
            {
                RestoreZipPathText.Text = latest.FullName;
            }
            RestoreStatusText.Text = "";
        }
        catch (Exception ex)
        {
            BackupStatusText.Text = $"Yedek durumu okunamadÃ„Â±: {ex.Message}";
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
                $"Yedek oluÃ…Å¸turulamadÃ„Â±:\n{ex.Message}",
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
                $"KlasÃƒÂ¶r aÃƒÂ§Ã„Â±lamadÃ„Â±:\n{ex.Message}",
                "Yedekleme",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }


    private void SelectRestoreZipButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var paths = AppPaths.Resolve();
            var backupsDir = Path.Combine(paths.RootDirectory, "backups");
            Directory.CreateDirectory(backupsDir);

            var dlg = new OpenFileDialog
            {
                Title = "Yedek Zip SeÃ§",
                Filter = "Zip (*.zip)|*.zip|TÃ¼m Dosyalar|*.*",
                InitialDirectory = backupsDir,
                CheckFileExists = true,
                Multiselect = false,
            };

            if (dlg.ShowDialog() == true)
            {
                RestoreZipPathText.Text = dlg.FileName;
                RestoreStatusText.Text = "";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Zip seÃ§ilemedi:\n{ex.Message}",
                "Geri YÃ¼kleme",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void OpenRestoreTargetButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var targetRoot = (RestoreTargetPathText.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(targetRoot))
            {
                MessageBox.Show("LÃ¼tfen hedef klasÃ¶r yolunu girin.", "Geri YÃ¼kleme", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            targetRoot = Path.GetFullPath(targetRoot);
            if (!Directory.Exists(targetRoot))
            {
                MessageBox.Show("Hedef klasÃ¶r bulunamadÄ± (henÃ¼z oluÅŸmamÄ±ÅŸ olabilir).", "Geri YÃ¼kleme", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = targetRoot,
                UseShellExecute = true,
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"KlasÃ¶r aÃ§Ä±lamadÄ±:\n{ex.Message}",
                "Geri YÃ¼kleme",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void RestoreBackupButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var zipPath = (RestoreZipPathText.Text ?? "").Trim();
            var targetRoot = (RestoreTargetPathText.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(zipPath))
            {
                MessageBox.Show("LÃ¼tfen bir yedek zip seÃ§in.", "Geri YÃ¼kleme", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(targetRoot))
            {
                MessageBox.Show("LÃ¼tfen boÅŸ bir hedef klasÃ¶r seÃ§in (yolunu yazÄ±n).", "Geri YÃ¼kleme", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!File.Exists(zipPath))
            {
                MessageBox.Show("Yedek zip dosyasÄ± bulunamadÄ±.", "Geri YÃ¼kleme", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            targetRoot = Path.GetFullPath(targetRoot);

            if (Directory.Exists(targetRoot) && Directory.EnumerateFileSystemEntries(targetRoot).Any())
            {
                RestoreStatusText.Text = "Hata: hedef klasÃ¶r boÅŸ deÄŸil.";
                MessageBox.Show("GÃ¼venlik iÃ§in sadece boÅŸ klasÃ¶re geri yÃ¼kleme yapÄ±labilir.", "Geri YÃ¼kleme", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "SeÃ§ilen zip boÅŸ bir klasÃ¶re geri yÃ¼klenecek.\n\n" +
                $"Zip: {zipPath}\n" +
                $"Hedef: {targetRoot}\n\n" +
                "Not: Bu iÅŸlem hedef klasÃ¶r boÅŸ deÄŸilse Ã§alÄ±ÅŸmaz.\n\n" +
                "Devam edilsin mi?",
                "Geri YÃ¼kleme OnayÄ±",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes)
            {
                return;
            }

            var result = BackupRestoreService.RestoreToEmptyRoot(zipPath, targetRoot);
            RestoreStatusText.Text = result.Message;

            MessageBox.Show(
                result.Message,
                "Geri YÃ¼kleme",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            RestoreStatusText.Text = "Hata: " + ex.Message;
            MessageBox.Show(
                $"Geri yÃ¼kleme baÅŸarÄ±sÄ±z:\n{ex.Message}",
                "Geri YÃ¼kleme",
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

