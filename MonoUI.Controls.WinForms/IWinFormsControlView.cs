using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;

namespace MonoUI.Controls.WinForms
{
    public interface IWinFormsControlView : IControlView
    {
        IEnumerable<Control> GetControls();
        IWinFormsContainer Parent { get; set; }
        ReadOnlyProperty<Size> PreferredSize { get; }
        Property<Rectangle> ActualBounds { get; }
        void Invalidate();
    }
}