using System;

namespace Domain.Share.Utils.Extensions
{
    /// <summary>
    /// String扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatString(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        /// <summary>
        /// 转成DateTime(不可空)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dateTimeFormatString">默认YYYY-MM-dd HH:mm:ss</param>
        /// <param name="defaultValue">默认当前时间</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, string dateTimeFormatString = "YYYY-MM-dd HH:mm:ss", DateTime? defaultValue = null)
        {
            if (defaultValue.HasValue == false) defaultValue = DateTime.Now;

            return str.ToDateTimeNull(dateTimeFormatString, defaultValue.Value).Value;
        }

        /// <summary>
        /// 转成DateTime(可空)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dateTimeFormatString">默认YYYY-MM-dd HH:mm:ss</param>
        /// <param name="defaultValue">默认空对象</param>
        /// <returns></returns>
        public static DateTime? ToDateTimeNull(this string str, string dateTimeFormatString = "YYYY-MM-dd HH:mm:ss", DateTime? defaultValue = null)
        {
            if (str.IsNullOrWhiteSpace()) str = JsonExtension.JsonSerializerSettings.DateFormatString;

            if (DateTime.TryParseExact(str, dateTimeFormatString, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {
                return dateTime;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
