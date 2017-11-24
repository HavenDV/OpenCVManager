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
        public static List<string> GetVcProjectLibs(Project project, string startsWith = null, string endsWith = null, bool withoutExtension = true)
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

        public static List<string> GetAvailableLibs(string installPath, string pattern = "*.lib")
        {
            var libPath = installPath + @"\lib\";
            var libs = Directory.EnumerateFiles(libPath, pattern).ToList();

            return libs.Where(item =>
                !item.Contains("d.lib") ||
                !libs.Contains(item.Replace("d.lib", ".lib"))).ToList();
        }
    }
}
