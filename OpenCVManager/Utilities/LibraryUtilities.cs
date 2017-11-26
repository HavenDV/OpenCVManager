using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace OpenCVManager.Utilities
{
    public static class LibraryUtilities
    {
        public static List<string> DebugFilter(ICollection<string> files) => files.Where(
            item =>
            {
                var extension = Path.GetExtension(item);
                var debugExtension = "d" + extension;

                return !item.Contains(debugExtension) ||
                !files.Contains(item.Replace(debugExtension, extension));
            }).ToList();

        public static List<string> FindAvailableFiles(string path, string pattern = "*.dll")
        {
            if (!Directory.Exists(path))
            {
                return new List<string>();
            }

            return DebugFilter(Directory.EnumerateFiles(path, pattern).ToList());
        }

        public static List<string> FindAvailableFiles(string path, IEnumerable<string> subfolders, string pattern = "*.*") => subfolders.
            SelectMany(subFolder => FindAvailableFiles(Path.Combine(path, subFolder), pattern)).
            ToList();
    }
}
