using MonoUI.Core;

namespace MonoUI.Controls.WinForms
{
    internal static class ControlExternsions
    {
        public static IWinFormsControlView GetWinFormsView(this IControl control)
        {
            return (IWinFormsControlView)control.View;
        }
    }
}