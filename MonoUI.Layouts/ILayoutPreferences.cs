using System.Drawing;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;

namespace MonoUI.Layouts
{
    public interface ILayoutPreferences
    {
        Alignment Alignment { get; }
        StretchOptions Stretch { get; }
        Size PreferredSize { get; }
    }

    public class LayoutPreferences : ILayoutPreferences
    {
        private readonly ReadOnlyProperty<Size> _preferredSize;
        private readonly ReadOnlyProperty<Alignment> _alignment;
        private readonly ReadOnlyProperty<StretchOptions> _stretch;

        public LayoutPreferences(ReadOnlyProperty<Size> preferredSize, ReadOnlyProperty<Alignment> alignment, ReadOnlyProperty<StretchOptions> stretch)
        {
            _preferredSize = preferredSize;
            _alignment = alignment;
            _stretch = stretch;
        }

        public Alignment Alignment { get { return _alignment; } }
        public StretchOptions Stretch { get { return _stretch; } }
        public Size PreferredSize { get { return _preferredSize; } }
    }
}