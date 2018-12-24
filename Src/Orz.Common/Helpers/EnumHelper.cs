using System;

namespace Orz.Common.Helpers
{
	/// <summary>
	/// 枚举辅助类
	/// </summary>
	public static class EnumHelper
	{
		/// <summary>
		/// 获取枚举值数组
		/// </summary>
		/// <typeparam name="TEnum">枚举类型</typeparam>
		/// <returns></returns>
		public static TEnum[] GetEnumValues<TEnum>() where TEnum : struct
		{
			return (TEnum[])Enum.GetValues(typeof(TEnum));
		}
	}
}
