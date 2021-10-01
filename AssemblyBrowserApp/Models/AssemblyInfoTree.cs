using AssemblyBrowserLib.Infos;
using System.Linq;

namespace Models
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
