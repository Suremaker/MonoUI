using System.Drawing;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;

namespace MonoUI.Layouts
{
    public interface ILayoutPreferences
    {
        Alignment Alignment { get; }
        StretchOptions Stretch { get; }
        ExpansionOptions Expansion { get; }
        Size PreferredSize { get; }
    }

    public class LayoutPreferences : ILayoutPreferences
    {
        private readonly ReadOnlyProperty<Size> _preferredSize;
        private readonly ReadOnlyProperty<Alignment> _alignment;
        private readonly ReadOnlyProperty<StretchOptions> _stretch;
        private readonly ReadOnlyProperty<ExpansionOptions> _expansion;

        public LayoutPreferences(ReadOnlyProperty<Size> preferredSize, ReadOnlyProperty<Alignment> alignment, ReadOnlyProperty<StretchOptions> stretch, ReadOnlyProperty<ExpansionOptions> expansion)
        {
            _preferredSize = preferredSize;
            _alignment = alignment;
            _stretch = stretch;
            _expansion = expansion;
        }

        public Alignment Alignment { get { return _alignment; } }
        public StretchOptions Stretch { get { return _stretch; } }
        public ExpansionOptions Expansion { get { return _expansion; } }
        public Size PreferredSize { get { return _preferredSize; } }
    }
}