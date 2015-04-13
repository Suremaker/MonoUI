using System.Drawing;
using System.Windows.Forms;
using MonoUI.Core;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;
using MonoUI.Layouts;

namespace MonoUI.Controls.WinForms
{
    public class WindowView : Window.IWindowView, IWinFormsContainer
    {
        private readonly Form _form;
        private readonly ILayoutPreferences _contentLayoutPreferences;
        private readonly Property<Size> _preferredSize;
        public Property<IControl> Content { get; private set; }
        public Property<Alignment> Alignment { get; private set; }
        public Property<StretchOptions> Stretch { get; private set; }
        public Property<Alignment> ContentAlignment { get; private set; }
        public Property<StretchOptions> ContentStretch { get; private set; }
        public ILayoutPreferences ContentLayoutPreferences { get { return _contentLayoutPreferences; } }

        public WindowView()
        {
            _form = new Form();
            _form.Resize += (o, e) => LayoutChildren();
            Alignment = Properties.Create<Alignment>();
            Content = Properties.Create<IControl>(OnContentChange);
            ContentAlignment = Properties.Create<Alignment>(e => LayoutChildren());
            ContentStretch = Properties.Create<StretchOptions>(e => LayoutChildren());
            Stretch = Properties.Create<StretchOptions>();
            _preferredSize = Properties.Create<Size>();
            _contentLayoutPreferences = new LayoutPreferences(_preferredSize, ContentAlignment, ContentStretch);
        }

        private void OnContentChange(PropertyChangedEvent<IControl> e)
        {
            if (e.OldValue != null)
                e.OldValue.GetWinFormsView().UnsetParent(this);

            var winFormsControlView = e.NewValue.GetWinFormsView();

            winFormsControlView.SetParent(this, _form);
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
            view.ActualBounds.Value = ControlLayout.CalculateBounds(_form.ClientSize, view.LayoutPreferences);
        }

        public void RecalculateSize()
        {
            if (Content.Value == null)
                return;
            _preferredSize.Value = Content.Value.GetWinFormsView().PreferredSize;
            if (_form.ClientSize != _preferredSize.Value)
                _form.ClientSize = _preferredSize.Value;
            else
                LayoutChildren();
        }

        public void Dispose()
        {
            _form.Dispose();
        }
    }
}
