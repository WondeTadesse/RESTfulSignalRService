//|---------------------------------------------------------------|
//|                     COMMON LIBRARY                            |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                     COMMON LIBRARY                            |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    /// <summary>
    /// Enum extension class
    /// </summary>
    public static class EnumExtension
    {
        #region Extension Methods 

        /// <summary>
        /// Get Enum Description
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>string value of the description</returns>
        public static string EnumDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Enum value is null !");
            }

            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            DescriptionAttribute[] attributes = (DescriptionAttribute[])
            fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }

        /// <summary>
        /// Find enum value from a string
        /// </summary>
        /// <typeparam name="TYourEnum">Enum Type</typeparam>
        /// <param name="value">Value to be seached</param>
        /// <returns>Enum value of type TYourEnum</returns>
        public static TYourEnum FindEnumFromDescription<TYourEnum>(this string value)
           where TYourEnum : struct, IConvertible
        {
            if (!typeof(TYourEnum).IsEnum || string.IsNullOrWhiteSpace(value))
            {
                return default(TYourEnum);
            }

            var enumValues = Enum.GetValues(typeof(TYourEnum));

            foreach (var item in enumValues)
            {
                if (value.ToLower().Equals((item as Enum).EnumDescription().ToLower()))
                {
                    return (TYourEnum)item;
                }
            }

            return default(TYourEnum);
        }
      
        #endregion
    }
}
