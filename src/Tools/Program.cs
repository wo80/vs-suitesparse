using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace suitesparse
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify a task to run: mgmt clean|update [args]");

                return;
            }

            string task = args[0].ToLowerInvariant();

            if (task.Equals("clean") || task.Equals("c"))
            {
                Cleaner.Run(args.Slice(1));
            }
            else if (task.Equals("update") || task.Equals("u"))
            {
                await Updater.Run(args.Slice(1));
            }
            else
            {
                Console.WriteLine("Unknown task: " + task);
            }
        }
    }

    #region Update

    class Updater
    {
        const string URL = "https://raw.githubusercontent.com/DrTimothyAldenDavis/SuiteSparse/dev/SuiteSparse_config/CMakeLists.txt";
        const string DL = "https://github.com/DrTimothyAldenDavis/SuiteSparse/archive/refs/heads/master.zip";
        const string RX_MAJOR = "SUITESPARSE_VERSION_MAJOR\\s+([\\.\\d]+)";
        const string RX_MINOR = "SUITESPARSE_VERSION_MINOR\\s+([\\.\\d]+)";
        const string RX_SUB = "SUITESPARSE_VERSION_SUB\\s+([\\.\\d]+)";

        public static async Task Run(string[] args)
        {
            Console.WriteLine("Running update task ...");

            var task = new Updater();

            if (args.IsNullOrEmpty())
            {
                args = new string[] { "--check" };
            }

            foreach (var item in args)
            {
                if (item.Equals("--check", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Installed version: " + task.GetInstalledVersion());
                    Console.WriteLine("Latest version: " + await task.GetLatestVersion());
                }
                else if (item.Equals("--download", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Latest version: " + await task.GetLatestVersion());

                    await task.DownloadLatestVersion();
                }
            }
        }

        string installedVersion;
        string latestVersion;
        string downloadUrl;

        string GetInstalledVersion()
        {
            const string needle = "SuiteSparse VERSION";

            int i = 0;

            installedVersion = "n/a";

            string root = Helper.GetRootDirectory("SuiteSparse");
            string path = Path.Combine(root, "README.md");

            if (File.Exists(path))
            {
                foreach (var line in File.ReadLines(path))
                {
                    int index = line.IndexOf(needle);

                    if (index >= 0)
                    {
                        installedVersion = line.Substring(index + needle.Length).Trim();
                    }

                    if (i++ > 5) break;
                }
            }

            return installedVersion;
        }

        async Task<string> GetLatestVersion()
        {
            latestVersion = "n/a";

            using (var client = new HttpClient())
            {
                string page = await client.GetStringAsync(URL);

                var m1 = Regex.Match(page, RX_MAJOR);
                var m2 = Regex.Match(page, RX_MINOR);
                var m3 = Regex.Match(page, RX_SUB);

                if (m1.Success && m2.Success && m3.Success)
                {
                    latestVersion = m1.Groups[1].Value + "." + m2.Groups[1].Value + "." + m3.Groups[1].Value;
                    downloadUrl = DL.Replace("{version}", latestVersion);
                }
            }

            return latestVersion;
        }

        async Task DownloadLatestVersion()
        {
            if (string.IsNullOrEmpty(downloadUrl))
            {
                return;
            }

            var uri = new Uri(downloadUrl);

            string root = Helper.GetRootDirectory();
            string path = Path.Combine(root, Path.GetFileName(uri.LocalPath));

            IProgress<float> progress = new Progress<float>(p =>
            {
                if (p < 1f)
                {
                    Console.Write("\rDownloading ... {0:0.0}%", p * 100f);
                }
                else
                {
                     Console.WriteLine("\rDownloading ... Done!");
                }
            });

            using (var client = new HttpClient())
            {
                //client.Timeout = TimeSpan.FromMinutes(5);

                // Create a file stream to store the downloaded data.
                using (var file = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)) {

                    // Use the custom extension method below to download the data.
                    await client.DownloadAsync(uri, file, progress, CancellationToken.None);
                }
            }
        }
    }

    #endregion

    #region Cleanup

    class Cleaner
    {
        static string[] directories = new string[]
        {
            ".github",
            "bin",
            "GPUQREngine",
            "GraphBLAS",
            "include",
            "lib",
            "MATLAB_Tools",
            "Mongoose",
            "RBio",
            "SPEX",
            "ssget",
            "SuiteSparse_GPURuntime"
        };
        static string[] files = new string[]
        {
            ".gitattributes",
            ".gitignore",
            "Contents.m",
            "CSparse_to_CXSparse",
            "Makefile",
            "SuiteSparse_demo.m",
            "SuiteSparse_install.m",
            "SuiteSparse_paths.m",
            "SuiteSparse_test.m"
        };

        public static void Run(string[] args)
        {
            Console.WriteLine("Running cleanup task ...");

            var task = new Cleaner();

            if (args.IsNullOrEmpty())
            {
                args = new string[] { "--default" };
            }

            foreach (var item in args)
            {
                if (item.Equals("--all", StringComparison.OrdinalIgnoreCase))
                {
                    task.CleanupDefault();
                    task.CleanupDocs();
                    task.CleanupTests();
                    task.CleanupDocs();
                    task.CleanupMatlab();
                    task.CleanupBuild();
                }
                else if (item.Equals("--default", StringComparison.OrdinalIgnoreCase))
                {
                    task.CleanupDefault();
                }
                else if (item.Equals("--docs", StringComparison.OrdinalIgnoreCase))
                {
                    task.CleanupDocs();
                }
                else if (item.Equals("--tests", StringComparison.OrdinalIgnoreCase))
                {
                    task.CleanupTests();
                }
                else if (item.Equals("--matlab", StringComparison.OrdinalIgnoreCase))
                {
                    task.CleanupMatlab();
                }
                else if (item.Equals("--build", StringComparison.OrdinalIgnoreCase))
                {
                    task.CleanupBuild();
                }
            }
        }

        void CleanupDefault()
        {
            Console.WriteLine("Running clean -default ...");

            string root;

            if (Helper.TryGetRootDirectory(out root, "SuiteSparse"))
            {
                DeleteDirectories(root, directories);
                DeleteFiles(root, files);
            }
        }

        void CleanupDocs()
        {
            Console.WriteLine("Running clean -docs ...");

            string root;

            if (Helper.TryGetRootDirectory(out root, "SuiteSparse"))
            {
                DeleteDirectories(Directory.EnumerateDirectories(root), new string[] { "Doc" });
            }
        }

        void CleanupTests()
        {
            Console.WriteLine("Running clean -tests ...");

            string root;

            if (Helper.TryGetRootDirectory(out root, "SuiteSparse"))
            {
                DeleteDirectories(Directory.EnumerateDirectories(root), new string[] { "Tcov" });
            }
        }

        void CleanupMatlab()
        {
            Console.WriteLine("Running clean -matlab ...");

            string root;

            if (Helper.TryGetRootDirectory(out root, "SuiteSparse"))
            {
                DeleteDirectories(Directory.EnumerateDirectories(root), new string[] { "MATLAB" });
            }
        }

        void CleanupBuild()
        {
            Console.WriteLine("Running clean -build ...");
            
            string root;

            if (Helper.TryGetRootDirectory(out root, "Visual Studio"))
            {
                var subDirectories = new string[] { "Release", "Debug", "x64" };

                DeleteDirectories(Directory.EnumerateDirectories(Path.Combine(root, "shared")), subDirectories);
                DeleteDirectories(Directory.EnumerateDirectories(Path.Combine(root, "static")), subDirectories);
            }
        }

        private void DeleteFiles(string baseDirectory, IEnumerable<string> files)
        {
            foreach (var item in files)
            {
                var path = Path.Combine(baseDirectory, item);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        void DeleteDirectories(string baseDirectory, IEnumerable<string> subDirectories)
        {
            foreach (var item in subDirectories)
            {
                var path = Path.Combine(baseDirectory, item);

                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        void DeleteDirectories(IEnumerable<string> baseDirectories, IEnumerable<string> subDirectories)
        {
            foreach (var dir in baseDirectories)
            {
                foreach (var item in subDirectories)
                {
                    var path = Path.Combine(dir, item);

                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                }
            }
        }
    }

    #endregion

    #region Extensions

    static class ExtensionMethods
    {
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return (array == null || array.Length == 0);
        }

        public static T[] Slice<T>(this T[] array, int index)
        {
            if (array == null || array.Length < index)
            {
                return Array.Empty<T>();
            }

            return array.Skip(index).ToArray();
        }
    }

    #endregion

    #region Helper

    static class Helper
    {
        public static string GetRootDirectory(string subDirectory = null)
        {
            // By default, expect the program to be run from "src/Tools"
            string root = Path.GetFullPath("..");

            if (subDirectory != null)
            {
                root = Path.Combine(root, subDirectory);
            }

            return root;
        }

        public static bool TryGetRootDirectory(out string root, string subDirectory = null)
        {
            root = GetRootDirectory(subDirectory);

            if (!Directory.Exists(root))
            {
                var color = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Couldn't find directory \"{0}\"", root);
                Console.ForegroundColor = color;

                return false;
            }

            return true;
        }
    }

    #endregion

    #region Extension methods

    // https://stackoverflow.com/questions/20661652/progress-bar-with-httpclient

    static class Extensions
    {
        public static async Task DownloadAsync(this HttpClient client, Uri requestUri, Stream destination, IProgress<float> progress = null, CancellationToken cancellationToken = default)
        {
            // Get the http headers first to examine the content length
            using (var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead))
            {
                var contentLength = response.Content.Headers.ContentLength;

                using (var download = await response.Content.ReadAsStreamAsync())
                {

                    // Ignore progress reporting when no progress reporter was 
                    // passed or when the content length is unknown
                    if (progress == null || !contentLength.HasValue)
                    {
                        await download.CopyToAsync(destination);
                        return;
                    }

                    // Convert absolute progress (bytes downloaded) into relative progress (0% - 100%)
                    var relativeProgress = new Progress<long>(totalBytes => progress.Report((float)totalBytes / contentLength.Value));
                    // Use extension method to report progress while downloading
                    await download.CopyToAsync(destination, 81920, relativeProgress, cancellationToken);
                    progress.Report(1);
                }
            }
        }

        public static async Task CopyToAsync(this Stream source, Stream destination, int bufferSize, IProgress<long> progress = null, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (!source.CanRead)
                throw new ArgumentException("Has to be readable", nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (!destination.CanWrite)
                throw new ArgumentException("Has to be writable", nameof(destination));
            if (bufferSize < 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            var buffer = new byte[bufferSize];
            long totalBytesRead = 0;
            int bytesRead;
            while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
                totalBytesRead += bytesRead;
                progress?.Report(totalBytesRead);
            }
        }
    }

    #endregion
}
