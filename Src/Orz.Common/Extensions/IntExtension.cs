namespace Orz.Common.Extensions
{
    /// <summary>
    /// <see cref="int"/>扩展方法
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// 获取位置对应的索引
        /// </summary>
        /// <param name="position">位置，从1开始</param>
        /// <returns></returns>
        public static int GetArrayIndex(this int position)
        {
            return position - 1;
        }
    }
}
