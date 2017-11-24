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
        public static List<string> GetVcProjectLibs(Project project)
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
