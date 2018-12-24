using System;

namespace Orz.Common.Helpers
{
	/// <summary>
	/// 时间辅助类
	/// </summary>
	public static class DateTimeHelper
	{
		#region 时间戳
		/// <summary>
		/// Unix时间戳(秒格式)转换成DateTime
		/// </summary>
		/// <param name="seconds">秒</param>
		/// <returns></returns>
		public static DateTime FromUnixTimeSeconds(double seconds)
		{
			return GetUnixStartLocalTime().AddSeconds(seconds);
		}

		/// <summary>
		/// Unix时间戳(毫秒格式)转换成DateTime
		/// </summary>
		/// <param name="milliseconds">毫秒</param>
		/// <returns></returns>
		public static DateTime FromUnixTimeMilliseconds(double milliseconds)
		{
			return GetUnixStartLocalTime().AddMilliseconds(milliseconds);
		}

		/// <summary>
		/// DateTime转换成Unix时间戳(秒格式)
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public static double ToUnixTimeSeconds(DateTime time)
		{
			return (time - GetUnixStartLocalTime()).TotalSeconds;
		}

		/// <summary>
		/// DateTime转换成Unix时间戳(毫秒格式)
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public static double ToUnixTimeMilliseconds(DateTime time)
		{
			return (time - GetUnixStartLocalTime()).TotalMilliseconds;
		}

		/// <summary>
		/// 获取Unix时间戳开始时间对应的本地时间
		/// </summary>
		/// <returns></returns>
		private static DateTime GetUnixStartLocalTime()
		{
			return TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
			//#if HaveTimeZoneInfo
			//			return TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
			//#else
			//			return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			//#endif
		}
		#endregion

		#region 时间操作
		/// <summary>
		/// 获取当天的开始时间
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static DateTime GetStartTime(DateTime value)
		{
			return value.Date;
		}

		/// <summary>
		/// 获取当天的结束时间
		/// </summary>
		/// <param name="value"></param>
		/// <param name="timePrecision">精度</param>
		/// <returns></returns>
		public static DateTime GetEndTime(DateTime value, TimePrecision timePrecision = TimePrecision.Second)
		{
			switch (timePrecision)
			{
				case TimePrecision.Tick:
					return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999).AddTicks(9999);
				case TimePrecision.Microsecond:
					return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999).AddTicks(9990);
				case TimePrecision.Millisecond:
					return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999);
				case TimePrecision.Second:
				default:
					return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
			}
		}

		/// <summary>
		/// 获取当月的第一天
		/// </summary>
		/// <param name="value"></param>
		/// <param name="keepTime">是否保留时间</param>
		/// <returns></returns>
		public static DateTime GetFirstDayOfMonth(DateTime value, bool keepTime = false)
		{
			var result = value.AddDays(1 - value.Day);
			return keepTime ? result : result.Date;
		}

		/// <summary>
		/// 获取当月的最后一天
		/// </summary>
		/// <param name="value"></param>
		/// <param name="keepTime">是否保留时间</param>
		/// <returns></returns>
		public static DateTime GetLastDayOfMonth(DateTime value, bool keepTime = false)
		{
			var daysInMonth = DateTime.DaysInMonth(value.Year, value.Month);
			var result = value.AddDays(daysInMonth - value.Day);
			return keepTime ? result : result.Date;
		}
		#endregion
	}

	/// <summary>
	/// 时间精度
	/// </summary>
	public enum TimePrecision
	{
		/// <summary>
		/// Tick，即100纳秒
		/// </summary>
		Tick,
		/// <summary>
		/// 微秒
		/// </summary>
		Microsecond,
		/// <summary>
		/// 毫秒
		/// </summary>
		Millisecond,
		/// <summary>
		/// 秒
		/// </summary>
		Second,
	}
}
