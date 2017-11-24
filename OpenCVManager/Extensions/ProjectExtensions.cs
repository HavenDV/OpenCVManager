using EnvDTE;

namespace OpenCVManager.Extensions
{
    public static class ProjectExtensions
    {
        public static void WrireGlobalVariable(this Project project, string name, object value)
        {
            project.Globals.VariablePersists[name] = true;
            project.Globals[name] = value;
        }

        public static bool GetGlobalVariable<T>(this Project project, string name, out T value)
        {
            var isExists = project.Globals.VariableExists[name];
            value = isExists ? (T)project.Globals[name] : default(T);

            return isExists;
        }
            
    }
}
