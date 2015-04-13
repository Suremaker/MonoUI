using MonoUI.Core.Observables;

namespace MonoUI.Core.Views
{
    public class ComposedView : IControlView
    {
        private readonly Property<Alignment> _alignment = Properties.Create<Alignment>();
        private Property<StretchOptions> _stretch = Properties.Create<StretchOptions>();
        private Property<IControlView> _parent = Properties.Create<IControlView>();
        public Property<Alignment> Alignment { get { return _alignment; } }
        public Property<StretchOptions> Stretch { get { return _stretch; } }
        public Property<IControlView> Parent { get { return _parent; } }

        public void Dispose()
        {
        }
    }
}
