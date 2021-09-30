using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace AssemblyBrowserLib
{
    public static class TextExt
    {
        private static readonly Regex regex = new("\\n+");
        public static string DeleteEmptyLines(this string s)
        {
            return regex.Replace(s, "\n");
        }

        public static void Append(this StringBuilder sb, string name, IEnumerable enumerable)
        {
            sb.Append(name).Append('\n');

            int len = sb.Length;
            foreach (object o in enumerable)
            {
                sb.Append(o).Append('\n');
            }
            if (len == sb.Length)
            {
                sb.Length--;
                sb.Append('[').Append(']').Append('\n');
            }
        }        
    }
}
