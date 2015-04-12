using MonoUI.Core;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;

namespace MonoUI.Controls.Primitives
{
    public class Label : Control<Label.ILabelView>
    {
        public interface ILabelView : IControlView
        {
            Property<string> Text { get; }
        }

        public Property<string> Text { get { return View.Text; } }
    }
}
