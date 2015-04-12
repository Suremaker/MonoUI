using System;
using MonoUI.Core.Observables;

namespace MonoUI.Core.Views
{
    public interface IControlView:IDisposable
    {
        Property<Alignment> Alignment { get; }
    }
}