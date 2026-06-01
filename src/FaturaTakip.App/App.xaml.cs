using System;
using System.IO;
using System.Linq;
using System.Windows;
using FaturaTakip.App.Diagnostics;
using FaturaTakip.App.Infrastructure;

namespace FaturaTakip.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var isHealthCheck = IsHealthCheck(e.Args);
        var isSelfTest = IsSelfTest(e.Args);
        var isCreateBackup = IsCreateBackup(e.Args);

        try
        {
            // Command modes should run headlessly and exit. Keep them before UI init.
            if (isSelfTest)
            {
                new SelfTestRunner().Run();
                Shutdown(0);
                return;
            }

            if (isHealthCheck)
            {
                _ = new ApplicationBootstrapper().Initialize();
                Shutdown(0);
                return;
            }

            if (isCreateBackup)
            {
                var includeAttachments = !e.Args.Any(arg =>
                    string.Equals(arg, "--backup-no-attachments", StringComparison.OrdinalIgnoreCase));
                var includeExports = !e.Args.Any(arg =>
                    string.Equals(arg, "--backup-no-exports", StringComparison.OrdinalIgnoreCase));

                var result = BackupService.CreateBackup(new BackupService.BackupOptions(
                    IncludeAttachments: includeAttachments,
                    IncludeExports: includeExports));

                try
                {
                    Console.WriteLine(result.Message);
                    Console.WriteLine(result.ZipPath);
                }
                catch
                {
                    // Ignore console issues in WPF.
                }

                Shutdown(0);
                return;
            }

            var startupStatus = new ApplicationBootstrapper().Initialize();
            var window = new MainWindow(startupStatus);
            MainWindow = window;
            window.Show();
        }
        catch (Exception exception)
        {
            TryWriteStartupError(exception);

            // Never block command-mode runs on a MessageBox (headless environments).
            if (!isHealthCheck && !isSelfTest && !isCreateBackup)
            {
                MessageBox.Show(
                    exception.Message,
                    "Uygulama baslatilamadi",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            Shutdown(1);
        }
    }

    private static bool IsHealthCheck(string[] args)
    {
        return args.Any(arg => string.Equals(arg, "--health-check", StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsSelfTest(string[] args)
    {
        return args.Any(arg => string.Equals(arg, "--self-test", StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsCreateBackup(string[] args)
    {
        return args.Any(arg => string.Equals(arg, "--create-backup", StringComparison.OrdinalIgnoreCase));
    }

    private static void TryWriteStartupError(Exception exception)
    {
        try
        {
            var paths = AppPaths.Resolve();
            var logsDirectory = Path.Combine(paths.RootDirectory, "logs");
            Directory.CreateDirectory(logsDirectory);
            File.WriteAllText(Path.Combine(logsDirectory, "startup-error.log"), exception.ToString());
        }
        catch
        {
            // Startup error logging must never hide the original startup failure.
        }
    }
}

