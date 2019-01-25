using System;
using System.Collections.Generic;
using System.Text;

namespace Orz.Common.Extensions
{
	/// <summary>
	/// <see cref="byte"/>扩展方法
	/// </summary>
	public static class ByteExtension
	{
		#region 转十六进制
		/// <summary>
		/// 将字节转换成十六进制的字符串
		/// </summary>
		/// <param name="value"></param>
		/// <param name="toUpper">是否大写</param>
		/// <returns></returns>
		public static string ToHex(this byte value, bool toUpper = true)
		{
			string format = toUpper ? "X2" : "x2";
			return value.ToString(format);
		}

		/// <summary>
		/// 将字节流转换成十六进制的字符串，为null时返回空串
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="toUpper">是否大写</param>
		/// <param name="separator">分隔符，为null时表示没有分隔符</param>
		/// <returns></returns>
		public static string ToHex(this IEnumerable<byte> bytes, bool toUpper = true, string separator = null)
		{
			if (bytes == null) return string.Empty;
			if (separator == null) separator = "";
			string format = toUpper ? "X2" : "x2";

			StringBuilder sb = new StringBuilder();
			foreach (var value in bytes)
			{
				sb.Append(value.ToString(format) + separator);
			}

			if (sb.Length > 0 && separator.Length > 0) sb.Remove(sb.Length - separator.Length, separator.Length);
			return sb.ToString();
		}
		#endregion

		#region 位运算
		/// <summary>
		/// 判断第<paramref name="index"/>位是否为1
		/// </summary>
		/// <param name="value"></param>
		/// <param name="index">0-7，从右数起</param>
		/// <returns></returns>
		public static bool IsBitOne(this byte value, int index)
		{
			return (value & (1 << index)) > 0;
		}

		/// <summary>
		/// 判断第<paramref name="index"/>位是否为0
		/// </summary>
		/// <param name="value"></param>
		/// <param name="index">0-7，从右数起</param>
		/// <returns></returns>
		public static bool IsBitZero(this byte value, int index)
		{
			return (value & (1 << index)) == 0;
		}

		/// <summary>
		/// 将第<paramref name="index"/>位设为1
		/// </summary>
		/// <param name="value"></param>
		/// <param name="index">0-7，从右数起</param>
		/// <returns></returns>
		public static byte SetBit(this byte value, int index)
		{
			value |= (byte)(1 << index);
			return value;
		}

		/// <summary>
		/// 将第<paramref name="index"/>位设为0
		/// </summary>
		/// <param name="value"></param>
		/// <param name="index">0-7，从右数起</param>
		/// <returns></returns>
		public static byte ClearBit(this byte value, int index)
		{
			value &= (byte)(255 - (1 << index));
			return value;
		}

		/// <summary>
		/// 将第<paramref name="index"/>位取反
		/// </summary>
		/// <param name="value"></param>
		/// <param name="index">0-7，从右数起</param>
		/// <returns></returns>
		public static byte ReverseBit(this byte value, int index)
		{
			value ^= (byte)(1 << index);
			return value;
		}
		#endregion

		#region 编码
		/// <summary>
		/// 将字节数组转换成base64编码的字符串，为null时返回空串
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public static string ToBase64String(this byte[] bytes)
		{
			return bytes == null ? string.Empty : Convert.ToBase64String(bytes);
		}

		/// <summary>
		/// 将字节数组转换成指定编码的字符串，为null时返回空串
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="encoding">为null时使用UTF8编码</param>
		/// <returns></returns>
		public static string ToEncodingString(this byte[] bytes, Encoding encoding = null)
		{
			if (bytes == null) return string.Empty;
			if (encoding == null) encoding = Encoding.UTF8;
			return encoding.GetString(bytes);
		}

		/// <summary>
		/// 将字节数组的子数组转换成指定编码的字符串，<paramref name="bytes"/>为null时返回空串
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="index">子数组开始索引</param>
		/// <param name="count">子数组长度</param>
		/// <param name="encoding">为null时使用UTF8编码</param>
		/// <returns></returns>
		public static string ToEncodingString(this byte[] bytes, int index, int count, Encoding encoding = null)
		{
			if (bytes == null) return string.Empty;
			if (encoding == null) encoding = Encoding.UTF8;
			return encoding.GetString(bytes, index, count);
		}
		#endregion
	}
}
