using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using Microsoft.Win32;
using FaturaTakip.App.Infrastructure;

namespace FaturaTakip.App.Views;

public partial class BackupView : UserControl
{
    private IReadOnlyList<BackupHistoryItem> _recentBackups = Array.Empty<BackupHistoryItem>();

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
            _recentBackups = BackupFileCatalog.GetRecentBackups(paths.RootDirectory, take: 5);
            RecentBackupsList.ItemsSource = _recentBackups;

            var latest = _recentBackups.FirstOrDefault();
            UpdateRecentBackupActions();

            if (latest is null)
            {
                BackupStatusText.Text = "Henüz yedek yok. Öneri: günde 1 kez yedek alın.";
                RecentBackupsStatusText.Text = "Liste bos. Ilk yedekten sonra burada son 5 zip gorunur.";
                UpdateRestoreTargetValidation();
                return;
            }

            BackupStatusText.Text = $"Son yedek: {latest.FileName} ({latest.LastWriteTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("tr-TR"))})";
            RecentBackupsStatusText.Text = $"{_recentBackups.Count} yedek listelendi.";

            // Convenience: prefill restore zip with the latest backup.
            if (string.IsNullOrWhiteSpace(RestoreZipPathText.Text))
            {
                RestoreZipPathText.Text = latest.FullPath;
            }

            if (RecentBackupsList.SelectedItem is null)
            {
                RecentBackupsList.SelectedIndex = 0;
            }

            RestoreStatusText.Text = "";
            UpdateRestoreTargetValidation();
        }
        catch (Exception ex)
        {
            BackupStatusText.Text = $"Yedek durumu okunamadı: {ex.Message}";
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
            Refresh();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Yedek oluşturulamadı:\n{ex.Message}",
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
                $"Klasör açılamadı:\n{ex.Message}",
                "Yedekleme",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void RefreshBackupsListButton_Click(object sender, RoutedEventArgs e)
    {
        Refresh();
    }

    private void RecentBackupsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateRecentBackupActions();
    }

    private void UseSelectedBackupButton_Click(object sender, RoutedEventArgs e)
    {
        if (RecentBackupsList.SelectedItem is not BackupHistoryItem selected)
        {
            RecentBackupsStatusText.Text = "Once listeden bir yedek secin.";
            return;
        }

        RestoreZipPathText.Text = selected.FullPath;
        RecentBackupsStatusText.Text = $"Restore icin secildi: {selected.FileName}";
        RestoreStatusText.Text = "";
        UpdateRestoreTargetValidation();
    }

    private void OpenSelectedBackupButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (RecentBackupsList.SelectedItem is not BackupHistoryItem selected)
            {
                RecentBackupsStatusText.Text = "Acmak icin once listeden bir yedek secin.";
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = selected.FullPath,
                UseShellExecute = true,
            });

            RecentBackupsStatusText.Text = $"Acildi: {selected.FileName}";
        }
        catch (Exception ex)
        {
            RecentBackupsStatusText.Text = "Hata: " + ex.Message;
        }
    }

    private void RevealSelectedBackupButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (RecentBackupsList.SelectedItem is not BackupHistoryItem selected)
            {
                RecentBackupsStatusText.Text = "Gostermek icin once listeden bir yedek secin.";
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"/select,\"{selected.FullPath}\"",
                UseShellExecute = true,
            });

            RecentBackupsStatusText.Text = $"Klasorde gosterildi: {selected.FileName}";
        }
        catch (Exception ex)
        {
            RecentBackupsStatusText.Text = "Hata: " + ex.Message;
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
                Title = "Yedek Zip Seç",
                Filter = "Zip (*.zip)|*.zip|Tüm Dosyalar|*.*",
                InitialDirectory = backupsDir,
                CheckFileExists = true,
                Multiselect = false,
            };

            if (dlg.ShowDialog() == true)
            {
                RestoreZipPathText.Text = dlg.FileName;
                RestoreStatusText.Text = "";
                UpdateRestoreTargetValidation();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Zip seçilemedi:\n{ex.Message}",
                "Geri Yükleme",
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
                MessageBox.Show("Lütfen hedef klasör yolunu girin.", "Geri Yükleme", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            targetRoot = Path.GetFullPath(targetRoot);
            if (!Directory.Exists(targetRoot))
            {
                MessageBox.Show("Hedef klasör bulunamadı (henüz oluşmamış olabilir).", "Geri Yükleme", MessageBoxButton.OK, MessageBoxImage.Information);
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
                $"Klasör açılamadı:\n{ex.Message}",
                "Geri Yükleme",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void SelectRestoreTargetButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var initialDirectory = AppPaths.Resolve().RootDirectory;
            var currentValue = (RestoreTargetPathText.Text ?? string.Empty).Trim();
            if (!string.IsNullOrWhiteSpace(currentValue))
            {
                var normalizedCurrentValue = Path.GetFullPath(currentValue);
                if (Directory.Exists(normalizedCurrentValue))
                {
                    initialDirectory = normalizedCurrentValue;
                }
                else
                {
                    var parent = Path.GetDirectoryName(normalizedCurrentValue);
                    if (!string.IsNullOrWhiteSpace(parent) && Directory.Exists(parent))
                    {
                        initialDirectory = parent;
                    }
                }
            }

            var dialog = new OpenFolderDialog
            {
                Title = "Bos hedef klasor sec",
                InitialDirectory = initialDirectory,
                Multiselect = false,
            };

            if (dialog.ShowDialog() == true)
            {
                RestoreTargetPathText.Text = dialog.FolderName;
                RestoreStatusText.Text = "";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Hedef klasor secilemedi:\n{ex.Message}",
                "Geri Yukleme",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void RestoreTargetPathText_TextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateRestoreTargetValidation();
    }

    private void RestoreBackupButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var zipPath = (RestoreZipPathText.Text ?? "").Trim();
            var targetRoot = (RestoreTargetPathText.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(zipPath))
            {
                MessageBox.Show("Lütfen bir yedek zip seçin.", "Geri Yükleme", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(targetRoot))
            {
                MessageBox.Show("Lütfen boş bir hedef klasör seçin (yolunu yazın).", "Geri Yükleme", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!File.Exists(zipPath))
            {
                MessageBox.Show("Yedek zip dosyası bulunamadı.", "Geri Yükleme", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var assessment = BackupRestoreService.EvaluateTargetRoot(targetRoot);
            if (!assessment.CanRestore)
            {
                RestoreStatusText.Text = "Hata: " + assessment.Message;
                MessageBox.Show(assessment.Message, "Geri Yükleme", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            targetRoot = assessment.TargetRoot!;

            var confirm = MessageBox.Show(
                "Seçilen zip boş bir klasöre geri yüklenecek.\n\n" +
                $"Zip: {zipPath}\n" +
                $"Hedef: {targetRoot}\n\n" +
                "Not: Bu işlem hedef klasör boş değilse çalışmaz.\n\n" +
                "Devam edilsin mi?",
                "Geri Yükleme Onayı",
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
                "Geri Yükleme",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            RestoreStatusText.Text = "Hata: " + ex.Message;
            MessageBox.Show(
                $"Geri yükleme başarısız:\n{ex.Message}",
                "Geri Yükleme",
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

    private void UpdateRecentBackupActions()
    {
        var hasSelection = RecentBackupsList.SelectedItem is BackupHistoryItem;

        if (UseSelectedBackupButton is not null)
        {
            UseSelectedBackupButton.IsEnabled = hasSelection;
        }

        if (OpenSelectedBackupButton is not null)
        {
            OpenSelectedBackupButton.IsEnabled = hasSelection;
        }

        if (RevealSelectedBackupButton is not null)
        {
            RevealSelectedBackupButton.IsEnabled = hasSelection;
        }
    }

    private void UpdateRestoreTargetValidation()
    {
        if (RestoreTargetValidationText is null)
        {
            return;
        }

        var assessment = BackupRestoreService.EvaluateTargetRoot(RestoreTargetPathText.Text);
        RestoreTargetValidationText.Text = assessment.Message;
        RestoreTargetValidationText.Foreground = assessment.CanRestore
            ? System.Windows.Media.Brushes.DarkGreen
            : System.Windows.Media.Brushes.IndianRed;

        if (RestoreBackupButton is not null)
        {
            RestoreBackupButton.IsEnabled =
                assessment.CanRestore &&
                !string.IsNullOrWhiteSpace(RestoreZipPathText.Text);
        }
    }
}

