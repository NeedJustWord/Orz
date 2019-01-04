using System.Collections;
using System.Collections.Generic;

namespace Orz.Common.Extensions
{
	/// <summary>
	/// 
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

#if IS_FRAMEWORK || IS_NETCOREAPP1
namespace System.Collections.Generic
{
	/// <summary>
	/// 
	/// </summary>
	public static class CollectionExtension
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dictionary"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException(nameof(dictionary));
			}

			if (dictionary.TryGetValue(key, out value))
			{
				dictionary.Remove(key);
				return true;
			}

			value = default(TValue);
			return false;
		}
	}
}
#endif