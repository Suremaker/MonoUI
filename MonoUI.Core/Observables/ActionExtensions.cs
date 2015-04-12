using System;

namespace MonoUI.Core.Observables
{
    public static class ActionExtensions
    {
        public static void Fire(this Action action)
        {
            if (action != null)
                action();
        }
        public static void Fire<TArg>(this Action<TArg> action, TArg arg1)
        {
            if (action != null)
                action(arg1);
        }
    }
}
