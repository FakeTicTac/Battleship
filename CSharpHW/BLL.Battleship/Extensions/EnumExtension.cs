using System;
using System.ComponentModel;


namespace BLL.Battleship.Extensions
{
    
    /// <summary>
    /// Describes Enum Class Extensions Methods.
    /// </summary>
    public static class EnumExtension
    {

        /// <summary>
        /// Get Enum Description in String Format.
        /// </summary>
        /// <param name="enumeration">Enum to get Description from.</param>
        /// <returns>Enum Description in String Format</returns>
        public static string? GetDescription(this Enum enumeration)
        {
            var type = enumeration.GetType();
            var name = Enum.GetName(type, enumeration);

            if (name == null) return null;
            
            var fieldInfo = type.GetField(name);

            if (fieldInfo == null) return null;
            
            var description = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return description?.Description;
        }
    }
}