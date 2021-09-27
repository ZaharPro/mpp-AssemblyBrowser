using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowserLib
{
    public static class AssemblyExt
    {
        public static IDictionary<string, IEnumerable<Type>> Namespaces(this Assembly assembly)
        {
            _ = assembly ?? throw new ArgumentNullException(nameof(assembly));
            Dictionary<string, List<Type>> namespaces = new();
            foreach (Type type in assembly.GetTypes())
            {
                if (!namespaces.TryGetValue(type.Namespace, out List<Type> list))
                {
                    list = new List<Type>();
                    namespaces.Add(type.Namespace, list);
                }
                list.Add(type);
            }
            return (IDictionary<string, IEnumerable<Type>>)namespaces;
        }
    }
}