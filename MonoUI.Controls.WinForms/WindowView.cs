using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonoUI.Core;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;

namespace MonoUI.Controls.WinForms
{
    public partial class WindowView : Form, IWinFormsControlView, IWindowView
    {
        private readonly Property<Size> _preferredSize;

        public WindowView()
        {
            InitializeComponent();
            Content = Properties.Create<IControl>(OnContentChange);
            Alignment = Properties.Create<Alignment>();
            _preferredSize = Properties.Create<Size>();
            ActualBounds = Properties.Create<Rectangle>();
            Resize += (o, e) => ActualBounds.Value = Bounds;
        }

        private void OnContentChange(PropertyChangedEvent<IControl> e)
        {
            if (e.OldValue != null)
                e.OldValue.Dispose();

            var winFormsControlView = e.NewValue.GetWinFormsView();

            Controls.AddRange(winFormsControlView.GetControls().ToArray());
            winFormsControlView.Parent = this;
            UpdateOwnLayout();
        }

        void IWindowView.Show()
        {
            ShowDialog();
        }

        public Property<IControl> Content { get; private set; }
        public Property<Alignment> Alignment { get; private set; }
        public IEnumerable<Control> GetControls()
        {
            yield return this;
        }

        public new IWinFormsControlView Parent { get { return null; } set { throw new NotSupportedException("Window cannot have parent"); } }

        public new ReadOnlyProperty<Size> PreferredSize
        {
            get { return _preferredSize; }
        }

        public Property<Rectangle> ActualBounds { get; private set; }
        public void UpdateOwnLayout()
        {
            if (Content.Value == null)
            {
                _preferredSize.Value = new Size();
                return;
            }
            var content = Content.Value.GetWinFormsView();
            _preferredSize.Value = content.PreferredSize;
            content.ActualBounds.Value = new Rectangle(new Point(0, 0), _preferredSize.Value);
        }
    }

    internal static class IControlExternsions
    {
        public static IWinFormsControlView GetWinFormsView(this IControl control)
        {
            return (IWinFormsControlView)control.View;
        }
    }
}
