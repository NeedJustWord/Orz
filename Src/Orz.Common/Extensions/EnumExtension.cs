using System;
using System.ComponentModel;

namespace Orz.Common.Extensions
{
	/// <summary>
	/// Enum扩展方法
	/// </summary>
	public static class EnumExtension
	{
#if !IS_NETCOREAPP1
		/// <summary>
		/// 获取枚举值的说明信息，没有的话返回枚举值字符串
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetDescription(this Enum value)
		{
			var name = value.ToString();
			var attributes = value.GetType().GetField(name).GetCustomAttributesSafe<DescriptionAttribute>(false);
			return attributes.Length > 0 ? attributes[0].Description : name;
		}
#endif
	}
}
