using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.VCProjectEngine;
using OpenCVManager.Utilities;

namespace OpenCVManager.Extensions
{
    public static class VcLinkerToolExtensions
    {
        public static List<string> GetAdditionalLibraryDirectories(this VCLinkerTool tool) =>
            PathUtilities.SplitPaths(tool.AdditionalLibraryDirectories, ';', ',');

        public static void SetAdditionalLibraryDirectories(this VCLinkerTool tool, List<string> value)
        {
            var newDirectories = PathUtilities.JoinPatches(value, ";");
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
            PathUtilities.SplitPathsByWhitespace(tool.AdditionalDependencies);

        public static void SetAdditionalDependencies(this VCLinkerTool tool, List<string> value)
        {
            var newDependencies = PathUtilities.JoinPatches(value, " ");
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

    }
}
