using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace suitesparse
{
    class Program
    {
        static void Main(string[] args)
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
                Updater.Run(args.Slice(1));
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
        const string URL = "http://faculty.cse.tamu.edu/davis/suitesparse.html";
        const string RX = "href=\"([^\"]*)\"><span class=\"style_1\">SuiteSparse</span><span> v([\\.\\d]+)</span></a>";

        public static void Run(string[] args)
        {
            Console.WriteLine("Running update task ...");

            var task = new Updater();

            if (args.IsNullOrEmpty())
            {
                args = new string[] { "-check" };
            }

            foreach (var item in args)
            {
                if (item.Equals("-check", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Installed version: " + task.GetInstalledVersion());
                    Console.WriteLine("Latest version: " + task.GetLatestVersion());
                }
                else if (item.Equals("-download", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Latest version: " + task.GetLatestVersion());

                    task.DownloadLatestVersion();

                    while (!task.DownloadCompleted)
                    {
                        // Block main thread while download is in progress.
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        string installedVersion;
        string latestVersion;
        string downloadUrl;

        private volatile bool downloadCompleted;

        public bool DownloadCompleted { get { return downloadCompleted; } }

        string GetInstalledVersion()
        {
            const string needle = "SuiteSparse VERSION";

            int i = 0;

            installedVersion = "n/a";

            string root = Helper.GetRootDirectory("SuiteSparse");
            string path = Path.Combine(root, "README.txt");

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

        string GetLatestVersion()
        {
            latestVersion = "n/a";

            using (var client = new WebClient())
            {
                string page = client.DownloadString(URL);

                var match = Regex.Match(page, RX);

                if (match.Success)
                {
                    downloadUrl = match.Groups[1].Value;
                    latestVersion = match.Groups[2].Value;
                }
            }

            return latestVersion;
        }

        void DownloadLatestVersion()
        {
            if (string.IsNullOrEmpty(downloadUrl))
            {
                return;
            }

            var uri = new Uri(downloadUrl);

            string root = Helper.GetRootDirectory();
            string path = Path.Combine(root, Path.GetFileName(uri.LocalPath));

            using (var client = new WebClient())
            {
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompletedCallback);
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);

                client.DownloadFileAsync(uri, path);
            }
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\rDownloading ... {0}%", e.ProgressPercentage);
        }

        private void DownloadCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("\rDownloading ... Done!");

            downloadCompleted = true;
        }
    }

    #endregion

    #region Cleanup

    class Cleaner
    {
        static string[] directories = new string[]
        {
            "bin",
            "CXSparse_newfiles",
            "GraphBLAS",
            "GPUQREngine",
            "include",
            "lib",
            "MATLAB_Tools",
            "Mongoose",
            "RBio",
            "SuiteSparse_GPURuntime",
            "share",
            "ssget"
        };
        static string[] files = new string[]
        {
            "Contents.m",
            "CSparse_to_CXSparse",
            "Makefile",
            "SuiteSparse_demo.m",
            "SuiteSparse_install.m",
            "SuiteSparse_test.m"
        };

        public static void Run(string[] args)
        {
            Console.WriteLine("Running cleanup task ...");

            var task = new Cleaner();

            if (args.IsNullOrEmpty())
            {
                args = new string[] { "-default" };
            }

            foreach (var item in args)
            {
                if (item.Equals("-all", StringComparison.OrdinalIgnoreCase))
                {
                    task.CleanupDefault();
                    task.CleanupDocs();
                    task.CleanupTests();
                    task.CleanupDocs();
                    task.CleanupMatlab();
                    task.CleanupBuild();
                }
                else if (item.Equals("-default", StringComparison.OrdinalIgnoreCase))
                {
                    task.CleanupDefault();
                }
                else if (item.Equals("-docs", StringComparison.OrdinalIgnoreCase))
                {
                    task.CleanupDocs();
                }
                else if (item.Equals("-tests", StringComparison.OrdinalIgnoreCase))
                {
                    task.CleanupTests();
                }
                else if (item.Equals("-matlab", StringComparison.OrdinalIgnoreCase))
                {
                    task.CleanupMatlab();
                }
                else if (item.Equals("-build", StringComparison.OrdinalIgnoreCase))
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
}
