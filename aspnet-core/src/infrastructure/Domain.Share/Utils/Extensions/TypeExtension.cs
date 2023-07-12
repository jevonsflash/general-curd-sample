using System;
using System.Collections;
using System.Linq;

namespace Domain.Share.Utils.Extensions
{
    /// <summary>
    /// 类型扩展
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 是否数组类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsArray(this Type type)
        {
            if (type.GetUnderlyingType().IsArray) return true;

            return type.GetUnderlyingType().GetInterfaces().Any(p => p == typeof(ICollection));
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnum(this Type type)
        {
            return type.GetUnderlyingType().IsEnum;
        }

        /// <summary>
        /// 获取实际类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetUnderlyingType(this Type type)
        {
            if (type.Name == typeof(Nullable<>).Name)
            {
                return type.GetGenericArguments().FirstOrDefault();
            }

            return type;
        }
    }
}