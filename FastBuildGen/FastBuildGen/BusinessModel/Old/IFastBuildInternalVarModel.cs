using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel.Old
{
    public interface IFastBuildInternalVarModel
    {
        event NotifyCollectionChangedEventHandler PropertiesChanged;

        IDictionary<string, string> DefaultProperties { get; }

        IEnumerable<KeyValuePair<string, string>> Properties { get; }

        string this[string key] { get; set; }

        bool ContainsPropertyName(string propertyName);

        bool ContainsPropertyValue(string propertyValue, string exceptForName = null);

        void ResetToDefault();

        bool TryGetValue(string propertyName, out string value);
    }
}