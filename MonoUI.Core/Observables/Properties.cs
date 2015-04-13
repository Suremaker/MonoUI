using System;
using System.Collections.ObjectModel;

namespace MonoUI.Core.Observables
{
    public static class Properties
    {
        private class ClassProperty<T> : Property<T>
        {
            protected override bool AreTheSame(T value1, T value2)
            {
                return ReferenceEquals(value1, value2);
            }

            public ClassProperty(T value) : base(value) { }
        }

        private class StructProperty<T> : Property<T>
        {
            protected override bool AreTheSame(T value1, T value2)
            {
                return Equals(value1, value2);
            }

            public StructProperty(T value) : base(value) { }
        }

        public static Property<T> Create<T>(T value = default(T))
        {
            return Create(null, value);
        }

        public static Property<T> Create<T>(Action<PropertyChangedEvent<T>> action, T value = default(T))
        {
            var prop = typeof(T).IsValueType
                ? (Property<T>)new StructProperty<T>(value)
                : new ClassProperty<T>(value);
            if (action != null)
                prop.Changed += action;
            return prop;
        }
    }

    public static class PropertyExtensions
    {
        public static TElement Set<TElement, T>(this TElement elem, Func<TElement, Property<T>> property, T value)
        {
            property(elem).Value = value;
            return elem;
        }

        public static TElement Set<TElement, T>(this TElement elem, Func<TElement, ObservableCollection<T>> property, params T[] values)
        {
            foreach (var value in values)
            {
                property(elem).Add(value);
            }

            return elem;
        }
    }
}