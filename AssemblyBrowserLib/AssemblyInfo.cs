using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("Name:").Append(Name).Append('\n');
            sb.Append("Namespaces:", NameSpaces);
            return sb.ToString();
        }
    }
}
