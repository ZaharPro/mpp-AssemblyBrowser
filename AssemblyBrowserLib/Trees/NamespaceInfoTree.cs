using AssemblyBrowserLib.Infos;
using System.Linq;

namespace AssemblyBrowserLib.Nodes
{
    public class NamespaceInfoTree : Tree
    {
        public NamespaceInfoTree(NamespaceInfo info) :
            base(info.Name,
                info.Types
                .Select(n => new TypeInfoTree(n))
                .ToArray())
        {
        }
    }
}
