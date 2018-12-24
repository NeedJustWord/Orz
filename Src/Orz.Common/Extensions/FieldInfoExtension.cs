using System;
using System.Reflection;

namespace Orz.Common.Extensions
{
	/// <summary>
	/// FieldInfo扩展方法
	/// </summary>
	public static class FieldInfoExtension
	{
		/// <summary>
		/// 安全获取自定义特性数组
		/// </summary>
		/// <typeparam name="TAttribute"></typeparam>
		/// <param name="fieldInfo"></param>
		/// <param name="inherit">是否搜索继承的特性</param>
		/// <returns></returns>
		public static TAttribute[] GetCustomAttributesSafe<TAttribute>(this FieldInfo fieldInfo, bool inherit) where TAttribute : Attribute
		{
			return fieldInfo == null ? new TAttribute[0] : (TAttribute[])fieldInfo.GetCustomAttributes(typeof(TAttribute), inherit);
		}
	}
}
