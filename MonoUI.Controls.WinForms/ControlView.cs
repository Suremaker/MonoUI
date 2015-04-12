using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;

namespace MonoUI.Controls.WinForms
{
    public abstract class ControlView : IWinFormsControlView
    {
        private readonly Property<Size> _preferredSize;
        public event Action OnBeforeDispose;
        public Property<Alignment> Alignment { get; private set; }
        public abstract IEnumerable<Control> GetControls();
        public IWinFormsContainer Parent { get; set; }
        public ReadOnlyProperty<Size> PreferredSize { get { return _preferredSize; } }
        public Property<Rectangle> ActualBounds { get; private set; }
        public abstract void Invalidate();

        protected ControlView()
        {
            _preferredSize = Properties.Create<Size>(e => RequestSizeRecalculation());
            ActualBounds = Properties.Create<Rectangle>();
            Alignment = Properties.Create<Alignment>(e => RequestLayout());
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