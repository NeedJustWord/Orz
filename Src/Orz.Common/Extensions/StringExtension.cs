using System;

#if IS_FRAMEWORK || IS_NETCOREAPP2
using System.Globalization;
#endif

namespace Orz.Common.Extensions
{
    /// <summary>
    /// <see cref="string"/>扩展方法
    /// </summary>
    public static class StringExtension
    {
        #region null、空串、空白字符串

        #region IsNullOrEmpty、IsNotNullAndEmpty
        /// <summary>
        /// 是否为null或空串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 是否不为null且不为空串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullAndEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
        #endregion

        #region IsNullOrWhiteSpace、IsNotNullAndWhiteSpace
        /// <summary>
        /// 是否为null或空白字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 是否不为null且不为空白字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullAndWhiteSpace(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
        #endregion

        #region IfNullOrEmpty、IfNullOrWhiteSpace
        /// <summary>
        /// 如果为null或空串，则返回<paramref name="defaultValue"/>，否则返回<paramref name="str"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string IfNullOrEmpty(this string str, string defaultValue)
        {
            return string.IsNullOrEmpty(str) ? defaultValue : str;
        }

        /// <summary>
        /// 如果为null或空白字符串，则返回<paramref name="defaultValue"/>，否则返回<paramref name="str"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string IfNullOrWhiteSpace(this string str, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(str) ? defaultValue : str;
        }
        #endregion

        #endregion

        #region 安全转换

        #region ToLower
        /// <summary>
        /// 将<paramref name="str"/>转小写，<paramref name="str"/>为null时返回<paramref name="nullValue"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string ToLowerSafe(this string str, string nullValue = "")
        {
            return str == null ? nullValue : str.ToLower();
        }

        /// <summary>
        /// 将<paramref name="str"/>转小写，<paramref name="str"/>为null时返回<paramref name="nullValue"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string ToLowerInvariantSafe(this string str, string nullValue = "")
        {
            return str == null ? nullValue : str.ToLowerInvariant();
        }
        #endregion

        #region ToUpper
        /// <summary>
        /// 将<paramref name="str"/>转大写，<paramref name="str"/>为null时返回<paramref name="nullValue"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string ToUpperSafe(this string str, string nullValue = "")
        {
            return str == null ? nullValue : str.ToUpper();
        }

        /// <summary>
        /// 将<paramref name="str"/>转大写，<paramref name="str"/>为null时返回<paramref name="nullValue"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string ToUpperInvariantSafe(this string str, string nullValue = "")
        {
            return str == null ? nullValue : str.ToUpperInvariant();
        }
        #endregion

        #region ToCharArray
        /// <summary>
        /// 将<paramref name="str"/>转字符数组，<paramref name="str"/>为null时返回空数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static char[] ToCharArraySafe(this string str)
        {
            return str == null ? new char[0] : str.ToCharArray();
        }

        /// <summary>
        /// 将<paramref name="str"/>的指定子串转字符数组，<paramref name="str"/>为null时返回空数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex">子串开始下标</param>
        /// <param name="length">子串长度</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/>或<paramref name="length"/>超出范围</exception>
        /// <returns></returns>
        public static char[] ToCharArraySafe(this string str, int startIndex, int length)
        {
            return str == null ? new char[0] : str.ToCharArray(startIndex, length);
        }
        #endregion

        #endregion

        #region 格式化字符串
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, object arg0, IFormatProvider provider = null)
        {
            return string.Format(provider, format, arg0);
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, object arg0, object arg1, IFormatProvider provider = null)
        {
            return string.Format(provider, format, arg0, arg1);
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, object arg0, object arg1, object arg2, IFormatProvider provider = null)
        {
            return string.Format(provider, format, arg0, arg1, arg2);
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, IFormatProvider provider = null, params object[] args)
        {
            return string.Format(provider, format, args);
        }
        #endregion

        #region 反转字符串
        /// <summary>
        /// 反转字符串，当<paramref name="str"/>为null时返回<paramref name="nullValue"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string Reverse(this string str, string nullValue = "")
        {
            if (str == null) return nullValue;
            if (str.Length < 2) return str;

            var array = str.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
        #endregion

        #region 长度
        /// <summary>
        /// 获取字符串长度，当字符串为null时返回<paramref name="nullValue"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static int LengthNull(this string str, int nullValue = 0)
        {
            return str == null ? nullValue : str.Length;
        }
        #endregion

#if IS_FRAMEWORK || IS_NETCOREAPP2
        #region 安全转换
        /// <summary>
        /// 使用<paramref name="culture"/>将<paramref name="str"/>转小写，<paramref name="str"/>或<paramref name="culture"/>为null时返回<paramref name="nullValue"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="culture"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string ToLowerSafe(this string str, CultureInfo culture, string nullValue = "")
        {
            return (str == null || culture == null) ? nullValue : str.ToLower(culture);
        }

        /// <summary>
        /// 使用<paramref name="culture"/>将<paramref name="str"/>转大写，<paramref name="str"/>或<paramref name="culture"/>为null时返回<paramref name="nullValue"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="culture"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string ToUpperSafe(this string str, CultureInfo culture, string nullValue = "")
        {
            return (str == null || culture == null) ? nullValue : str.ToUpper(culture);
        }
        #endregion
#endif
    }
}
