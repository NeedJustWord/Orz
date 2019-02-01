#if IS_FRAMEWORK || IS_NETCOREAPP1
namespace System
{
	/// <summary>
	/// 
	/// </summary>
	public static class StringExtension
	{
		#region 分隔字符串
		/// <summary>
		/// 分隔字符串
		/// </summary>
		/// <param name="str"></param>
		/// <param name="separator"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static string[] Split(this string str, string separator, StringSplitOptions options = StringSplitOptions.None)
		{
			return str.Split(new string[] { separator }, options);
		}
		#endregion
	}
}
#endif