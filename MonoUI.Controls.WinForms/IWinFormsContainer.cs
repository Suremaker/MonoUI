using MonoUI.Core.Views;
using MonoUI.Layouts;

namespace MonoUI.Controls.WinForms
{
    public interface IWinFormsContainer : IContainerView
    {
        void LayoutChildren();
        void RecalculateSize();
        ILayoutPreferences ContentLayoutPreferences { get; }
    }
}