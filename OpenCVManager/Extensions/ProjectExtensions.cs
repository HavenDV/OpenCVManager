﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.VCProjectEngine;

namespace OpenCVManager.Extensions
{
    public static class ProjectExtensions
    {
        public static VCConfiguration GetConfiguration(this Project project, string configurationName, string platformName)
        {
            if (!(project.Object is VCProject vcProject))
            {
                throw new ArgumentException(@"Project is not C++ project", nameof(project));
            }

            foreach (VCConfiguration configuration in (IVCCollection)vcProject.Configurations)
            {
                if (!string.Equals(configuration.ConfigurationName, configurationName,
                        StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(((VCPlatform)configuration.Platform).Name, platformName))
                {
                    continue;
                }

                return configuration;
            }

            throw new ArgumentException(@"Configuration not found", nameof(configurationName));
        }

        public static T GetTool<T>(this Project project, string configurationName, string platformName, string toolName)
            where T : class =>
            ((IVCCollection)project.GetConfiguration(configurationName, platformName).Tools).
            Item(toolName) as T;

        public static VCLinkerTool GetLinkerTool(this Project project, string configurationName, string platformName) =>
            project.GetTool<VCLinkerTool>(configurationName, platformName, "VCLinkerTool");

        public static VCCLCompilerTool GetCompilerTool(this Project project, string configurationName, string platformName) =>
            project.GetTool<VCCLCompilerTool>(configurationName, platformName, "VCCLCompilerTool");

        public static void AddProjectDependencies(this Project project, string configurationName, string platformName,
            params string[] dependencies) =>
            project.GetLinkerTool(configurationName, platformName).AddAdditionalDependencies(dependencies);

        public static void DeleteProjectDependencies(this Project project, string configurationName, string platformName,
            params string[] dependencies) =>
            project.GetLinkerTool(configurationName, platformName).DeleteAdditionalDependencies(dependencies);

        public static void AddProjectLibraryDirectories(this Project project, string configurationName, string platformName,
            params string[] directories) =>
            project.GetLinkerTool(configurationName, platformName).AddAdditionalLibraryDirectories(directories);

        public static void DeleteProjectLibraryDirectories(this Project project, string configurationName, string platformName,
            params string[] directories) =>
            project.GetLinkerTool(configurationName, platformName).DeleteAdditionalLibraryDirectories(directories);

        public static void AddProjectHeadersDirectories(this Project project, string configurationName, string platformName,
            params string[] directories) =>
            project.GetCompilerTool(configurationName, platformName).AddAdditionalHeadersDirectories(directories);

        public static void DeleteProjectHeadersDirectories(this Project project, string configurationName, string platformName,
            params string[] directories) =>
            project.GetCompilerTool(configurationName, platformName).DeleteAdditionalHeadersDirectories(directories);

        public static List<string> GetProjectLibraries(this Project project, string startsWith = null, string endsWith = null, bool withoutExtension = false)
        {
            if (!(project.Object is VCProject vcProject))
            {
                throw new ArgumentException(@"Project is not C++ project", nameof(project));
            }

            var libs = new List<string>();
            foreach (VCConfiguration config in (IVCCollection)vcProject.Configurations)
            {
                //var compiler = CompilerToolWrapper.Create(config);
                if (((IVCCollection)config.Tools).Item("VCLinkerTool") is VCLinkerTool linker)
                {
                    libs.AddRange(linker.GetAdditionalDependencies());
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

            return libs.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }

        public static void WrireGlobalVariable(this Project project, string name, object value)
        {
            var isEmpty = value == null || value is string stringValue && string.IsNullOrWhiteSpace(stringValue);

            project.Globals.VariablePersists[name] = !isEmpty;
            project.Globals[name] = isEmpty ? null : value;
        }

        public static T GetGlobalVariable<T>(this Project project, string name) => (T)project.Globals[name];

        public static bool TryGetGlobalVariable<T>(this Project project, string name, out T value)
        {
            var isExists = project.Globals.VariableExists[name];
            value = isExists ? project.GetGlobalVariable<T>(name) : default(T);

            return isExists;
        }

    }
}
