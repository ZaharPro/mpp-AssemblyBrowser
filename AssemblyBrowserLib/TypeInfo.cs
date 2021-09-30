using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib
{
    public class TypeInfo : IItem
    {
        class StringItem : IItem
        {
            public string Header => null;
            public string Text { get; set; }
            public IEnumerable<IItem> SubItems => null;
        }
        public string Definition { get; }
        public IEnumerable<string> FieldDefinitions { get; }
        public IEnumerable<string> ConstructorDefinitions { get; }
        public IEnumerable<string> MethodDefinitions { get; }


        public string Header => nameof(TypeInfo);
        public string Text => Definition;
        public IEnumerable<IItem> SubItems { get; }

        public TypeInfo(Type type)
        {
            BindingFlags flags =
                BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.DeclaredOnly;

            Definition = type.PrintDefinition();
            FieldDefinitions = type.GetFields(flags)
                .Select(field => field.PrintDefinition())
                .ToArray();
            ConstructorDefinitions = type.GetConstructors(flags)
                .Select(constructor => constructor.PrintDefinition())
                .ToArray();
            MethodDefinitions = type.GetMethods(flags)
                .Select(method => method.PrintDefinition())
                .ToArray();

            List<string> itemsBuilder = new();
            //itemsBuilder.Add("Fields:");
            itemsBuilder.AddRange(FieldDefinitions);
            //itemsBuilder.Add("Constructors:");
            itemsBuilder.AddRange(ConstructorDefinitions);
            //itemsBuilder.Add("Methods:");
            itemsBuilder.AddRange(MethodDefinitions);

            SubItems = itemsBuilder
                .Select(s => new StringItem() { Text = s })
                .ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("Definition:\n").Append(Definition).Append('\n');
            sb.Append("Fields:", FieldDefinitions);
            sb.Append("Constructors:", ConstructorDefinitions);
            sb.Append("Methods:", MethodDefinitions);
            return sb.ToString();
        }
    }
}
