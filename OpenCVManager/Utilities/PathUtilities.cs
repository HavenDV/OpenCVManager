using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace OpenCVManager.Utilities
{
    public static class PathUtilities
    {
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

        public static List<string> SplitPathsByWhitespace(string path)
        {
            var separators = new [] { ' ', '\t' };
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
    }
}
