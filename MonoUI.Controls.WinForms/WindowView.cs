using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MonoUI.Core;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;
using HorizontalAlignment = MonoUI.Core.Views.HorizontalAlignment;

namespace MonoUI.Controls.WinForms
{
    public class WindowView : Window.IWindowView, IWinFormsContainer
    {
        private readonly Form _form;
        public Property<IControl> Content { get; private set; }
        public Property<Alignment> Alignment { get; private set; }

        public WindowView()
        {
            _form = new Form();
            _form.Resize += (o, e) => LayoutChildren();
            Alignment = Properties.Create<Alignment>();
            Content = Properties.Create<IControl>(OnContentChange);
        }

        private void OnContentChange(PropertyChangedEvent<IControl> e)
        {
            if (e.OldValue != null)
                e.OldValue.Dispose();

            var winFormsControlView = e.NewValue.GetWinFormsView();

            _form.Controls.AddRange(winFormsControlView.GetControls().ToArray());
            winFormsControlView.Parent = this;
            RecalculateSize();
        }

        public void Show()
        {
            _form.ShowDialog();
        }

        public void LayoutChildren()
        {
            if (Content.Value == null)
                return;

            var view = Content.Value.GetWinFormsView();
            Size preferred = view.PreferredSize;
            var horizontalAlignment = view.Alignment.Value.Horizontal;
            var verticalAlignment = view.Alignment.Value.Vertical;

            var clientWidth = _form.ClientSize.Width;
            var clientHeight = _form.ClientSize.Height;
            int x = clientWidth - preferred.Width;
            int y = clientHeight - preferred.Height;

            if (x < 0 || horizontalAlignment == HorizontalAlignment.Left)
                x = 0;
            else if (horizontalAlignment == HorizontalAlignment.Center)
                x /= 2;

            if (y < 0 || verticalAlignment == VerticalAlignment.Top)
                y = 0;
            else if (verticalAlignment == VerticalAlignment.Center)
                y /= 2;

            view.ActualBounds.Value = new Rectangle(x, y, Math.Min(clientWidth, preferred.Width), Math.Min(clientHeight, preferred.Height));
        }

        public void RecalculateSize()
        {
            if (Content.Value == null)
                return;
            Size preferredSize = Content.Value.GetWinFormsView().PreferredSize;
            if (_form.ClientSize != preferredSize)
                _form.ClientSize = preferredSize;
            else
                LayoutChildren();
        }

        public void Dispose()
        {
            _form.Dispose();
        }
    }
}
