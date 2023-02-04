using System.Text.Json;
using System.IO;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Paramizer
{
    /// <summary>
    /// Обсуживание параметров заданных через ParameterAttribute
    /// </summary>
    public static class ParamizerConfigLoader
    {
        /// <summary>
        /// Приведение строки к любому типу
        /// </summary>
        /// <param name="str">строка</param>
        /// <param name="type">требуемы тип</param>
        /// <returns></returns>
        private static object GetTfromString(string str, Type type)
        {
            var foo = TypeDescriptor.GetConverter(type);
            return foo.ConvertFromInvariantString(str);
        }

        /// <summary>
        /// Парсинг параметров из различных источников
        /// </summary>
        /// <typeparam name="T">Типа переменной с параметром</typeparam>
        /// <param name="args">Аргументы командной строки</param>
        /// <param name="fn">Путь к json файлу</param>
        /// <param name="fileLead">Максимальный приоритет файоа</param>
        /// <returns></returns>
        public static T ParseParams<T>(string[] args = null, string fn = null, bool fileLead = false) where T : class, new()
        {
            T ret = null;
            if (fn != null && File.Exists(fn))
            {
                var json = File.ReadAllText(fn);
                ret = JsonSerializer.Deserialize<T>(json);
                if (fileLead)
                    return ret;
            }
            if (ret == null)
                ret = new T();

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                string v = null;
                var attributes = prop.GetCustomAttributes(typeof(ParameterAttribute), true);
                foreach (ParameterAttribute attribute in attributes)
                {
                    if (attribute.EnvironmentName != null && Environment.GetEnvironmentVariable(attribute.EnvironmentName) != null)
                        v = Environment.GetEnvironmentVariable(attribute.EnvironmentName);
                    if (args == null)
                        continue;
                    if (attribute.Position != null && args.Length > attribute.Position)
                        v = args[attribute.Position.Value];
                    if (attribute.Name != null && Array.IndexOf(args, $"--{attribute.Name}") > 0)
                        v = args[Array.IndexOf(args, $"--{attribute.Name}") + 1];
                    if (attribute.ShortName != null && Array.IndexOf(args, $"-{attribute.ShortName}") > 0)
                        v = args[Array.IndexOf(args, $"--{attribute.Name}") + 1];
                }
                if (v != null)
                    prop.SetValue(ret, GetTfromString(v, prop.PropertyType));
            }

            return ret;
        }


    }
}
