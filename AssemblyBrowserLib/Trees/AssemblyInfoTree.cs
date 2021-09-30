using AssemblyBrowserLib.Infos;
using System.Linq;

namespace AssemblyBrowserLib.Nodes
{
    public class AssemblyInfoTree : Tree
    {
        public AssemblyInfoTree(AssemblyInfo info) :
            base(info.Name,
                info.NameSpaces
                .Select(n => new NamespaceInfoTree(n))
                .ToArray())
        {
        }
    }
}
