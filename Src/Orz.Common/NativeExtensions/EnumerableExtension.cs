using System.Collections.Generic;
using Orz.Common.Extensions;

namespace System.Linq
{
    /// <summary>
    /// Linq扩展
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// 将<see cref="IEnumerable{T}"/>转换成<see cref="Dictionary{TKey, TValue}"/>，其中T是一个<see cref="KeyValuePair{TKey, TValue}"/>。
        /// <para>此方法不会判断<typeparamref name="TKey"/>是否存在。</para>
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="keyValues"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValues, IEqualityComparer<TKey> comparer = null)
        {
            return keyValues.ToDictionary(t => t.Key, t => t.Value, comparer);
        }

        /// <summary>
        /// 将<see cref="IEnumerable{T}"/>转换成<see cref="Dictionary{TKey, TValue}"/>，其中T是一个<see cref="KeyValuePair{TKey, TValue}"/>
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="keyValues"></param>
        /// <param name="replaceExisted">是否替换已存在的<typeparamref name="TKey"/></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValues, bool replaceExisted, IEqualityComparer<TKey> comparer = null)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>(comparer);
            result.AddRange(keyValues, replaceExisted);
            return result;
        }

        /// <summary>
        /// 对<paramref name="source"/>的每个元素执行<paramref name="action"/>操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null)
            {
                if (action == null)
                {
                    foreach (var item in source)
                    {
                        yield return item;
                    }
                }
                else
                {
                    foreach (var item in source)
                    {
                        action(item);
                        yield return item;
                    }
                }
            }
        }
    }
}
