using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_CleanupFileAndNamespaces
{
    public partial class PlugIn1 : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            registerCleanupFileAndNamespaces();
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion
        public void registerCleanupFileAndNamespaces()
        {
            DevExpress.CodeRush.Core.Action CleanupFileAndNamespaces = new DevExpress.CodeRush.Core.Action(components);
            ((System.ComponentModel.ISupportInitialize)(CleanupFileAndNamespaces)).BeginInit();
            CleanupFileAndNamespaces.ActionName = "CleanupFileAndNamespaces";
            CleanupFileAndNamespaces.ButtonText = "CleanupFile And Namespaces"; // Used if button is placed on a menu.
            CleanupFileAndNamespaces.RegisterInCR = true;
            CleanupFileAndNamespaces.Execute += CleanupFileAndNamespaces_Execute;
            ((System.ComponentModel.ISupportInitialize)(CleanupFileAndNamespaces)).EndInit();
        }
        private void CleanupFileAndNamespaces_Execute(ExecuteEventArgs ea)
        {
            // Store Address
            var Address = NavigationServices.GetCurrentAddress();

            // Find first NamespaceReference
            ElementEnumerable Enumerable = 
                new ElementEnumerable(CodeRush.Source.ActiveFileNode, 
                                      LanguageElementType.NamespaceReference, true);
            NamespaceReference Reference = Enumerable.OfType<NamespaceReference>().FirstOrDefault();

            if (Reference == null)
            {
                // No Namespace References to sort.
                return;
            }
            // Move caret       
            CodeRush.Caret.MoveTo(Reference.Range.Start);

            // Invoke Refactoring.
            RefactoringProviderBase Refactoring = CodeRush.Refactoring.Get("Optimize Namespace References");
            
            CodeRush.SmartTags.UpdateContext();
            if (Refactoring.IsAvailable)
            {
                Refactoring.Execute();
            }

            // Cleanup the rest of the file.            
            DevExpress.CodeRush.Core.Action CleanupAction = CodeRush.Actions.Get("CleanupFile");
            CleanupAction.DoExecute();

            // Restore Location
            NavigationServices.ResolveAddress(Address).Show();
        }
    }
}