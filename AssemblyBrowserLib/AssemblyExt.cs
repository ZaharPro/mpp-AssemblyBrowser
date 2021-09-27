﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowserLib
{
    public static class AssemblyExt
    {
        public static AssemblyInfo GetAssemblyInfo(this Assembly assembly)
        {
            return new AssemblyInfo(assembly);
        }
        public static IDictionary<string, List<Type>> GetNamespaces(this Assembly assembly)
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
            return namespaces;
        }
    }
}