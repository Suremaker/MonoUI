using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MonoUI.Metadata;

namespace MonoUI.Core
{
    public class UIManager : IDisposable
    {
        private readonly Dictionary<Type, Func<object>> _viewCreators = new Dictionary<Type, Func<object>>();
        #region Instance
        private static UIManager _instance;

        internal static UIManager Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("UIManager instance is null. Please instantiate UIManager first.");
                return _instance;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void SetInstance(UIManager instance)
        {

            if (_instance != null)
                throw new InvalidOperationException("UIManager instance is already created. Please dispose existing instance first, before creating new one.");
            _instance = instance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void RemoveInstance(UIManager instance)
        {
            if (_instance == instance)
                _instance = null;
        }
        #endregion

        public UIManager(Assembly[] controlAssemblies, Assembly[] rendererAssemblies)
        {
            SetInstance(this);
            var viewTypes = FindViewTypes(controlAssemblies);
            RegisterNativeRenderers(rendererAssemblies, viewTypes);
            RegisterGeneratedViewTypes(viewTypes);
        }

        private void RegisterGeneratedViewTypes(Type[] viewTypes)
        {
            var generatedViewTypes = viewTypes.Where(v => v.GetCustomAttributes(typeof(GeneratedViewAttribute), true).Any()).ToArray();
            foreach (var viewType in generatedViewTypes)
            {
                var viewDefinitionLocation = viewType.GetCustomAttributes(typeof(GeneratedViewAttribute), true)
                    .Cast<GeneratedViewAttribute>()
                    .Single()
                    .ViewDefinitionLocation;

                _viewCreators.Add(viewType, new DynamicViewBuilder(viewType, viewDefinitionLocation).Build);
            }
        }

        private void RegisterNativeRenderers(Assembly[] rendererAssemblies, Type[] viewTypes)
        {
            var nativeViewTypes = viewTypes.Where(v => !v.GetCustomAttributes(typeof(GeneratedViewAttribute), true).Any()).ToArray();
            var rendererTypes = GetRendererTypes(rendererAssemblies, nativeViewTypes);
            foreach (var viewType in nativeViewTypes)
            {
                var renderers = rendererTypes.Where(t => t.GetInterfaces().Contains(viewType)).ToArray();
                if (renderers.Length > 1)
                    throw new InvalidOperationException(string.Format("Multiple view {0} implementations found: {1}", viewType,
                        string.Join<Type>(", ", renderers)));
                if (renderers.Length == 0)
                    throw new InvalidOperationException(string.Format("View {0} implementation not found.", viewType));
                _viewCreators.Add(viewType, () => Activator.CreateInstance(renderers[0]));
            }
        }

        private static Type[] GetRendererTypes(Assembly[] rendererAssemblies, Type[] nativeViewTypes)
        {
            return rendererAssemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && t.GetInterfaces().Any(nativeViewTypes.Contains))
                .ToArray();
        }

        private static Type[] FindViewTypes(Assembly[] controlAssemblies)
        {
            return controlAssemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(Control<>))
                .Select(t => t.BaseType.GetGenericArguments()[0])
                .Distinct()
                .ToArray();
        }

        internal TView CreateView<TView>()
        {
            if (!typeof(TView).IsInterface) throw new ArgumentException(string.Format("Invalid view type: {0}. The view type has to be an interface.", typeof(TView)));
            return (TView)_viewCreators[typeof(TView)].Invoke();
        }

        public void Dispose()
        {
            RemoveInstance(this);
        }
    }
}