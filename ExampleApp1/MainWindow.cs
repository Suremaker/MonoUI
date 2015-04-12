using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoUI.Controls;
using MonoUI.Controls.Primitives;
using MonoUI.Core;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;
using MonoUI.Metadata;

namespace ExampleApp1
{
    //[GeneratedView("MainWindow.xml")]
    public interface IMainWindowView : IControlView //generated
    {
        Window Root { get; }
    }

    public class MainWindowControl : Control<IMainWindowView>
    {
        public void Show()
        {
            View.Root.Show();
        }
    }

    //Generated
    public class MainWindowView : ComposedView, IMainWindowView
    {
        public MainWindowView()
        {
            Root = new Window()
                .Set(w => w.Content, new Label()
                    .Set(l => l.Text, "Text"));
        }

        public Window Root { get; private set; }
        public Property<Alignment> Alignment { get; private set; }
    }
}
