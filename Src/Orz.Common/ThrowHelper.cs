using System;

namespace Orz.Common
{
	/// <summary>
	/// 抛异常辅助类
	/// </summary>
	public static class ThrowHelper
	{
		#region ArgumentNullException
		/// <summary>
		/// 如果<paramref name="obj"/>为null，则抛出<see cref="ArgumentNullException"/>异常
		/// </summary>
		/// <typeparam name="T">检查对象类型</typeparam>
		/// <param name="obj">检查对象</param>
		public static void CheckArgumentNull<T>(T obj)
		{
			if (obj == null) throw new ArgumentNullException();
		}

		/// <summary>
		/// 如果<paramref name="obj"/>为null，则抛出<see cref="ArgumentNullException"/>异常
		/// </summary>
		/// <typeparam name="T">检查对象类型</typeparam>
		/// <param name="obj">检查对象</param>
		/// <param name="paramName">检查对象参数名</param>
		public static void CheckArgumentNull<T>(T obj, string paramName)
		{
			if (obj == null) throw new ArgumentNullException(paramName);
		}

		/// <summary>
		/// 如果<paramref name="obj"/>为null，则抛出<see cref="ArgumentNullException"/>异常
		/// </summary>
		/// <typeparam name="T">检查对象类型</typeparam>
		/// <param name="obj">检查对象</param>
		/// <param name="paramName">检查对象参数名</param>
		/// <param name="message">异常消息</param>
		public static void CheckArgumentNull<T>(T obj, string paramName, string message)
		{
			if (obj == null) throw new ArgumentNullException(paramName, message);
		}

		/// <summary>
		/// 如果<paramref name="str"/>为null或empty，则抛出<see cref="ArgumentNullException"/>异常
		/// </summary>
		/// <param name="str">检查对象</param>
		/// <param name="paramName">检查对象参数名</param>
		/// <param name="message">异常消息</param>
		public static void CheckArgumentNullOrEmpty(string str, string paramName, string message)
		{
			if (string.IsNullOrEmpty(str)) throw new ArgumentNullException(paramName, message);
		}

		/// <summary>
		/// 如果<paramref name="str"/>为null或whiteSpace，则抛出<see cref="ArgumentNullException"/>异常
		/// </summary>
		/// <param name="str">检查对象</param>
		/// <param name="paramName">检查对象参数名</param>
		/// <param name="message">异常消息</param>
		public static void CheckArgumentNullOrWhiteSpace(string str, string paramName, string message)
		{
			if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(paramName, message);
		}
		#endregion

		#region ArgumentOutOfRangeException
		/// <summary>
		/// 如果<paramref name="index"/>超出<paramref name="array"/>的索引范围，则抛出<see cref="ArgumentOutOfRangeException"/>异常。
		/// <para>此方法不会检查<paramref name="array"/>是否为null。</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array">数组</param>
		/// <param name="index">索引</param>
		/// <param name="indexParamName">索引参数名</param>
		public static void CheckArgumentOutOfRange<T>(T[] array, int index, string indexParamName)
		{
			if (index < 0 || index >= array.Length) throw new ArgumentOutOfRangeException(indexParamName);
		}

		/// <summary>
		/// 如果<paramref name="index"/>超出<paramref name="array"/>的索引范围，或者<paramref name="length"/>超出<paramref name="array"/>的长度范围，则抛出<see cref="ArgumentOutOfRangeException"/>异常。
		/// <para>此方法不会检查<paramref name="array"/>是否为null。</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array">数组</param>
		/// <param name="index">索引</param>
		/// <param name="length">长度</param>
		/// <param name="indexParamName">索引参数名</param>
		/// <param name="lengthParamName">长度参数名</param>
		/// <param name="lengthCanZero">长度的最小值是否可以为0，是0，否1</param>
		public static void CheckArgumentOutOfRange<T>(T[] array, int index, int length, string indexParamName, string lengthParamName, bool lengthCanZero = false)
		{
			if (index < 0 || index >= array.Length) throw new ArgumentOutOfRangeException(indexParamName);
			int minLength = lengthCanZero ? 0 : 1;
			if (length < minLength || index + length > array.Length) throw new ArgumentOutOfRangeException(lengthParamName);
		}
		#endregion
	}
}
