using System;

#if IS_FRAMEWORK || IS_NETCOREAPP2
using System.ComponentModel;
#endif

namespace Orz.Common.Extensions
{
    /// <summary>
    /// <see cref="Enum"/>扩展方法
    /// </summary>
    public static class EnumExtension
    {
#if IS_FRAMEWORK || IS_NETCOREAPP2
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
