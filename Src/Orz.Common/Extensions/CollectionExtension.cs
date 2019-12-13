using System.Collections;
using System.Collections.Generic;

namespace Orz.Common.Extensions
{
    /// <summary>
    /// <see cref="ICollection"/>和<see cref="ICollection{T}"/>的扩展方法
    /// </summary>
    public static class CollectionExtension
    {
        #region 数量
        /// <summary>
        /// 获取集合的数量，为null时返回<paramref name="nullValue"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static int CountNull<T>(this ICollection<T> collection, int nullValue = 0)
        {
            return collection == null ? nullValue : collection.Count;
        }

        /// <summary>
        /// 获取集合的数量，为null时返回<paramref name="nullValue"/>
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static int CountNull(this ICollection collection, int nullValue = 0)
        {
            return collection == null ? nullValue : collection.Count;
        }
        #endregion
    }
}
