using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyBrowserLib
{
    public class NamespaceInfo : IItem
    {
        public string Name { get; }
        public IEnumerable<TypeInfo> Types { get; }

        public string Header => nameof(NamespaceInfo);
        public string Text => Name;
        public IEnumerable<IItem> SubItems => Types;

        public NamespaceInfo(string name, IEnumerable<Type> types)
        {
            Name = name;
            Types = types.Select(type => new TypeInfo(type));
        }
    }
}
