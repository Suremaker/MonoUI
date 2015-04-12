using System;
using System.Xml;
using System.Xml.Linq;

namespace MonoUI.Core
{
    public class DynamicViewBuilder
    {
        private readonly Type _viewType;
        private XDocument _doc;

        public DynamicViewBuilder(Type viewType, string viewDefinitionLocation)
        {
            _viewType = viewType;
            try
            {
                _doc = XDocument.Load(viewDefinitionLocation);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(string.Format("Unable to load view definition for {0}: {1}", viewType, e.Message), e);
            }
        }

        public object Build()
        {
            return null;
        }
    }
}