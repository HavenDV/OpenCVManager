using System;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace OpenCVManager.Utilities
{
    public static class SolutionUtilities
    {
        public static Project GetSelectedProject()
        {
            var monitorSelection = (IVsMonitorSelection)Package.GetGlobalService(typeof(SVsShellMonitorSelection));
            monitorSelection.GetCurrentSelection(out IntPtr hierarchyPointer, out var projectItemId,
                out IVsMultiItemSelect _, out IntPtr _);

            var selectedHierarchy = Marshal.GetTypedObjectForIUnknown(hierarchyPointer, typeof(IVsHierarchy)) as IVsHierarchy;
            if (selectedHierarchy == null)
            {
                return null;
            }

            ErrorHandler.ThrowOnFailure(selectedHierarchy.GetProperty(
                projectItemId,
                (int)__VSHPROPID.VSHPROPID_ExtObject,
                out var selectedObject));

            return selectedObject as Project;
        }
    }
}
