
namespace DXApplication2.Module.Controllers
{
    partial class HistoryController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.showHistory = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // showHistory
            // 
            this.showHistory.Caption = null;
            this.showHistory.Category = "Tools";
            this.showHistory.ConfirmationMessage = null;
            this.showHistory.Id = "0848e4fc-60b2-46e1-b739-08cfc0f23097";
            this.showHistory.ToolTip = null;
            this.showHistory.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.showHistory_Execute);
            // 
            // HistoryController
            // 
            this.Actions.Add(this.showHistory);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction showHistory;
    }
}
