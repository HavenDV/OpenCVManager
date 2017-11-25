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
        public IEnumerable<string> SubFoldersForFind { get; } = new List<string>
        {
            Path.Combine("install", "x64", "vc15", "lib"),
            Path.Combine("install", "x86", "vc15", "lib"),
            Path.Combine("x64", "vc15", "lib"),
            Path.Combine("x86", "vc15", "lib"),
            "lib", ""
        };

        #endregion

        #region Constructor

        public Library(string path)
        {
            LibraryPath = path;
        }

        #endregion

        #region Public methods

        public string GetVersion() => GetVersion(FindAvailableLibraries().FirstOrDefault() ?? "Unknown");

        public List<string> FindAvailableLibraries() => SubFoldersForFind.
            SelectMany(subFolder => LibraryUtilities.GetAvailableLibraries(Path.Combine(LibraryPath, subFolder))).
            Select(GetName).
            ToList();

        #endregion

        #region Static methods

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
