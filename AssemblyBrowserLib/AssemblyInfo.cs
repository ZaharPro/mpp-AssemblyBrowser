using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowserLib
{
    public class AssemblyInfo : IItem
    {
        public string Name { get; }
        public IEnumerable<NamespaceInfo> NameSpaces { get; }

        public string Header => nameof(AssemblyInfo);
        public string Text => Name;
        public IEnumerable<IItem> SubItems => NameSpaces;


        public AssemblyInfo(Assembly assembly)
        {
            Name = assembly.GetName().ToString();
            NameSpaces = assembly.GetNamespaces()
                .Select(pair => new NamespaceInfo(pair.Key, pair.Value))
                .ToArray();
        }
    }
}
