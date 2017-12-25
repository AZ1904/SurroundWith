namespace SurroundWith
{
    using Microsoft.VisualStudio.Shell;
    using System;
    using System.ComponentModel.Design;
    using System.Runtime.InteropServices;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("8e4b8a21-4f76-46cd-a3bf-04a2c07adc38")]
    public class SurroundWithToolWindow : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SurroundWithToolWindow"/> class.
        /// </summary>
        public SurroundWithToolWindow() : base(null)
        {
            this.Caption = "SurroundWithToolWindow";
            this.ToolBar = new CommandID(new Guid(SurroundWithPackageGuids.guidSurroundWithToolWindowPackageCmdSet1), SurroundWithPackageGuids.SWToolbar);
            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new SurroundWithToolWindowControl();
        }
    }
}