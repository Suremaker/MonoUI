using MonoUI.Core.Observables;

namespace MonoUI.Core.Views
{
    public class ComposedView : IControlView
    {
        private readonly Property<Alignment> _alignment = Properties.Create<Alignment>();
        public Property<Alignment> Alignment { get { return _alignment; } }
        public Property<IControlView> Parent { get; private set; }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
