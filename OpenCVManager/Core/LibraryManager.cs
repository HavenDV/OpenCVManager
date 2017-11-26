using System;
using System.Collections.Generic;
using System.Linq;
using OpenCVManager.Properties;

namespace OpenCVManager.Core
{
    public static class LibraryManager
    {
        #region Properties

        #endregion

        #region Public methods

        public static List<string> GetSavedVersions() => ToVersionsList(Settings.Default.AvailableVersions);

        public static void AddNewVersion(string path) =>
            Settings.Default.AvailableVersions = Settings.Default.AvailableVersions + VersionsSeparator + path;

        public static void DeleteVersion(string path) =>
            Settings.Default.AvailableVersions = ToVersionsString(GetSavedVersions().Except(new[] { path }));

        public static void Save() => Settings.Default.Save();
        public static void Cancel() => Settings.Default.Reload();

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
