using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_CleanupFileAndNamespaces
{
    [Export(typeof(IVsixPluginExtension))]
    public class CR_CleanupFileAndNamespacesExtension : IVsixPluginExtension { }
}