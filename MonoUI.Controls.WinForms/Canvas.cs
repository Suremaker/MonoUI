using System;
using System.Drawing;
using System.Windows.Forms;

namespace MonoUI.Controls.WinForms
{
    public class Canvas : Control
    {
        private readonly Action<PaintEventArgs> _paintFunction;

        public Canvas(Action<PaintEventArgs> paintFunction)
        {
            _paintFunction = paintFunction;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _paintFunction(e);
        }
    }
}