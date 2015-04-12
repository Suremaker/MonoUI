using System.Drawing;
using System.Windows.Forms;
using MonoUI.Controls.Primitives;
using MonoUI.Core.Observables;

namespace MonoUI.Controls.WinForms
{
    public class LabelView : ControlView, ILabelView
    {
        public Property<string> Text { get; private set; }

        public static readonly ReadOnlyProperty<Font> FontStyle = Properties.Create(Control.DefaultFont);
        public static readonly ReadOnlyProperty<Brush> ForegroundColorStyle = Properties.Create<Brush>(new SolidBrush(Control.DefaultForeColor));

        public LabelView()
        {
            Text = Properties.Create<string>(e => UpdateOwnLayout());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawString(Text, FontStyle, ForegroundColorStyle.Value, 0, 0);
        }

        protected override Size MeasurePreferredSize()
        {
            return TextRenderer.MeasureText(Text, FontStyle);
        }
    }
}