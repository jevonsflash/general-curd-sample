using System;
using System.ComponentModel;

namespace Domain.Share.Utils.Extensions
{
    /// <summary>
    /// Enum扩展
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取标记描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum obj)
        {
            var field = obj.GetType().GetField(obj.ToString());

            var customAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            if (customAttribute == null) return obj.ToString();

            return ((DescriptionAttribute)customAttribute).Description;
        }
    }
}
