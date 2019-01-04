using System.IO;
using System.Xml.Serialization;

namespace Orz.Common.Helpers
{
	/// <summary>
	/// Xml辅助类
	/// </summary>
	public static class XmlHelper
	{
		#region 序列化
		/// <summary>
		/// 将实例序列化成xml字符串
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string SerializeObject(object obj)
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
				xmlSerializer.Serialize(stringWriter, obj);
				stringWriter.Flush();
				return stringWriter.ToString();
			}
		}

		/// <summary>
		/// 将实例序列化成xml文件
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool SerializeObjectToFile(object obj, string path)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
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
		/// <typeparam name="T"></typeparam>
		/// <param name="xml"></param>
		/// <returns></returns>
		public static T DeserializeObject<T>(string xml)
		{
			using (StreamReader streamReader = new StreamReader(xml))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				return (T)xmlSerializer.Deserialize(streamReader);
			}
		}

		/// <summary>
		/// 将xml文件反序列化成实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path"></param>
		/// <exception cref="FileNotFoundException">文件不存在</exception>
		/// <returns></returns>
		public static T DeserializeObjectFromFile<T>(string path)
		{
			if (!File.Exists(path)) throw new FileNotFoundException(nameof(path));

			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				return (T)xmlSerializer.Deserialize(fileStream);
			}
		}
		#endregion
	}
}
