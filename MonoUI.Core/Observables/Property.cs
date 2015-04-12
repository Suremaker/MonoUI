using System;

namespace MonoUI.Core.Observables
{
    public abstract class Property<T> : ReadOnlyProperty<T>
    {
        public new T Value { get { return base.Value; } set { base.Value = value; } }

        protected Property(T value) : base(value){}
    }
}