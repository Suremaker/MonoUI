using System;

namespace MonoUI.Core.Observables
{
    public class PropertyChangedEvent<T>
    {
        public PropertyChangedEvent(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public T OldValue { get; private set; }
        public T NewValue { get; private set; }

        public override string ToString()
        {
            return string.Format("Old={0}, New={1}", OldValue, NewValue);
        }
    }
    public abstract class ReadOnlyProperty<T>
    {
        private T _value;

        public T Value
        {
            get { return _value; }
            protected set
            {
                if (AreTheSame(_value, value)) return;
                var old = _value;
                _value = value;
                if (Changed != null)
                    Changed.Invoke(new PropertyChangedEvent<T>(old, value));
            }
        }

        public event Action<PropertyChangedEvent<T>> Changed;

        protected ReadOnlyProperty(T value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}", Value);
        }

        protected abstract bool AreTheSame(T value1, T value2);

        public static implicit operator T(ReadOnlyProperty<T> p)
        {
            return p.Value;
        }
    }
}