using System.IO;
using System.Windows;
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

        try
        {
            var startupStatus = new ApplicationBootstrapper().Initialize();
            if (IsHealthCheck(e.Args))
            {
                Shutdown(0);
                return;
            }

            var window = new MainWindow(startupStatus);
            MainWindow = window;
            window.Show();
        }
        catch (Exception exception)
        {
            TryWriteStartupError(exception);

            if (!IsHealthCheck(e.Args))
            {
                MessageBox.Show(
                    exception.Message,
                    "Uygulama başlatılamadı",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            Shutdown(1);
        }
    }

    private static bool IsHealthCheck(IEnumerable<string> args)
    {
        return args.Any(arg => string.Equals(arg, "--health-check", StringComparison.OrdinalIgnoreCase));
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
