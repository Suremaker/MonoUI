using MonoUI.Core;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;

namespace MonoUI.Controls
{
    public interface IWindowView : IControlView
    {
        void Show();
        Property<IControl> Content { get; }
    }

    public class Window : Control<IWindowView>
    {
        public Property<IControl> Content { get { return View.Content; } }

        public void Show()
        {
            View.Show();
        }
    }
}
