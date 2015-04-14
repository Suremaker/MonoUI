using System.Collections.ObjectModel;
using MonoUI.Core;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;

namespace MonoUI.Controls.Containers
{
    public enum Spacing
    {
        None,
        Minimal,
        Normal,
        Large
    }
    public class LinearContainer : Control<LinearContainer.ILinearContainerView>
    {
        public interface ILinearContainerView : IControlView, IContainerView
        {
            ObservableCollection<IControl> Items { get; }
            Property<Orientation> Orientation { get; }
            Property<Spacing> Spacing { get; }
        }

        public ObservableCollection<IControl> Items { get { return View.Items; } }
        public Property<Orientation> Orientation { get { return View.Orientation; } }
        public Property<Spacing> Spacing { get { return View.Spacing; } }
        public Property<Alignment> ContentAlignment { get { return View.ContentAlignment; } }
        public Property<StretchOptions> ContentStretch { get { return View.ContentStretch; } }
    }
}