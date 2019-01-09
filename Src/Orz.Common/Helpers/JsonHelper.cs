using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Orz.Common.Helpers
{
	/// <summary>
	/// Json辅助类
	/// </summary>
	public static class JsonHelper
	{
		#region 序列化
		/// <summary>
		/// 将实例序列化成json字符串
		/// </summary>
		/// <typeparam name="T">序列化类型</typeparam>
		/// <param name="value">序列化实例</param>
		/// <param name="dateFormatString">时间的格式化字符串，为null表示使用默认的格式化字符串</param>
		/// <returns></returns>
		public static string SerializeObject<T>(T value, string dateFormatString = DateTimeFormatString.DateTimeStringUsual)
		{
			var settings = CreateJsonSerializerSettings(dateFormatString);
			return JsonConvert.SerializeObject(value, settings);
		}

		/// <summary>
		/// 将实例序列化成json文件
		/// </summary>
		/// <typeparam name="T">序列化类型</typeparam>
		/// <param name="value">序列化实例</param>
		/// <param name="path">json文件路径</param>
		/// <param name="dateFormatString">时间的格式化字符串，为null表示使用默认的格式化字符串</param>
		/// <param name="encoding">为null时使用UTF8编码</param>
		/// <returns></returns>
		public static bool SerializeObjectToFile<T>(T value, string path, string dateFormatString = DateTimeFormatString.DateTimeStringUsual, Encoding encoding = null)
		{
			var json = SerializeObject(value, dateFormatString);
			File.WriteAllText(path, json, encoding ?? Encoding.UTF8);
			return true;
		}
		#endregion

		#region 反序列化
		/// <summary>
		/// 将json字符串反序列化成object的实例
		/// </summary>
		/// <param name="json">json字符串</param>
		/// <param name="type">反序列化类型</param>
		/// <param name="dateFormatString">时间的格式化字符串，为null表示使用默认的格式化字符串</param>
		/// <returns></returns>
		public static object DeserializeObject(string json, Type type = null, string dateFormatString = DateTimeFormatString.DateTimeStringUsual)
		{
			var settings = CreateJsonSerializerSettings(dateFormatString);
			return JsonConvert.DeserializeObject(json, type, settings);
		}
		#endregion

		#region 泛型反序列化
		/// <summary>
		/// 将json字符串反序列化成指定类型的实例
		/// </summary>
		/// <typeparam name="T">反序列化类型</typeparam>
		/// <param name="json">json字符串</param>
		/// <param name="dateFormatString">时间的格式化字符串，为null表示使用默认的格式化字符串</param>
		/// <returns></returns>
		public static T DeserializeObject<T>(string json, string dateFormatString = DateTimeFormatString.DateTimeStringUsual)
		{
			var settings = CreateJsonSerializerSettings(dateFormatString);
			return JsonConvert.DeserializeObject<T>(json, settings);
		}

		/// <summary>
		/// 将json文件反序列化成指定类型的实例
		/// </summary>
		/// <typeparam name="T">反序列化类型</typeparam>
		/// <param name="path">json文件路径</param>
		/// <param name="dateFormatString">时间的格式化字符串，为null表示使用默认的格式化字符串</param>
		/// <param name="encoding">为null时使用UTF8编码</param>
		/// <exception cref="FileNotFoundException"><paramref name="path"/>指定的文件不存在</exception>
		/// <returns></returns>
		public static T DeserializeObjectFromFile<T>(string path, string dateFormatString = DateTimeFormatString.DateTimeStringUsual, Encoding encoding = null)
		{
			if (!File.Exists(path)) throw new FileNotFoundException($"{nameof(path)}指定的文件不存在", path);

			var json = File.ReadAllText(path, encoding ?? Encoding.UTF8);
			return DeserializeObject<T>(json, dateFormatString);
		}
		#endregion

		private static JsonSerializerSettings CreateJsonSerializerSettings(string dateFormatString)
		{
			return dateFormatString == null ? null : new JsonSerializerSettings()
			{
				DateFormatString = dateFormatString
			};
		}
	}
}
