using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using MonoUI.Core;
using MonoUI.Core.Views;

namespace MonoUI.Layouts
{
    public static class LinearLayout
    {
        public static Size CalculateTotalSize(Orientation orientation, IReadOnlyCollection<ILayoutPreferences> controls, int spacing)
        {
            return orientation == Orientation.Horizontal
                ? CalculateHorizontalTotalSize(controls, spacing)
                : CalculateVerticalTotalSize(controls, spacing);
        }

        private static Size CalculateVerticalTotalSize(IReadOnlyCollection<ILayoutPreferences> controls, int spacing)
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

        private static Size CalculateHorizontalTotalSize(IReadOnlyCollection<ILayoutPreferences> controls, int spacing)
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

        public static IEnumerable<Rectangle> CalculateBounds(Orientation orientation, Size containerSize, ILayoutPreferences contentLayoutPreferences, IReadOnlyCollection<ILayoutPreferences> controls, int spacing)
        {
            return (orientation == Orientation.Horizontal)
                ? CalculateHorizontalBounds(containerSize, contentLayoutPreferences, controls, spacing)
                : CalculateVerticalBounds(containerSize, contentLayoutPreferences, controls, spacing);
        }

        private static IEnumerable<Rectangle> CalculateHorizontalBounds(Size containerSize, ILayoutPreferences contentLayoutPreferences, IReadOnlyCollection<ILayoutPreferences> controls, int spacing)
        {
            var bounds = ControlLayout.CalculateBounds(containerSize, contentLayoutPreferences);
            var expansionEnumerator = CalculateHorizontalExpansions(bounds.Width, contentLayoutPreferences.PreferredSize.Width, controls).GetEnumerator();
            int totalWidth = bounds.X + bounds.Width;
            int x = bounds.X;
            foreach (var control in controls)
            {
                expansionEnumerator.MoveNext();
                var controlAllocatedSize = new Size(Math.Min(totalWidth - x, control.PreferredSize.Width) + expansionEnumerator.Current, bounds.Height);
                var controlBounds = ControlLayout.CalculateBounds(controlAllocatedSize, control);

                controlBounds.X += x;
                controlBounds.Y += bounds.Y;
                x += controlAllocatedSize.Width + spacing;
                yield return controlBounds;
            }
        }

        private static IEnumerable<Rectangle> CalculateVerticalBounds(Size containerSize, ILayoutPreferences contentLayoutPreferences, IReadOnlyCollection<ILayoutPreferences> controls, int spacing)
        {
            var bounds = ControlLayout.CalculateBounds(containerSize, contentLayoutPreferences);
            var expansionEnumerator = CalculateVerticalExpansions(bounds.Height, contentLayoutPreferences.PreferredSize.Height, controls).GetEnumerator();
            int totalHeight = bounds.Y + bounds.Height;
            int y = bounds.Y;
            foreach (var control in controls)
            {
                expansionEnumerator.MoveNext();
                var controlAllocatedSize = new Size(bounds.Width, Math.Min(totalHeight - y, control.PreferredSize.Height) + expansionEnumerator.Current);
                var controlBounds = ControlLayout.CalculateBounds(controlAllocatedSize, control);
                controlBounds.X += bounds.X;
                controlBounds.Y += y;
                y += controlAllocatedSize.Height + spacing;
                yield return controlBounds;
            }
        }

        private static IEnumerable<int> CalculateHorizontalExpansions(int containerSize, int preferredSize, IReadOnlyCollection<ILayoutPreferences> controls)
        {
            if (containerSize <= preferredSize)
                return Enumerable.Repeat(0, controls.Count);
            return CalculateExpansions(containerSize - preferredSize, controls, x => x.PreferredSize.Width);
        }

        private static IEnumerable<int> CalculateVerticalExpansions(int containerSize, int preferredSize, IReadOnlyCollection<ILayoutPreferences> controls)
        {
            if (containerSize <= preferredSize)
                return Enumerable.Repeat(0, controls.Count);
            return CalculateExpansions(containerSize - preferredSize, controls, x => x.PreferredSize.Height);
        }

        private static IEnumerable<int> CalculateExpansions(int total, IReadOnlyCollection<ILayoutPreferences> controls, Func<ILayoutPreferences, int> sizeFn)
        {
            int totalCount = controls.Where(c => c.Expansion.Value > 0).Sum(c => c.Expansion.Value);
            double rem = 0;
            foreach (var control in controls)
            {
                double expansion = rem + (total * control.Expansion.Value) / (double)totalCount;
                int exp = (int)expansion;
                rem = expansion - exp;
                yield return exp;
            }
        }
    }
}