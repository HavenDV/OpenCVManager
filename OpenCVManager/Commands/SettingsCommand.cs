using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using OpenCVManager.Extensions;
using OpenCVManager.Forms;
using OpenCVManager.Utilities;

namespace OpenCVManager.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class SettingsCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("d653f5fd-8004-4f1c-8d17-cf892d0bbe7c");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package _package;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private SettingsCommand(Package package)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));

            if (!(ServiceProvider.GetService(typeof(IMenuCommandService)) is OleMenuCommandService commandService))
            {
                return;
            }

            var menuCommandId = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(MenuItemCallback, menuCommandId);
            menuItem.BeforeQueryStatus += OnBeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        private static void OnBeforeQueryStatus(object sender, EventArgs e)
        {
            if (!(sender is OleMenuCommand command))
            {
                return;
            }

            command.Visible = SolutionUtilities.GetSelectedProject()?.IsVcProject() ?? false;
        }


        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static SettingsCommand Instance {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider => _package;

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new SettingsCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            //var message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", GetType().FullName);
            //var title = "SettingsCommand";

            // Show a message box to prove we were here
            //VsShellUtilities.ShowMessageBox(ServiceProvider, message, title,
            //   OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            
            using (var form = new SettingsForm(SolutionUtilities.GetSelectedProject()))
            {
                form.ShowDialog();
            }
        }
    }
}
