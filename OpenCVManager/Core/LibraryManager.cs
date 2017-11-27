using System;
using System.Collections.Generic;
using System.Linq;
using OpenCVManager.Properties;

namespace OpenCVManager.Core
{
    public static class LibraryManager
    {
        #region Properties

        public static string DefaultX86 {
            get => Settings.Default.Default_x86;
            set => Settings.Default.Default_x86 = value;
        }

        public static string DefaultX64 {
            get => Settings.Default.Default_x64;
            set => Settings.Default.Default_x64 = value;
        }

        public static List<string> SavedVersions {
            get => ToVersionsList(Settings.Default.AvailableVersions);
            set => Settings.Default.AvailableVersions = ToVersionsString(value);
        }

        #endregion

        #region Public methods

        public static void AddNewVersion(string path) =>
            Settings.Default.AvailableVersions = Settings.Default.AvailableVersions + VersionsSeparator + path;

        public static void DeleteVersion(string path) =>
            Settings.Default.AvailableVersions = ToVersionsString(SavedVersions.Except(new[] { path }));

        public static void Save() => Settings.Default.Save();
        public static void Cancel() => Settings.Default.Reload();

        #endregion

        #region Utilities

        public static char VersionsSeparator { get; } = ';';

        public static List<string> ToVersionsList(string versionsString) => !string.IsNullOrWhiteSpace(versionsString)
            ? versionsString.
            Split(VersionsSeparator).
            Distinct(StringComparer.OrdinalIgnoreCase).
            ToList() : new List<string>();

        public static string ToVersionsString(IEnumerable<string> versionsList) =>
            string.Join(VersionsSeparator.ToString(), versionsList);

        #endregion
    }
}
