﻿using System.Collections.Generic;
using Orz.Common.Extensions;

namespace System.Linq
{
	/// <summary>
	/// Linq扩展
	/// </summary>
	public static class EnumerableExtension
	{
		/// <summary>
		/// 将<see cref="IEnumerable{T}"/>转换成<see cref="Dictionary{TKey, TValue}"/>，其中T是一个<see cref="KeyValuePair{TKey, TValue}"/>
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="keyValues"></param>
		/// <param name="replaceExisted">是否替换已存在的<typeparamref name="TKey"/></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValues, bool replaceExisted = false, IEqualityComparer<TKey> comparer = null)
		{
			Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>(comparer);
			result.AddRange(keyValues, replaceExisted);
			return result;
		}
	}
}
