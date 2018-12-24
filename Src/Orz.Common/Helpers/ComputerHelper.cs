using System;

namespace Orz.Common.Helpers
{
	/// <summary>
	/// 电脑信息辅助类
	/// </summary>
	public static class ComputerHelper
	{
		/// <summary>
		/// 判断是否在64位操作系统上运行
		/// </summary>
		public static bool Is64BitOperatingSystem => Environment.Is64BitOperatingSystem;

		/// <summary>
		/// 判断是否在64位进程中运行
		/// </summary>
		public static bool Is64BitProcess => Environment.Is64BitProcess;
	}
}
