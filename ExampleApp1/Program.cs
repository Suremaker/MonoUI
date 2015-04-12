using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MonoUI.Controls;
using MonoUI.Controls.WinForms;
using MonoUI.Core;
using Window = MonoUI.Controls.Window;

namespace ExampleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly[] controlAssemblies=new []{typeof(Program).Assembly,typeof(Window).Assembly};
            Assembly[] rendererAssemblies = new[] { typeof(Program).Assembly, typeof(WindowView).Assembly };
            using (new UIManager(controlAssemblies, rendererAssemblies))
            {
                new MainWindowControl().Show();
            }
        }
    }
}
