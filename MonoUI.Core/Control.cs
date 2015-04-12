using System;
using System.Linq;
using System.Text;
using MonoUI.Core.Observables;
using MonoUI.Core.Views;

namespace MonoUI.Core
{

    public interface IControl : IDisposable
    {
        IControlView View { get; }
    }
    /// <summary>
    /// A visual element
    /// </summary>
    /// <typeparam name="TView">TView type</typeparam>
    public abstract class Control<TView> : IControl where TView : IControlView
    {
        IControlView IControl.View { get { return View; } }
        protected TView View { get; private set; }
        protected event Action OnBeforeDispose;

        protected Control()
        {
            View = UIManager.Instance.CreateView<TView>();
        }

        public void Dispose()
        {
            OnBeforeDispose.Fire();
            View.Dispose();
        }
    }
}
