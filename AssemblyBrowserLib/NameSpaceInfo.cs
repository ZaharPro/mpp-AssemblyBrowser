using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyBrowserLib
{
    public class NameSpaceInfo
    {
        public string Name { get; }
        public IEnumerable<TypeInfo> Types { get; }

        public NameSpaceInfo(string name, IEnumerable<Type> types)
        {
            Name = name;
            Types = types.Select(type => new TypeInfo(type));
        }
    }
}
