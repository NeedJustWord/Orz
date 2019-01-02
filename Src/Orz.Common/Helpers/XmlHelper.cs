using System.IO;
using System.Xml.Serialization;

namespace Orz.Common.Helpers
{
	/// <summary>
	/// Xml辅助类
	/// </summary>
	public static class XmlHelper
	{
		/// <summary>
		/// 将实例序列化成xml字符串
		/// </summary>
		/// <param name="graph"></param>
		/// <returns></returns>
		public static string SerializeObject(object graph)
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				XmlSerializer xmlSerializer = new XmlSerializer(graph.GetType());
				xmlSerializer.Serialize(stringWriter, graph);
				stringWriter.Flush();
				return stringWriter.ToString();
			}
		}

		/// <summary>
		/// 将实例序列化成xml文件
		/// </summary>
		/// <param name="graph"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool SerializeObjectToFile(object graph, string path)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(graph.GetType());
				xmlSerializer.Serialize(fileStream, graph);
				fileStream.Flush();
				return true;
			}
		}

		/// <summary>
		/// 将xml字符串反序列化成实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serializedGraph"></param>
		/// <returns></returns>
		public static T DeserializeObject<T>(string serializedGraph)
		{
			using (StreamReader streamReader = new StreamReader(serializedGraph))
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
	}
}
