using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.VCProjectEngine;

namespace OpenCVManager.Extensions
{
    public static class VcLinkerToolExtensions
    {
        public static List<string> GetAdditionalLibraryDirectories(this VCLinkerTool tool) =>
            SplitPaths(tool.AdditionalLibraryDirectories, ';', ',');

        public static void SetAdditionalLibraryDirectories(this VCLinkerTool tool, List<string> value)
        {
            var newDirectories = JoinPatches(value, ";");
            if (!string.Equals(newDirectories, tool.AdditionalLibraryDirectories,
                StringComparison.OrdinalIgnoreCase))
            {
                tool.AdditionalLibraryDirectories = newDirectories;
            }
        }

        public static void AddAdditionalLibraryDirectories(this VCLinkerTool tool, params string[] values) =>
            tool.SetAdditionalLibraryDirectories(tool.
                GetAdditionalLibraryDirectories().
                Concat(values).
                ToList());

        public static void DeleteAdditionalLibraryDirectories(this VCLinkerTool tool, params string[] values) =>
            tool.SetAdditionalLibraryDirectories(tool.
                GetAdditionalLibraryDirectories().
                Except(values, StringComparer.OrdinalIgnoreCase).
                ToList());


        public static List<string> GetAdditionalDependencies(this VCLinkerTool tool) =>
            SplitPathsByWhitespace(tool.AdditionalDependencies);

        public static void SetAdditionalDependencies(this VCLinkerTool tool, List<string> value)
        {
            var newDependencies = JoinPatches(value, " ");
            if (!string.Equals(newDependencies, tool.AdditionalDependencies,
                StringComparison.OrdinalIgnoreCase))
            {
                tool.AdditionalDependencies = newDependencies;
            }
        }

        public static void AddAdditionalDependencies(this VCLinkerTool tool,  params string[] values) =>
            tool.SetAdditionalDependencies(tool.
                GetAdditionalDependencies().
                Concat(values).
                ToList());

        public static void DeleteAdditionalDependencies(this VCLinkerTool tool, params string[] values) =>
            tool.SetAdditionalDependencies(tool.
                GetAdditionalDependencies().
                Except(values, StringComparer.OrdinalIgnoreCase).
                ToList());

        #region Utilities

        public static string JoinPatches(IEnumerable<string> paths, string separator)
        {
            if (paths == null)
            {
                return null;
            }

            return string.Join(separator, paths.Select(item =>
                !Path.IsPathRooted(item) || item.IndexOfAny(new[] { ' ', '\t' }) > 0 ? $"\"{item}\"" : item));
        }

        public static List<string> SplitPaths(string path, params char[] separators) => path?.
            Split(separators, StringSplitOptions.RemoveEmptyEntries).
            Select(directory => directory.Trim('\"')).
            ToList();

        private static List<string> SplitPathsByWhitespace(string path)
        {
            var separators = new char[] { ' ', '\t' };
            if (path.IndexOf('"') < 0)
            {
                return SplitPaths(path, separators);
            }

            var paths = new List<string>();
            var startIndex = 0;
            foreach (Match match in new Regex(@"""[^""]*""").Matches(path))
            {
                var item = match.Value.Trim('\"');

                // Add all items before this match, using standard splitting.
                paths.AddRange(SplitPaths(path.Substring(startIndex, match.Index - startIndex), separators));

                startIndex = match.Index + match.Length;

                if (item.Length == 0)
                {
                    continue;
                }

                paths.Add(item);
            }

            if (startIndex < path.Length - 1)
            {
                // Add all items after the quoted match, using standard splitting.
                paths.AddRange(SplitPaths(path.Substring(startIndex), separators));
            }

            return paths;
        }

        #endregion

    }
}
