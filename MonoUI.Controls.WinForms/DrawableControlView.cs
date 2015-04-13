using System.Drawing;
using System.Windows.Forms;
using MonoUI.Core.Observables;

namespace MonoUI.Controls.WinForms
{
    public abstract class DrawableControlView : ControlView
    {
        private readonly Canvas _canvas;

        public DrawableControlView()
        {
            _canvas = new Canvas(OnPaint);
            ActualBounds.Changed += UpdateCanvasBounds;
            OnBeforeDispose += () => _canvas.Dispose();
        }

        private void UpdateCanvasBounds(PropertyChangedEvent<Rectangle> e)
        {
            _canvas.SetBounds(e.NewValue.X, e.NewValue.Y, e.NewValue.Width, e.NewValue.Height);
        }

        protected abstract void OnPaint(PaintEventArgs e);
        
        public override Control Control{get { return _canvas; }}
    }
}