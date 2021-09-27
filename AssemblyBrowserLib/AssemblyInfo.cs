using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowserLib
{
    public class AssemblyInfo
    {
        public string Name { get; }
        public IEnumerable<NameSpaceInfo> NameSpaces { get; }

        public AssemblyInfo(Assembly assembly)
        {
            Name = assembly.GetName().ToString();
            NameSpaces = assembly.GetNamespaces()
                .Select(pair => new NameSpaceInfo(pair.Key, pair.Value))
                .ToArray();
        }
    }
}
