using System.Collections.Generic;

namespace AssemblyBrowserLib
{
    public interface IItem
    {
        string Header { get; }
        string Text { get; }
        IEnumerable<IItem> SubItems { get; }
    }
}
