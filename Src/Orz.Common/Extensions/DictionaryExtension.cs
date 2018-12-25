using System;
using System.Collections.Generic;

namespace Orz.Common.Extensions
{
	/// <summary>
	/// Dictionary扩展方法
	/// </summary>
	public static class DictionaryExtension
	{
		#region Dictionary
		/// <summary>
		/// 添加键值对，已存在则直接返回
		/// </summary>
		/// <typeparam name="Tkey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Dictionary<Tkey, TValue> AddSafe<Tkey, TValue>(this Dictionary<Tkey, TValue> dict, Tkey key, TValue value)
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
		/// <returns></returns>
		public static Dictionary<Tkey, TValue> AddOrReplace<Tkey, TValue>(this Dictionary<Tkey, TValue> dict, Tkey key, TValue value)
		{
			if (dict == null) throw new ArgumentNullException(nameof(dict));

			dict[key] = value;
			return dict;
		}

		/// <summary>
		/// 获取指定key的值，key不存在则返回默认值
		/// </summary>
		/// <typeparam name="Tkey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static TValue GetValue<Tkey, TValue>(this Dictionary<Tkey, TValue> dict, Tkey key, TValue defaultValue = default(TValue))
		{
			if (dict == null) throw new ArgumentNullException(nameof(dict));

			return dict.ContainsKey(key) ? dict[key] : defaultValue;
		}

		/// <summary>
		/// 批量添加键值对
		/// </summary>
		/// <typeparam name="Tkey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="keyValues"></param>
		/// <param name="replaceExisted">是否替换已存在的Tkey</param>
		/// <returns></returns>
		public static Dictionary<Tkey, TValue> AddRange<Tkey, TValue>(this Dictionary<Tkey, TValue> dict, IEnumerable<KeyValuePair<Tkey, TValue>> keyValues, bool replaceExisted = false)
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

		#region IDictionary
		/// <summary>
		/// 添加键值对，已存在则直接返回
		/// </summary>
		/// <typeparam name="Tkey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
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
		/// <returns></returns>
		public static IDictionary<Tkey, TValue> AddOrReplace<Tkey, TValue>(this IDictionary<Tkey, TValue> dict, Tkey key, TValue value)
		{
			if (dict == null) throw new ArgumentNullException(nameof(dict));

			dict[key] = value;
			return dict;
		}

		/// <summary>
		/// 获取指定key的值，key不存在则返回默认值
		/// </summary>
		/// <typeparam name="Tkey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static TValue GetValue<Tkey, TValue>(this IDictionary<Tkey, TValue> dict, Tkey key, TValue defaultValue = default(TValue))
		{
			if (dict == null) throw new ArgumentNullException(nameof(dict));

			return dict.ContainsKey(key) ? dict[key] : defaultValue;
		}

		/// <summary>
		/// 批量添加键值对
		/// </summary>
		/// <typeparam name="Tkey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="keyValues"></param>
		/// <param name="replaceExisted">是否替换已存在的Tkey</param>
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
	}
}
