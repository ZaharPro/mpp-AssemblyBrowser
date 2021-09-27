using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyBrowserLib
{
    public class NamespaceInfo

    {
        public string Name { get; }
        public IEnumerable<TypeInfo> Types { get; }

        public NamespaceInfo(string name, IEnumerable<Type> types)
        {
            Name = name;
            Types = types.Select(type => new TypeInfo(type));
        }
    }
}
