using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.VCProjectEngine;

namespace OpenCVManager.Utilities
{
    public static class LibraryUtilities
    {
        public static List<string> GetVcProjectLibraries(Project project, string startsWith = null, string endsWith = null, bool withoutExtension = true)
        {
            var libs = new List<string>();

            var vcProject = project.Object as VCProject;
            if (vcProject == null)
            {
                throw new ArgumentException(@"Project is not C++ project", nameof(project));
            }

            foreach (VCConfiguration config in (IVCCollection)vcProject.Configurations)
            {
                //var compiler = CompilerToolWrapper.Create(config);
                if (((IVCCollection)config.Tools).Item("VCLinkerTool") is VCLinkerTool linker)
                {
                    var linkerWrapper = new LinkerToolWrapper(linker);
                    var additionalDeps = linkerWrapper.AdditionalDependencies;
                    libs.AddRange(additionalDeps);
                }
            }

            if (withoutExtension)
            {
                libs = libs.Select(Path.GetFileNameWithoutExtension).ToList();
            }
            if (!string.IsNullOrWhiteSpace(startsWith))
            {
                libs = libs.Where(lib => lib.StartsWith(startsWith)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(endsWith))
            {
                libs = libs.Where(lib => lib.EndsWith(endsWith)).ToList();
            }

            return libs;
        }

        public static List<string> FindAvailableLibraries(string librariesPath, string pattern = "*.lib")
        {
            if (!Directory.Exists(librariesPath))
            {
                return new List<string>();
            }

            var libraries = Directory.EnumerateFiles(librariesPath, pattern).ToList();

            return libraries.Where(item =>
                !item.Contains("d.lib") ||
                !libraries.Contains(item.Replace("d.lib", ".lib"))).ToList();
        }

        public static List<string> FindAvailableDlls(string librariesPath, string pattern = "*.dll")
        {
            if (!Directory.Exists(librariesPath))
            {
                return new List<string>();
            }

            return Directory.EnumerateFiles(librariesPath, pattern).ToList();
        }

        public static List<string> FindAvailableLibraries(string path, IEnumerable<string> subfolders, string pattern = "*.lib") => subfolders.
            SelectMany(subFolder => FindAvailableLibraries(Path.Combine(path, subFolder), pattern)).
            ToList();

        public static List<string> FindAvailableDlls(string path, IEnumerable<string> subfolders, string pattern = "*.dll") => subfolders.
            SelectMany(subFolder => FindAvailableDlls(Path.Combine(path, subFolder), pattern)).
            ToList();
    }
}
