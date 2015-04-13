using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MonoUI.Controls.Containers;
using MonoUI.Core;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;
using MonoUI.Layouts;
using Orientation = MonoUI.Core.Orientation;

namespace MonoUI.Controls.WinForms
{
    public class LinearContainerView : DrawableControlView, LinearContainer.ILinearContainerView, IWinFormsContainer
    {
        public static readonly ReadOnlyProperty<int> MinimalSpacingStyle = Properties.Create<int>(1);
        public static readonly ReadOnlyProperty<int> NormalSpacingStyle = Properties.Create<int>(5);
        public static readonly ReadOnlyProperty<int> LargeSpacingStyle = Properties.Create<int>(15);
        private readonly ILayoutPreferences _contentLayoutPreferences;

        public ObservableCollection<IControl> Items { get; private set; }
        public Property<Orientation> Orientation { get; private set; }
        public Property<Alignment> ContentAlignment { get; private set; }
        public Property<StretchOptions> ContentStretch { get; private set; }
        public Property<Spacing> Spacing { get; private set; }

        public LinearContainerView()
        {
            Items = new ObservableCollection<IControl>();
            Orientation = Properties.Create<Orientation>(e => RecalculateSize());
            Spacing = Properties.Create<Spacing>(e => RecalculateSize());
            ContentAlignment = Properties.Create<Alignment>(e => LayoutChildren());
            ContentStretch = Properties.Create<StretchOptions>(e => LayoutChildren(),StretchOptions.Both);
            Stretch.Value = StretchOptions.Both;
            Items.CollectionChanged += OnCollectionChange;
            ActualBounds.Changed += e => LayoutChildren();
            _contentLayoutPreferences = new LayoutPreferences(PreferredSize, ContentAlignment, ContentStretch);
            
        }

        private void OnCollectionChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (IControl item in e.OldItems)
                    item.GetWinFormsView().UnsetParent(this);
            if (e.NewItems != null)
                foreach (IControl item in e.NewItems)
                    item.GetWinFormsView().SetParent(this, Control);
            RecalculatePreferredSize();
        }

        protected override Size MeasurePreferredSize()
        {
            if (!Items.Any())
                return new Size();
            return LinearLayout.CalculateTotalSize(Orientation, Items.Select(item => item.GetWinFormsView().LayoutPreferences), GetSpacing());
        }

        public void LayoutChildren()
        {
            var bounds = LinearLayout.CalculateBounds(
                Orientation,
                ActualBounds.Value.Size,
                ContentLayoutPreferences,
                Items.Select(i => i.GetWinFormsView().LayoutPreferences), GetSpacing()).GetEnumerator();
            foreach (var item in Items.Select(i => i.GetWinFormsView()))
            {
                bounds.MoveNext();
                item.ActualBounds.Value = bounds.Current;
            }
        }

        private int GetSpacing()
        {
            switch (Spacing.Value)
            {
                case Containers.Spacing.Large:
                    return LargeSpacingStyle;
                case Containers.Spacing.Normal:
                    return NormalSpacingStyle;
                case Containers.Spacing.Minimal:
                    return MinimalSpacingStyle;
                default:
                    return 0;
            }
        }

        public void RecalculateSize()
        {
            RecalculatePreferredSize();
        }

        public ILayoutPreferences ContentLayoutPreferences { get { return _contentLayoutPreferences; } }

        protected override void OnPaint(PaintEventArgs e)
        {
        }
    }
}