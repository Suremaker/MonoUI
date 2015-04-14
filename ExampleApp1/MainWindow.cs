using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoUI.Controls;
using MonoUI.Controls.Containers;
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
                .Set(w => w.Content, new LinearContainer()
                    .Set(l => l.ContentAlignment, new Alignment(HorizontalAlignment.Right, VerticalAlignment.Top))
                    .Set(l => l.ContentStretch, StretchOptions.Both)
                    .Set(l => l.Orientation, Orientation.Horizontal)
                    .Set(l => l.Spacing, Spacing.Minimal)
                    .Set(l => l.Items,
                        new Label()
                            .Set(l => l.Text, "Hellooooo\noooo:")
                            .Set(l => l.Alignment, new Alignment(HorizontalAlignment.Right, VerticalAlignment.Top)),
                        new Label()
                            .Set(l => l.Text, "Worlddddd")
                            .Set(l => l.Expansion, ExpansionOptions.Expand)
                            .Set(l => l.Alignment, new Alignment(HorizontalAlignment.Left, VerticalAlignment.Center)),
                        new Label()
                            .Set(l => l.Text, "Lalalaaaaa")
                            .Set(l => l.Expansion, ExpansionOptions.Expand)
                            .Set(l => l.Alignment, new Alignment(HorizontalAlignment.Center, VerticalAlignment.Bottom))
                        ));
        }

        public Window Root { get; private set; }
    }
}
