using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Orz.Common.Helpers
{
	/// <summary>
	/// Xml辅助类
	/// </summary>
	public static class XmlHelper
	{
		#region 泛型序列化
		/// <summary>
		/// 将实例序列化成xml字符串
		/// </summary>
		/// <typeparam name="T">序列化类型</typeparam>
		/// <param name="obj">序列化实例</param>
		/// <param name="encoding">为null时使用UTF8编码</param>
		/// <returns></returns>
		public static string SerializeObject<T>(T obj, Encoding encoding = null)
		{
			if (encoding == null) encoding = Encoding.UTF8;
			//todo:使用encoding

			using (StringWriter stringWriter = new StringWriter())
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				xmlSerializer.Serialize(stringWriter, obj);
				stringWriter.Flush();
				return stringWriter.ToString();
			}
		}

		/// <summary>
		/// 将实例序列化成xml文件
		/// </summary>
		/// <typeparam name="T">序列化类型</typeparam>
		/// <param name="obj">序列化实例</param>
		/// <param name="path">xml文件路径</param>
		/// <param name="encoding">为null时使用UTF8编码</param>
		/// <returns></returns>
		public static bool SerializeObjectToFile<T>(T obj, string path, Encoding encoding = null)
		{
			if (encoding == null) encoding = Encoding.UTF8;
			//todo:使用encoding

			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				xmlSerializer.Serialize(fileStream, obj);
				fileStream.Flush();
				return true;
			}
		}
		#endregion

		#region 泛型反序列化
		/// <summary>
		/// 将xml字符串反序列化成实例
		/// </summary>
		/// <typeparam name="T">反序列化类型</typeparam>
		/// <param name="xml">xml字符串</param>
		/// <param name="encoding">为null时使用UTF8编码</param>
		/// <exception cref="ArgumentException"><paramref name="xml"/>为null或空白字符串</exception>
		/// <returns></returns>
		public static T DeserializeObject<T>(string xml, Encoding encoding = null)
		{
			if (string.IsNullOrWhiteSpace(xml)) throw new ArgumentException($"{nameof(xml)}为null或空白字符串");

			if (encoding == null) encoding = Encoding.UTF8;
			//todo:使用encoding

			using (StringReader stringReader = new StringReader(xml))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				return (T)xmlSerializer.Deserialize(stringReader);
			}
		}

		/// <summary>
		/// 将xml文件反序列化成实例
		/// </summary>
		/// <typeparam name="T">反序列化类型</typeparam>
		/// <param name="path">xml文件路径</param>
		/// <param name="encoding">为null时使用UTF8编码</param>
		/// <exception cref="FileNotFoundException"><paramref name="path"/>指定的文件不存在</exception>
		/// <returns></returns>
		public static T DeserializeObjectFromFile<T>(string path, Encoding encoding = null)
		{
			if (!File.Exists(path)) throw new FileNotFoundException($"{nameof(path)}指定的文件不存在", path);

			if (encoding == null) encoding = Encoding.UTF8;
			//todo:使用encoding

			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				return (T)xmlSerializer.Deserialize(fileStream);
			}
		}
		#endregion
	}
}
