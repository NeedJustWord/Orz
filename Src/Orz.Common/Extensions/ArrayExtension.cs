using System;
using System.Collections.Generic;

namespace Orz.Common.Extensions
{
	/// <summary>
	/// Array扩展方法
	/// </summary>
	public static class ArrayExtension
	{
		#region 元素总数
		/// <summary>
		/// 获取Array的所有维数中元素的总数，为null时返回nullValue
		/// </summary>
		/// <param name="array"></param>
		/// <param name="nullValue"></param>
		/// <returns></returns>
		public static int LengthNull(this Array array, int nullValue = 0)
		{
			return array == null ? nullValue : array.Length;
		}

		/// <summary>
		/// 获取Array的所有维数中元素的总数，为null时返回nullValue
		/// </summary>
		/// <param name="array"></param>
		/// <param name="nullValue"></param>
		/// <returns></returns>
		public static long LongLengthNull(this Array array, long nullValue = 0)
		{
			return array == null ? nullValue : array.LongLength;
		}
		#endregion

		#region 为空判断
		/// <summary>
		/// 是否为空数组,null不算空数组
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static bool IsEmpty(this Array array)
		{
			return array != null && array.Length == 0;
		}

		/// <summary>
		/// 是否为null或空数组
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static bool IsNullOrEmpty(this Array array)
		{
			return array == null || array.Length == 0;
		}
		#endregion

		#region 范围判断
		/// <summary>
		/// 判断index是否在数组范围内
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static bool WithinIndex(this Array array, int index)
		{
			return array != null && index > -1 && index < array.Length;
		}

		/// <summary>
		/// 判断index是否在数组范围内
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static bool WithinIndex(this Array array, long index)
		{
			return array != null && index > -1 && index < array.LongLength;
		}

		/// <summary>
		/// 判断在array中指定维数dimension的指定索引index是否存在
		/// </summary>
		/// <param name="array"></param>
		/// <param name="dimension"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static bool WithinIndex(this Array array, int dimension, int index)
		{
			return array != null && dimension > -1 && dimension < array.Rank && index >= array.GetLowerBound(dimension) && index <= array.GetUpperBound(dimension);
		}
		#endregion

		#region Clear
		/// <summary>
		/// 将数组全部设置成默认值
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static Array ClearAll(this Array array)
		{
			if (array != null) Array.Clear(array, 0, array.Length);
			return array;
		}

		/// <summary>
		/// 将数组全部设置成指定默认值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static T[] ClearAll<T>(this T[] array, T defaultValue = default(T))
		{
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = defaultValue;
				}
			}
			return array;
		}

		/// <summary>
		/// 将指定索引的值设置成默认值
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static Array ClearAt(this Array array, int index)
		{
			if (array.WithinIndex(index)) Array.Clear(array, index, 1);
			return array;
		}

		/// <summary>
		/// 将指定索引的值设置成指定默认值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		/// <param name="index"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static T[] ClearAt<T>(this T[] array, int index, T defaultValue = default(T))
		{
			if (array.WithinIndex(index)) array[index] = defaultValue;
			return array;
		}
		#endregion

		#region Combine
		/// <summary>
		/// 合并两个数组并返回合并后的新数组,combineWith在arrayToCombine前面。
		/// 如果combineWith和arrayToCombine都为null则返回一个空数组。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="combineWith"></param>
		/// <param name="arrayToCombine"></param>
		/// <returns></returns>
		public static T[] CombineArray<T>(this T[] combineWith, T[] arrayToCombine)
		{
			if (combineWith == null && arrayToCombine == null) return new T[0];

			T[] result;
			if (combineWith == null)
			{
				result = new T[arrayToCombine.Length];
				Array.Copy(arrayToCombine, 0, result, 0, arrayToCombine.Length);
				return result;
			}

			if (arrayToCombine == null)
			{
				result = new T[combineWith.Length];
				Array.Copy(combineWith, 0, result, 0, combineWith.Length);
				return result;
			}

			result = new T[combineWith.Length + arrayToCombine.Length];
			Array.Copy(combineWith, 0, result, 0, combineWith.Length);
			Array.Copy(arrayToCombine, 0, result, combineWith.Length, arrayToCombine.Length);
			return result;
		}
		#endregion

		#region BlockCopy
		/// <summary>
		/// 块复制
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		/// <param name="index">起始下标</param>
		/// <param name="length">复制长度</param>
		/// <param name="padToLength">长度不足时是否填充默认值来达到指定的总长度length，默认不填充</param>
		/// <exception cref="ArgumentNullException">array为null</exception>
		/// <exception cref="ArgumentOutOfRangeException">index超出array的下标范围</exception>
		/// <exception cref="ArgumentException">length小于1</exception>
		/// <returns></returns>
		public static T[] BlockCopy<T>(this T[] array, int index, int length, bool padToLength = false)
		{
			if (array == null) throw new ArgumentNullException(nameof(array));
			if (index < 0 || index >= array.Length) throw new ArgumentOutOfRangeException(nameof(index));
			if (length < 1) throw new ArgumentException($"{nameof(length)}小于1");

			int subLength = length;
			T[] result = null;

			if (array.Length < index + length)
			{
				subLength = array.Length - index;
				if (padToLength) result = new T[length];
			}

			if (result == null) result = new T[subLength];
			Array.Copy(array, index, result, 0, subLength);
			return result;
		}

		/// <summary>
		/// 每count个元素复制成一个数组，返回数组集合
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		/// <param name="count"></param>
		/// <param name="padToLength">长度不足时是否填充默认值来达到指定的总长度count，默认不填充</param>
		/// <returns></returns>
		public static IEnumerable<T[]> BlockCopy<T>(this T[] array, int count, bool padToLength = false)
		{
			if (array != null && count > 0)
			{
				for (int i = 0; i < array.Length; i += count)
				{
					yield return array.BlockCopy(i, count, padToLength);
				}
			}
		}
		#endregion
	}
}
