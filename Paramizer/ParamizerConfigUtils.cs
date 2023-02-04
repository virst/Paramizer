using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Paramizer
{
    public static class ParamizerConfigUtils
    {
        public static string ToString(object o)
        {
            StringBuilder sb = new StringBuilder();
            var properties = o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                if (!prop.CanRead)
                    continue;
                sb.AppendLine($"{prop.Name} = {prop.GetValue(o)}");
            }
            return sb.ToString();
        }
    }
}
