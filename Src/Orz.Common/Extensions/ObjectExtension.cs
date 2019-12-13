using System;

namespace Orz.Common.Extensions
{
    /// <summary>
    /// <see cref="object"/>扩展方法
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 用<see cref="object"/>的ReferenceEquals方法判断是否为null
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T target)
        {
            return ReferenceEquals(target, null);
        }

        /// <summary>
        /// 用<see cref="object"/>的ReferenceEquals方法判断是否不为null
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this T target)
        {
            return !ReferenceEquals(target, null);
        }

        /// <summary>
        /// 如果<paramref name="target"/>为null，则返回<paramref name="defaultValue"/>，否则返回<paramref name="target"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T IfNull<T>(this T target, T defaultValue)
        {
            return ReferenceEquals(target, null) ? defaultValue : target;
        }

        /// <summary>
        /// 如果<paramref name="target"/>为null，则返回<paramref name="func"/>的结果，否则返回<paramref name="target"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="func"></param>
        /// <exception cref="ArgumentNullException"><paramref name="func"/>为null</exception>
        /// <returns></returns>
        public static T IfNull<T>(this T target, Func<T> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            return ReferenceEquals(target, null) ? func() : target;
        }
    }
}
