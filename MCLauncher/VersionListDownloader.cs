using System.Diagnostics;
using System.IO;
using System.Windows;

namespace MCLauncher
{
    public class VersionListDownloader
    {
        static public async Task<string> GetVersionList()
        {
            string VersionGetterPath = Path.Combine(AppContext.BaseDirectory, "VersionGetter");
            string PythonFilePath = Path.Combine(VersionGetterPath, "main.py");
            string URLPath= Path.Combine(VersionGetterPath, "urls.min.json");

            ProcessStartInfo StartInfo = new()
            {
                FileName = "python",
                Arguments = PythonFilePath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            // add some error handling maybe
            Process Process = new() { StartInfo = StartInfo };


            Process.Start();

            Task<string> outputTask = Process.StandardOutput.ReadToEndAsync();
            Task<string> errorTask = Process.StandardError.ReadToEndAsync();

            await Process.WaitForExitAsync();

            // Need to do account login shenanigans, oh no

            if (Process.ExitCode != 0)
            {
                MessageBox.Show($"The version downloader failed: {await errorTask}");
            }

            return URLPath;
        }
    }
}
