using System;
using System.ComponentModel;
using Orz.Common.Extensions;

#if IS_NETCOREAPP1
using System.Reflection;
#endif

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

        /// <summary>
        /// 根据<see cref="DescriptionAttribute"/>说明或枚举名获取枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetEnumName<T>(string description)
        {
            foreach (var field in typeof(T).GetFields())
            {
                var array = field.GetCustomAttributesSafe<DescriptionAttribute>(false);
                if ((array.Length > 0 && array[0].Description == description) || field.Name == description) return (T)field.GetValue(null);
            }
            throw new ArgumentException($"未能找到对应的枚举：{description}", nameof(description));
        }
    }
}
