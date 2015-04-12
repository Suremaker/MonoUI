using MonoUI.Core;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;

namespace MonoUI.Controls.Primitives
{
    public class Label : Control<ILabelView>
    {
        public Property<string> Text { get { return View.Text; } }
    }

    public interface ILabelView : IControlView
    {
        Property<string> Text { get; }
    }
}
