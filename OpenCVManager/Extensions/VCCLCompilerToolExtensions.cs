using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.VCProjectEngine;
using OpenCVManager.Utilities;

namespace OpenCVManager.Extensions
{
    public static class VcclCompilerToolExtensions
    {
        public static List<string> GetAdditionalHeadersDirectories(this VCCLCompilerTool tool) =>
            PathUtilities.SplitPaths(tool.AdditionalIncludeDirectories, ';', ',');

        public static void SetAdditionalHeadersDirectories(this VCCLCompilerTool tool, List<string> value)
        {
            var newDirectories = PathUtilities.JoinPatches(value, ";");
            if (!string.Equals(newDirectories, tool.AdditionalIncludeDirectories,
                StringComparison.OrdinalIgnoreCase))
            {
                tool.AdditionalIncludeDirectories = newDirectories;
            }
        }

        public static void AddAdditionalHeadersDirectories(this VCCLCompilerTool tool, params string[] values) =>
            tool.SetAdditionalHeadersDirectories(tool.
                GetAdditionalHeadersDirectories().
                Concat(values).
                ToList());

        public static void DeleteAdditionalHeadersDirectories(this VCCLCompilerTool tool, params string[] values) =>
            tool.SetAdditionalHeadersDirectories(tool.
                GetAdditionalHeadersDirectories().
                Except(values, StringComparer.OrdinalIgnoreCase).
                ToList());
    }
}
