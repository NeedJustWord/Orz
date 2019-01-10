using System;

namespace Orz.Common.Helpers
{
	/// <summary>
	/// 时间辅助类
	/// </summary>
	public static class DateTimeHelper
	{
		#region 时间戳
		private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		#region UtcNow和Now的Unix时间戳
		/// <summary>
		/// 获取UtcNow对应的Unix时间戳(秒格式)
		/// </summary>
		/// <returns></returns>
		public static long UtcNowToUnixTimeSeconds()
		{
			return ToUnixTimeSeconds(DateTime.UtcNow);
		}

		/// <summary>
		/// 获取UtcNow对应的Unix时间戳(毫秒格式)
		/// </summary>
		/// <returns></returns>
		public static long UtcNowToUnixTimeMilliseconds()
		{
			return ToUnixTimeMilliseconds(DateTime.UtcNow);
		}

		/// <summary>
		/// 获取Now对应的Unix时间戳(秒格式)
		/// </summary>
		/// <returns></returns>
		public static long NowToUnixTimeSeconds()
		{
			return ToUnixTimeSeconds(DateTime.Now);
		}

		/// <summary>
		/// 获取Now对应的Unix时间戳(毫秒格式)
		/// </summary>
		/// <returns></returns>
		public static long NowToUnixTimeMilliseconds()
		{
			return ToUnixTimeMilliseconds(DateTime.Now);
		}
		#endregion

		#region DateTime与时间戳的转换
		/// <summary>
		/// Unix时间戳(秒格式)转换成DateTime
		/// </summary>
		/// <param name="seconds">秒</param>
		/// <param name="dateTimeType">返回的时间类型，默认返回当地时间</param>
		/// <returns></returns>
		public static DateTime FromUnixTimeSeconds(long seconds, DateTimeType dateTimeType = DateTimeType.Local)
		{
			return GetUnixStartDateTime(dateTimeType).AddSeconds(seconds);
		}

		/// <summary>
		/// Unix时间戳(毫秒格式)转换成DateTime
		/// </summary>
		/// <param name="milliseconds">毫秒</param>
		/// <param name="dateTimeType">返回的时间类型，默认返回当地时间</param>
		/// <returns></returns>
		public static DateTime FromUnixTimeMilliseconds(long milliseconds, DateTimeType dateTimeType = DateTimeType.Local)
		{
			return GetUnixStartDateTime(dateTimeType).AddMilliseconds(milliseconds);
		}

		/// <summary>
		/// DateTime转换成Unix时间戳(秒格式)
		/// </summary>
		/// <param name="time"></param>
		/// <param name="unspecifiedAs"><paramref name="time"/>的Kind属性为Unspecified时当作的时间类型，默认当作当地时间</param>
		/// <returns></returns>
		public static long ToUnixTimeSeconds(DateTime time, DateTimeType unspecifiedAs = DateTimeType.Local)
		{
			return (long)(ToUtcDateTime(time, unspecifiedAs) - Jan1st1970).TotalSeconds;
		}

		/// <summary>
		/// DateTime转换成Unix时间戳(毫秒格式)
		/// </summary>
		/// <param name="time"></param>
		/// <param name="unspecifiedAs"><paramref name="time"/>的Kind属性为Unspecified时当作的时间类型，默认当作当地时间</param>
		/// <returns></returns>
		public static long ToUnixTimeMilliseconds(DateTime time, DateTimeType unspecifiedAs = DateTimeType.Local)
		{
			return (long)(ToUtcDateTime(time, unspecifiedAs) - Jan1st1970).TotalMilliseconds;
		}

		/// <summary>
		/// 获取Unix开始时间
		/// </summary>
		/// <param name="dateTimeType">返回时间类型</param>
		/// <returns></returns>
		private static DateTime GetUnixStartDateTime(DateTimeType dateTimeType)
		{
			return dateTimeType == DateTimeType.Utc ? Jan1st1970 : TimeZoneInfo.ConvertTime(Jan1st1970, TimeZoneInfo.Utc, TimeZoneInfo.Local);
		}

		/// <summary>
		/// 将<paramref name="time"/>转换成Utc时间
		/// </summary>
		/// <param name="time">待转换时间</param>
		/// <param name="unspecifiedAs"><paramref name="time"/>的Kind属性为Unspecified时当作的时间类型</param>
		/// <returns></returns>
		private static DateTime ToUtcDateTime(DateTime time, DateTimeType unspecifiedAs)
		{
			if (time.Kind == DateTimeKind.Utc || (time.Kind == DateTimeKind.Unspecified && unspecifiedAs == DateTimeType.Utc)) return time;

			return TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local, TimeZoneInfo.Utc);
		}
		#endregion

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

	/// <summary>
	/// 时间类型
	/// </summary>
	public enum DateTimeType
	{
		/// <summary>
		/// Utc时间
		/// </summary>
		Utc = 1,
		/// <summary>
		/// 当地时间
		/// </summary>
		Local = 2,
	}
}
