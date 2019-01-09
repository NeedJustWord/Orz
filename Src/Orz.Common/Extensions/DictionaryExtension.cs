﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Orz.Common.Extensions
{
	/// <summary>
	/// <see cref="IDictionary{TKey, TValue}"/>扩展方法
	/// </summary>
	public static class DictionaryExtension
	{
		#region Add
		/// <summary>
		/// 添加键值对，已存在则直接返回
		/// </summary>
		/// <typeparam name="Tkey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <exception cref="ArgumentNullException"><paramref name="dict"/>为null</exception>
		/// <returns></returns>
		public static IDictionary<Tkey, TValue> AddSafe<Tkey, TValue>(this IDictionary<Tkey, TValue> dict, Tkey key, TValue value)
		{
			if (dict == null) throw new ArgumentNullException(nameof(dict));

			if (!dict.ContainsKey(key)) dict.Add(key, value);
			return dict;
		}

		/// <summary>
		/// 添加或替换键值对
		/// </summary>
		/// <typeparam name="Tkey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <exception cref="ArgumentNullException"><paramref name="dict"/>为null</exception>
		/// <returns></returns>
		public static IDictionary<Tkey, TValue> AddOrReplace<Tkey, TValue>(this IDictionary<Tkey, TValue> dict, Tkey key, TValue value)
		{
			if (dict == null) throw new ArgumentNullException(nameof(dict));

			dict[key] = value;
			return dict;
		}

		/// <summary>
		/// 批量添加键值对
		/// </summary>
		/// <typeparam name="Tkey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="keyValues"></param>
		/// <param name="replaceExisted">是否替换已存在的<typeparamref name="Tkey"/></param>
		/// <exception cref="ArgumentNullException"><paramref name="dict"/>为null</exception>
		/// <returns></returns>
		public static IDictionary<Tkey, TValue> AddRange<Tkey, TValue>(this IDictionary<Tkey, TValue> dict, IEnumerable<KeyValuePair<Tkey, TValue>> keyValues, bool replaceExisted = false)
		{
			if (dict == null) throw new ArgumentNullException(nameof(dict));

			if (keyValues != null)
			{
				foreach (var item in keyValues)
				{
					if (replaceExisted || dict.ContainsKey(item.Key) == false) dict[item.Key] = item.Value;
				}
			}
			return dict;
		}
		#endregion

		#region Get
		/// <summary>
		/// 获取指定<paramref name="key"/>的值，<paramref name="key"/>不存在则返回默认值
		/// </summary>
		/// <typeparam name="Tkey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <exception cref="ArgumentNullException"><paramref name="dict"/>为null</exception>
		/// <returns></returns>
		public static TValue GetOrDefault<Tkey, TValue>(this IDictionary<Tkey, TValue> dict, Tkey key, TValue defaultValue = default(TValue))
		{
			if (dict == null) throw new ArgumentNullException(nameof(dict));

			return dict.ContainsKey(key) ? dict[key] : defaultValue;
		}

		/// <summary>
		/// 获取指定<paramref name="key"/>的值，<paramref name="key"/>不存在则抛出指定异常<paramref name="exception"/>
		/// </summary>
		/// <typeparam name="Tkey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="key"></param>
		/// <param name="exception"></param>
		/// <exception cref="ArgumentNullException"><paramref name="dict"/>为null</exception>
		/// <returns></returns>
		public static TValue GetOrThrow<Tkey, TValue>(this IDictionary<Tkey, TValue> dict, Tkey key, Exception exception)
		{
			if (dict == null) throw new ArgumentNullException(nameof(dict));

			if (dict.ContainsKey(key)) return dict[key];
			throw exception;
		}
		#endregion

		#region Sort
		/// <summary>
		/// 根据<typeparamref name="TKey"/>排序
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dict, IComparer<TKey> comparer = null)
		{
			if (dict == null) throw new ArgumentNullException(nameof(dict));

			return new SortedDictionary<TKey, TValue>(dict, comparer);
		}

		/// <summary>
		/// 根据<typeparamref name="TValue"/>排序
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <returns></returns>
		public static IDictionary<TKey, TValue> SortByValue<TKey, TValue>(this IDictionary<TKey, TValue> dict)
		{
			return dict.Sort().OrderBy(t => t.Value).ToDictionary();
		}
		#endregion
	}
}
