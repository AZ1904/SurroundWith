using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;

namespace SurroundWith.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CurlyBracesCommand
    {
        //following two comes from the guidCurlyBracesCommandPackageCmdSet GuidSymbol in the vsct file
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("9b8861f8-b5d0-4ed5-843d-01e712f925fe");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package _package;

        private readonly ToggleCommands _toggle;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurlyBracesCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private CurlyBracesCommand(Package package, ToggleCommands toggleCommands)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));

            if (!(ServiceProvider.GetService(typeof(IMenuCommandService)) is OleMenuCommandService commandService))
                return;

            _toggle = toggleCommands;

            var menuCommandId = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(MenuItemCallback, menuCommandId);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CurlyBracesCommand Instance
        {
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
        public static void Initialize(Package package, ToggleCommands toggleCommands) => Instance = new CurlyBracesCommand(package, toggleCommands);

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            //https://msdn.microsoft.com/en-us/library/ms228776.aspx
            //https://docs.microsoft.com/en-us/dotnet/api/envdte.textselection?redirectedfrom=MSDN&view=visualstudiosdk-2017
            var dte = (DTE2)ServiceProvider.GetService(typeof(DTE));

            if (dte.ActiveDocument == null) return;

            var selection = (TextSelection)dte.ActiveDocument.Selection;
            string text = selection.Text;

            if (!string.IsNullOrEmpty(text))
            {
                selection.Text = _toggle.ToggleCurlyBraces(text);
                //selection.SmartFormat();

                //selection.GotoLine(selection.TopPoint.Line);
                ////selection.NewLine(1);
                //selection.Insert($"{{{Environment.NewLine}", (int)vsInsertFlags.vsInsertFlagsInsertAtStart);
                //selection.GotoLine(selection.BottomPoint.Line);
                ////selection.NewLine(1);
                //selection.Insert($"{Environment.NewLine}}}", (int)vsInsertFlags.vsInsertFlagsInsertAtEnd);
            }
            else
            {
                //todo: show something in the status-bar
            }
        }
    }
}