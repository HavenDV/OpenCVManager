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

        public string LibraryPath { get; }
        public List<string> AvailableLibraries { get; }
        public List<string> AvailableDlls { get; }
        public List<string> AvailableModules { get; }
        public string VersionShort { get; } = "Unknown";
        public bool? Is64Bit { get; }
        public string MachineType => Is64Bit.HasValue ? Is64Bit.Value ? "x64" : "x86" : "Unknown";
        public string Version { get; } = "Unknown";

        public IEnumerable<string> LibrariesSubFolders { get; } = new List<string>
        {
            Path.Combine("install", "x64", "vc15", "lib"),
            Path.Combine("install", "x86", "vc15", "lib"),
            Path.Combine("x64", "vc15", "lib"),
            Path.Combine("x86", "vc15", "lib"),
            "lib", ""
        };

        public IEnumerable<string> DllSubFolders { get; } = new List<string>
        {
            Path.Combine("install", "x64", "vc15", "bin"),
            Path.Combine("install", "x86", "vc15", "bin"),
            Path.Combine("x64", "vc15", "bin"),
            Path.Combine("x86", "vc15", "bin"),
            "bin", ""
        };

        #endregion

        #region Constructor

        public Library(string path)
        {
            LibraryPath = path;
            AvailableLibraries = LibraryUtilities.FindAvailableLibraries(path, LibrariesSubFolders);
            AvailableDlls = LibraryUtilities.FindAvailableDlls(path, DllSubFolders);
            if (!AvailableLibraries.Any() )
            {
                return;
            }

            AvailableModules = AvailableLibraries.Select(GetName).ToList();
            VersionShort = FindVersion(AvailableLibraries);
            Version = ToFullVersion(VersionShort);

            if (!AvailableDlls.Any())
            {
                return;
            }
            Is64Bit = FindIs64Bit(AvailableDlls);
        }

        #endregion

        #region Static methods

        public static string ToFullVersion(string shortVersion) =>
            $"{shortVersion[0]}.{shortVersion.Substring(1, shortVersion.Length - 2)}.{shortVersion[shortVersion.Length - 1]}";

        public static string FindVersion(ICollection<string> libraries)
        {
            libraries = libraries ?? throw new ArgumentNullException(nameof(libraries));
            libraries = libraries.Any() ? libraries : throw new ArgumentException(@"Array is empty", nameof(libraries));

            return GetVersion(libraries.FirstOrDefault());
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
