using System;
using System.Collections.Generic;
using System.IO;
using OpenCVManager.Utilities;
using System.Linq;

namespace OpenCVManager.Core
{
    public class Library
    {
        #region Properties

        public bool IsAvailable { get; }
        public string Error { get; } = string.Empty;

        public string LibraryPath { get; }
        public List<string> AvailableModules { get; } = new List<string>();

        public string HeadersPath { get; }
        public string LibrariesPath { get; }
        public string DllsPath { get; }
        public string ExesPath { get; }

        public List<string> AvailableHeaders { get; }
        public List<string> AvailableLibraries { get; }
        public List<string> AvailableDlls { get; }
        public List<string> AvailableExes { get; }

        public string Version { get; } = "Unknown";
        public string VersionShort { get; } = "Unknown";

        public bool? Is64Bit { get; }
        public string MachineType => Is64Bit.HasValue ? Is64Bit.Value ? "x64" : "x86" : "Unknown";

        #region Subfolders
        
        public IEnumerable<string> LibrariesSubFolders { get; } = new []
        {
            Path.Combine("install", "x64", "vc15", "lib"),
            Path.Combine("install", "x86", "vc15", "lib"),
            Path.Combine("x64", "vc15", "lib"),
            Path.Combine("x86", "vc15", "lib"),
            Path.Combine("install", "x64", "vc14", "lib"),
            Path.Combine("install", "x86", "vc14", "lib"),
            Path.Combine("x64", "vc14", "lib"),
            Path.Combine("x86", "vc14", "lib"),
            "lib", ""
        };

        public IEnumerable<string> DllSubFolders { get; } = new []
        {
            Path.Combine("install", "x64", "vc15", "bin"),
            Path.Combine("install", "x86", "vc15", "bin"),
            Path.Combine("x64", "vc15", "bin"),
            Path.Combine("x86", "vc15", "bin"),
            Path.Combine("install", "x64", "vc14", "bin"),
            Path.Combine("install", "x86", "vc14", "bin"),
            Path.Combine("x64", "vc14", "bin"),
            Path.Combine("x86", "vc14", "bin"),
            "bin", ""
        };

        public IEnumerable<string> IncludeSubFolders { get; } = new[]
        {
            Path.Combine("install", "include", "opencv2"),
            Path.Combine("include", "opencv2"),
            "opencv2", ""
        };

        #endregion
        
        #endregion

        #region Constructor

        public Library(string path)
        {
            if (string.Equals("$(Default)", path, StringComparison.OrdinalIgnoreCase))
            {
                path = LibraryManager.DefaultX64;
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                Error = $"Argument is null or empty: {nameof(path)}";
                return;
            }

            if (!Directory.Exists(path))
            {
                Error = $"Directory is not exists: {path}";
                return;
            }

            LibraryPath = path;

            AvailableLibraries = LibraryUtilities.FindAvailableFiles(path, LibrariesSubFolders, "*.lib");
            if (!AvailableLibraries.Any())
            {
                Error = "Libraries are not found";
                return;
            }

            VersionShort = FindVersion(AvailableLibraries);
            AvailableLibraries = LibraryUtilities.FindAvailableFiles(path, LibrariesSubFolders, $"*{VersionShort}.lib");
            if (!AvailableLibraries.Any())
            {
                Error = "Libraries are not found";
                return;
            }

            LibrariesPath = Path.GetDirectoryName(AvailableLibraries.First());
            Version = ToFullVersion(VersionShort);
            AvailableModules = AvailableLibraries.Select(GetName).ToList();
            if (!AvailableModules.Any())
            {
                Error = "Modules are not found";
                return;
            }

            AvailableDlls = LibraryUtilities.FindAvailableFiles(path, DllSubFolders, $"*{VersionShort}.dll");
            if (!AvailableDlls.Any())
            {
                Error = "Dlls are not found";
                return;
            }
            Is64Bit = FindIs64Bit(AvailableDlls);
            DllsPath = Path.GetDirectoryName(AvailableDlls.First());

            AvailableHeaders = LibraryUtilities.FindAvailableFiles(path, IncludeSubFolders, "*.hpp");
            if (!AvailableHeaders.Any())
            {
                Error = "Headers are not found";
                return;
            }
            HeadersPath = Path.GetDirectoryName(Path.GetDirectoryName(AvailableHeaders.First()));

            IsAvailable = true;
            AvailableExes = LibraryUtilities.FindAvailableFiles(path, DllSubFolders, "*.exe");
            if (!AvailableExes.Any())
            {
                return;
            }

            ExesPath = Path.GetDirectoryName(AvailableExes.First());
        }

        #endregion

        #region Static methods

        public static string ToFullVersion(string shortVersion) =>
            $"{shortVersion[0]}.{shortVersion[1]}.{shortVersion.Substring(2, shortVersion.Length - 2)}";

        public static string FindVersion(ICollection<string> libraries)
        {
            libraries = libraries ?? throw new ArgumentNullException(nameof(libraries));
            libraries = libraries.Any() ? libraries : throw new ArgumentException(@"Array is empty", nameof(libraries));

            return GetVersion(libraries.
                FirstOrDefault(library => Path.GetFileName(library)?.StartsWith("opencv_core") ?? false));
        }

        public static bool FindIs64Bit(ICollection<string> dlls)
        {
            dlls = dlls ?? throw new ArgumentNullException(nameof(dlls));
            dlls = dlls.Any() ? dlls : throw new ArgumentException(@"Array is empty", nameof(dlls));

            return BinaryUtilities.Is64Bit(dlls.FirstOrDefault());
        }

        public static string GetName(string libPath)
        {
            libPath = !string.IsNullOrWhiteSpace(libPath) ? libPath : throw new ArgumentNullException(nameof(libPath));

            var name = Path.GetFileNameWithoutExtension(libPath).Replace("opencv_", "");
            var version = GetVersion(libPath);
            if (!string.IsNullOrWhiteSpace(version))
            {
                name = name.Replace(version, "");
            }

            return name;
        }

        public static string GetFileName(string name, string version, string extension) => $"opencv_{name}{version}{extension}";

        public static string GetVersion(string libPath)
        {
            libPath = !string.IsNullOrWhiteSpace(libPath) ? libPath : throw new ArgumentNullException(nameof(libPath));

            var name = Path.GetFileNameWithoutExtension(libPath);
            if (name == null)
            {
                throw new ArgumentException(@"Incorrect lib path", nameof(libPath));
            }

            var index = name.Length - 1;
            var character = name[index];
            while (char.IsDigit(character))
            {
                index--;
                character = name[index];
            }

            return index < name.Length - 1 ? name.Substring(index + 1) : string.Empty;
        }

        #endregion
    }
}
