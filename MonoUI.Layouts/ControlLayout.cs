using System;
using System.Drawing;
using MonoUI.Core.Views;

namespace MonoUI.Layouts
{
    public static class ControlLayout
    {
        public static Rectangle CalculateBounds(Size containerSize, ILayoutPreferences control)
        {
            var preferredSize = control.PreferredSize;
            var horizontalAlignment = control.Alignment.Horizontal;
            var verticalAlignment = control.Alignment.Vertical;

            var clientWidth = containerSize.Width;
            var clientHeight = containerSize.Height;
            var controlWidth = (control.Stretch & StretchOptions.Horizontal) != 0 ? clientWidth : preferredSize.Width;
            var controlHeight = (control.Stretch & StretchOptions.Vertical) != 0 ? clientHeight : preferredSize.Height;
            int x = clientWidth - controlWidth;
            int y = clientHeight - controlHeight;

            if (x < 0 || horizontalAlignment == HorizontalAlignment.Left)
                x = 0;
            else if (horizontalAlignment == HorizontalAlignment.Center)
                x /= 2;

            if (y < 0 || verticalAlignment == VerticalAlignment.Top)
                y = 0;
            else if (verticalAlignment == VerticalAlignment.Center)
                y /= 2;

            return new Rectangle(x, y, Math.Min(clientWidth, controlWidth), Math.Min(clientHeight, controlHeight));
        }
    }
}
