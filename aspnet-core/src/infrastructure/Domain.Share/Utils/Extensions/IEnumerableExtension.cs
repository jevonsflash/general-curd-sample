using System;
using System.Collections.Generic;

namespace Domain.Share.Utils.Extensions
{
    /// <summary>
    /// IEnumerable扩展
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 循环遍历
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="lists"></param>
        /// <param name="func">循环回调函数</param>
        public static void ForEach<T>(this IEnumerable<T> lists, Action<T> func)
        {
            if (func == null) return;

            foreach (var item in lists)
            {
                func(item);
            }
        }

        /// <summary>
        /// 空对象转空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lists"></param>
        /// <returns></returns>
        public static IEnumerable<T> NullToEmpty<T>(this IEnumerable<T> lists)
        {
            if (lists == null) return new List<T>();
            return lists;
        }

        /// <summary>
        /// 字符集合转字符
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinAsString<T>(this IEnumerable<T> list, string separator = ",")
        {
            return string.Join(separator, list);
        }
    }
}
