using System;
using System.Collections.Generic;
using System.Drawing;
using MonoUI.Core;

namespace MonoUI.Layouts
{
    public static class LinearLayout
    {
        public static Size CalculateTotalSize(Orientation orientation, IEnumerable<ILayoutPreferences> controls, int spacing)
        {
            return orientation == Orientation.Horizontal
                ? CalculateHorizontalTotalSize(controls, spacing)
                : CalculateVerticalTotalSize(controls, spacing);
        }

        private static Size CalculateVerticalTotalSize(IEnumerable<ILayoutPreferences> controls, int spacing)
        {
            int width = 0;
            int height = 0;
            int total = 0;
            foreach (var control in controls)
            {
                var size = control.PreferredSize;
                width = Math.Max(size.Width, width);
                height += size.Height;
                total += 1;
            }
            height += spacing * total;
            return new Size(width, height);
        }

        private static Size CalculateHorizontalTotalSize(IEnumerable<ILayoutPreferences> controls, int spacing)
        {
            int width = 0;
            int height = 0;
            int total = 0;
            foreach (var control in controls)
            {
                var size = control.PreferredSize;
                width += size.Width;
                height = Math.Max(size.Height, height);
                total += 1;
            }
            width += spacing * total;
            return new Size(width, height);
        }

        public static IEnumerable<Rectangle> CalculateBounds(Orientation orientation, Size containerSize, ILayoutPreferences contentLayoutPreferences, IEnumerable<ILayoutPreferences> controls, int spacing)
        {
            return (orientation == Orientation.Horizontal)
                ? CalculateHorizontalBounds(containerSize, contentLayoutPreferences, controls, spacing)
                : CalculateVerticalBounds(containerSize, contentLayoutPreferences, controls, spacing);
        }

        private static IEnumerable<Rectangle> CalculateHorizontalBounds(Size containerSize, ILayoutPreferences contentLayoutPreferences, IEnumerable<ILayoutPreferences> controls, int spacing)
        {
            var bounds = ControlLayout.CalculateBounds(containerSize, contentLayoutPreferences);
            int totalWidth = bounds.X + bounds.Width;            
            int x = bounds.X;
            foreach (var control in controls)
            {
                var controlAllocatedSize = new Size(Math.Min(totalWidth - x, control.PreferredSize.Width), bounds.Height);
                var controlBounds = ControlLayout.CalculateBounds(controlAllocatedSize, control);

                controlBounds.X += x;
                controlBounds.Y += bounds.Y;
                x += controlBounds.Width;
                yield return controlBounds;
            }
        }

        private static IEnumerable<Rectangle> CalculateVerticalBounds(Size containerSize, ILayoutPreferences contentLayoutPreferences, IEnumerable<ILayoutPreferences> controls, int spacing)
        {
            var bounds = ControlLayout.CalculateBounds(containerSize, contentLayoutPreferences);
            int totalHeight = bounds.Y + bounds.Height;
            int y = bounds.Y;
            foreach (var control in controls)
            {
                var controlAllocatedSize = new Size(bounds.Width, Math.Min(totalHeight - y, control.PreferredSize.Height));
                var controlBounds = ControlLayout.CalculateBounds(controlAllocatedSize, control);
                controlBounds.X += bounds.X;
                controlBounds.Y += y;
                y += controlBounds.Width;
                yield return controlBounds;
            }
        }
    }
}