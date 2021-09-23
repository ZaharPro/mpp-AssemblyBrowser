using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib
{
    public class AssemblyBrowser
    {
        public IDictionary<string, IList<Type>> NamespacesFrom(Assembly assembly)
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
            return (IDictionary<string, IList<Type>>)namespaces;
        }

        public void AppendAttribures(StringBuilder sb, Type type)
        {
            TypeAttributes attributes = type.Attributes;
            string visibility = (attributes & TypeAttributes.VisibilityMask) switch
            {
                TypeAttributes.NotPublic => "not public",
                TypeAttributes.Public => "public",
                TypeAttributes.NestedPublic => "nested and public",
                TypeAttributes.NestedPrivate => "nested and private",
                TypeAttributes.NestedAssembly => "nested and internal",
                TypeAttributes.NestedFamily => "nested and protected",
                TypeAttributes.NestedFamORAssem => "nested and protected internal",
                _ => null,
            };

            if (visibility != null)
            {
                sb.Append(visibility);
            }

            if ((attributes & TypeAttributes.Abstract) != 0)
            {
                if (sb.Length != 0)
                {
                    sb.Append(' ');
                }
                sb.Append("abstract");
            }

            if ((attributes & TypeAttributes.Sealed) != 0)
            {
                if (sb.Length != 0)
                {
                    sb.Append(' ');
                }
                sb.Append("sealed");
            }

            string semantics = (attributes & TypeAttributes.ClassSemanticsMask) switch
            {
                TypeAttributes.Class => type.IsValueType ? "value type" : "class",
                TypeAttributes.Interface => "interface",
                _ => null,
            };
            if (semantics != null)
            {
                if (sb.Length != 0)
                {
                    sb.Append(' ');
                }
                sb.Append(semantics);
            }
        }
        private void AppendTypeName(StringBuilder sb, Type type)
        {
            sb.Append(type.Name);
            if (type.IsGenericType)
            {
                sb.Length -= 2;
                Type[] genericTypes = type.GetGenericArguments();
                if (genericTypes.Length != 0)
                {
                    sb.Append('<');
                    foreach (Type genericType in genericTypes)
                    {
                        AppendTypeName(sb, genericType);
                        sb.Append(',').Append(' ');
                    }
                    sb.Length -= 2;
                    sb.Append('>');
                }
            }
        }
        private string TypeToString(Type type)
        {
            StringBuilder sb = new();
            AppendAttribures(sb, type);

            if (sb.Length != 0)
            {
                sb.Append(' ');
            }

            AppendTypeName(sb, type);
            Type baseType = type.BaseType;
            if (baseType != null)
            {
                sb.Append(':').Append(' ');
                AppendTypeName(sb, baseType);
            }
            Type[] interfaces = type.GetInterfaces();
            if (interfaces.Length != 0)
            {
                sb.Append(baseType == null ? ": " : ' ');

                foreach (Type interfaceType in interfaces)
                {
                    AppendTypeName(sb, interfaceType);
                    sb.Append(',').Append(' ');
                }
                sb.Length -= 2;
            }
            return sb.ToString();
        }

        public void Parse(IDictionary<string, IList<Type>> namespaces)
        {
            foreach (var @namespace in namespaces)
            {
                string namespaseName = @namespace.Key;
                IList<Type> declaredTypes = @namespace.Value;
                foreach (var type in declaredTypes)
                {
                    string typeToString = TypeToString(type);

                    ConstructorInfo[] constructors = type.GetConstructors();
                    FieldInfo[] fields = type.GetFields();
                    PropertyInfo[] properties = type.GetProperties();
                    EventInfo[] events = type.GetEvents();
                    MethodInfo[] methods = type.GetMethods();
                    Type[] nestedTypes = type.GetNestedTypes();
                }
            }
        }
    }
}