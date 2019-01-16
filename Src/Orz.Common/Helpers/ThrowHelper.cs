using System;

namespace Orz.Common.Helpers
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
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">检查对象</param>
		public static void CheckArgumentNull<T>(T obj)
		{
			if (obj == null) throw new ArgumentNullException();
		}

		/// <summary>
		/// 如果<paramref name="obj"/>为null，则抛出<see cref="ArgumentNullException"/>异常
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">检查对象</param>
		/// <param name="paramName">参数名</param>
		public static void CheckArgumentNull<T>(T obj, string paramName)
		{
			if (obj == null) throw new ArgumentNullException(paramName);
		}

		/// <summary>
		/// 如果<paramref name="obj"/>为null，则抛出<see cref="ArgumentNullException"/>异常
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">检查对象</param>
		/// <param name="paramName">参数名</param>
		/// <param name="message">异常消息</param>
		public static void CheckArgumentNull<T>(T obj, string paramName, string message)
		{
			if (obj == null) throw new ArgumentNullException(paramName, message);
		}
		#endregion
	}
}
