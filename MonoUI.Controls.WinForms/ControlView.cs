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
        private readonly Canvas _canvas;
        private readonly Property<Size> _preferredSize;
        public Property<Alignment> Alignment { get; private set; }

        public IWinFormsControlView Parent { get; set; }

        public ReadOnlyProperty<Size> PreferredSize
        {
            get { return _preferredSize; }
        }

        public Property<Rectangle> ActualBounds { get; private set; }

        public ControlView()
        {
            _canvas = new Canvas(OnPaint);
            _preferredSize = Properties.Create<Size>(OnPreferredSizeChange);
            ActualBounds = Properties.Create<Rectangle>(UpdateCanvasBounds);
            Alignment = Properties.Create<Alignment>(e => UpdateOwnLayout());
        }

        private void OnPreferredSizeChange(PropertyChangedEvent<Size> e)
        {
            if (Parent != null)
                Parent.UpdateOwnLayout();
        }

        private void UpdateCanvasBounds(PropertyChangedEvent<Rectangle> e)
        {
            _canvas.SetBounds(e.NewValue.X, e.NewValue.Y, e.NewValue.Width, e.NewValue.Height);
        }

        protected abstract void OnPaint(PaintEventArgs e);
        public void Invalidate() { _canvas.Invalidate(); }

        public void UpdateOwnLayout()
        {
            _preferredSize.Value = MeasurePreferredSize();
        }

        protected abstract Size MeasurePreferredSize();

        public void Dispose()
        {
            _canvas.Dispose();
        }

        public IEnumerable<Control> GetControls()
        {
            yield return _canvas;
        }
    }
}