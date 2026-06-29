using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace FaturaTakip.App.Views;

public sealed class AccessibleSummaryBorder : Border
{
    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return new AccessibleSummaryBorderAutomationPeer(this);
    }

    private sealed class AccessibleSummaryBorderAutomationPeer(AccessibleSummaryBorder owner)
        : FrameworkElementAutomationPeer(owner)
    {
        protected override string GetClassNameCore()
        {
            return nameof(AccessibleSummaryBorder);
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Group;
        }

        protected override List<AutomationPeer>? GetChildrenCore()
        {
            return null;
        }
    }
}
