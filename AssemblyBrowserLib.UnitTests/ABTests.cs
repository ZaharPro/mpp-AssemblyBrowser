using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace AssemblyBrowserLib.UnitTests
{
    public class ABTests
    {
        public interface IInterface : IEnumerable<IEnumerator<int>> { }
        protected internal abstract class AbstractClass<T> : IInterface
        {
            public virtual IEnumerator<IEnumerator<int>> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
        private protected struct Enumerator : IEnumerator
        {
            private static IEnumerable<int> _field;

            public Enumerator(IEnumerable<int> field)
            {
                _field = field;
            }

            public object Current => _field;

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        private record User(string Email, decimal HashPassword);

        private interface IExample
        {
            IEnumerable<User> GetUsers();
        }

        [Fact]
        public void Test()
        {
            /*foreach (NameSpaceInfo @namespace in typeof(ABTests).Assembly.GetAssemblyInfo().NameSpaces)
            {
                Console.WriteLine(@namespace.Name);
                Console.WriteLine("Defined types:");
                foreach (TypeInfo type in @namespace.Types)
                {
                    Console.WriteLine(type.Definition);
                    Console.WriteLine("Fields:");
                    foreach (string f in type.FieldDefinitions)
                        Console.WriteLine(f);

                    Console.WriteLine("Constructors:");
                    foreach (string c in type.ConstructorDefinitions)
                        Console.WriteLine(c);

                    Console.WriteLine("Methods:");
                    foreach (string m in type.MethodDefinitions)
                        Console.WriteLine(m);
                }
            }*/
        }
    }
}
