using System.Drawing;
using System.Windows.Forms;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;
using MonoUI.Layouts;

namespace MonoUI.Controls.WinForms
{
    public interface IWinFormsControlView : IControlView
    {
        Control Control { get; }
        IWinFormsContainer Parent { get; }
        void SetParent(IWinFormsContainer parent, Control parentControl);
        void UnsetParent(IWinFormsContainer parent);
        ReadOnlyProperty<Size> PreferredSize { get; }
        Property<Rectangle> ActualBounds { get; }
        ILayoutPreferences LayoutPreferences { get; }
    }
}