using System;
using MonoUI.Core.Observables;

namespace MonoUI.Core.Views
{
    public interface IContainerView : IDisposable
    {
        Property<Alignment> ContentAlignment { get; }
        Property<StretchOptions> ContentStretch { get; }
    }
}