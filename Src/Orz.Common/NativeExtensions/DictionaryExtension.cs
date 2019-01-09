#if IS_FRAMEWORK || IS_NETCOREAPP1
namespace System.Collections.Generic
{
	/// <summary>
	/// 
	/// </summary>
	public static class DictionaryExtension
	{
		/// <summary>
		/// 从<paramref name="dictionary"/>里移除指定<paramref name="key"/>
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