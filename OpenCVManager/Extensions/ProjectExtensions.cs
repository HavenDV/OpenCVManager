using EnvDTE;

namespace OpenCVManager.Extensions
{
    public static class ProjectExtensions
    {
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
