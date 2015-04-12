using System;

namespace MonoUI.Metadata
{
    /// <summary>
    /// A generated view, based on xml defintion
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class GeneratedViewAttribute : Attribute
    {
        public GeneratedViewAttribute(string viewDefinitionLocation)
        {
            ViewDefinitionLocation = viewDefinitionLocation;
        }

        public string ViewDefinitionLocation { get; private set; }
    }
}
