using System;

namespace Paramizer
{
    /// <summary>
    /// Атриб для авторазбираемых параметров
    /// </summary>
    public class ParameterAttribute : Attribute
    {
        internal readonly string Name;
        internal readonly char? ShortName = null;
        internal readonly int? Position = null;
        internal readonly string EnvironmentName;
        internal readonly string Description;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Системное имя параметра</param>
        /// <param name="shortName">Сокращение системного имени параметра</param>
        /// <param name="position">Определение параметра по позиции в аргументах приложения</param>
        /// <param name="environmentName">Имя параметра в переменных окружения</param>
        /// <param name="description">Полнотекстовое описание параметра</param>
        public ParameterAttribute(string name = null,
            char shortName = '\0',
            int position = -1,
            string environmentName = null,
            string description = null)
        {
            Name = name;
            if (shortName != '\0')
                ShortName = shortName;
            if (position > -1)
                Position = position;
            EnvironmentName = environmentName;
            Description = description;
        }
    }
}
