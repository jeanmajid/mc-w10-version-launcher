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
            string URLPath= Path.Combine(VersionGetterPath, "urls.json");

            ProcessStartInfo StartInfo = new()
            {
                FileName = "python",
                Arguments = PythonFilePath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = VersionGetterPath
            };

            // add some error handling maybe
            Process Process = new() { StartInfo = StartInfo };


            Process.Start();

            Task<string> outputTask = Process.StandardOutput.ReadToEndAsync();
            Task<string> errorTask = Process.StandardError.ReadToEndAsync();

            await Process.WaitForExitAsync();

            if (Process.ExitCode != 0)
            {
                MessageBox.Show($"The version downloader failed \nSTDOUT: {await outputTask}\nSTDERR: {await errorTask}");
            }

            return URLPath;
        }
    }
}
