using System;
using System.Drawing;
using System.Windows.Forms;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;
using MonoUI.Layouts;

namespace MonoUI.Controls.WinForms
{
    public abstract class ControlView : IWinFormsControlView
    {
        private readonly Property<Size> _preferredSize;
        private readonly ILayoutPreferences _layoutPreferences;
        public event Action OnBeforeDispose;
        public Property<Alignment> Alignment { get; private set; }
        public Property<StretchOptions> Stretch { get; private set; }
        public abstract Control Control { get; }
        public IWinFormsContainer Parent { get; private set; }

        public void SetParent(IWinFormsContainer parent, Control parentControl)
        {
            Parent = parent;
            parentControl.Controls.Add(Control);
        }

        public void UnsetParent(IWinFormsContainer parent)
        {
            if (Parent != parent)
                return;
            Parent = null;
            Control.Parent.Controls.Remove(Control);
        }

        public ReadOnlyProperty<Size> PreferredSize { get { return _preferredSize; } }
        public Property<Rectangle> ActualBounds { get; private set; }
        public ILayoutPreferences LayoutPreferences { get { return _layoutPreferences; } }

        protected ControlView()
        {
            _preferredSize = Properties.Create<Size>(e => RequestSizeRecalculation());
            ActualBounds = Properties.Create<Rectangle>();
            Alignment = Properties.Create<Alignment>(e => RequestLayout());
            Stretch = Properties.Create<StretchOptions>(e => RequestLayout());
            _layoutPreferences = new LayoutPreferences(PreferredSize, Alignment, Stretch);
        }

        protected void RequestSizeRecalculation()
        {
            if (Parent != null)
                Parent.RecalculateSize();
        }

        protected void RequestLayout()
        {
            if (Parent != null)
                Parent.LayoutChildren();
        }

        protected void RecalculatePreferredSize()
        {
            _preferredSize.Value = MeasurePreferredSize();
        }

        protected abstract Size MeasurePreferredSize();

        public void Dispose()
        {
            OnBeforeDispose.Fire();
        }
    }

}