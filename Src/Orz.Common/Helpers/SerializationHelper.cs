using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#if IS_FRAMEWORK
using System.Runtime.Serialization.Formatters.Soap;
#endif

namespace Orz.Common.Helpers
{
	/// <summary>
	/// 序列化和反序列化辅助类
	/// </summary>
	/// <remarks>
	/// 对象序列化是把对象序列化转化为string类型，对象反序列化是把对象从string类型反序列化转化为其源类型。
	/// </remarks>
	public static class SerializationHelper
	{
		/// <summary>
		/// 将对象实例序列化成字符串
		/// </summary>
		/// <param name="graph"></param>
		/// <param name="formatterType"></param>
		/// <exception cref="ArgumentNullException">graph为null</exception>
		/// <exception cref="NotSupportedException"></exception>
		/// <returns></returns>
		public static string SerializeObject(object graph, SerializationFormatterType formatterType)
		{
			if (graph == null) throw new ArgumentNullException(nameof(graph));

			if (SerializationFormatterType.Xml == formatterType) return XmlHelper.SerializeObject(graph);
			if (SerializationFormatterType.Json == formatterType) return JsonHelper.SerializeObject(graph);

			var formatter = GetFormatter(formatterType);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				formatter.Serialize(memoryStream, graph);
				byte[] arrGraph = memoryStream.ToArray();
				return Convert.ToBase64String(arrGraph);
			}
		}

		/// <summary>
		/// 将对象实例序列化到文件里
		/// </summary>
		/// <param name="graph"></param>
		/// <param name="formatterType"></param>
		/// <param name="path"></param>
		/// <exception cref="ArgumentNullException">graph为null</exception>
		/// <exception cref="NotSupportedException"></exception>
		/// <returns></returns>
		public static bool SerializeObjectToFile(object graph, SerializationFormatterType formatterType, string path)
		{
			if (graph == null) throw new ArgumentNullException(nameof(graph));

			if (SerializationFormatterType.Xml == formatterType) return XmlHelper.SerializeObjectToFile(graph, path);
			if (SerializationFormatterType.Json == formatterType) return JsonHelper.SerializeObjectToFile(graph, path);

			var formatter = GetFormatter(formatterType);
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				formatter.Serialize(fileStream, graph);
				fileStream.Flush();
				return true;
			}
		}

		/// <summary>
		/// 将字符串反序列化成对象实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serializedGraph"></param>
		/// <param name="formatterType"></param>
		/// <param name="binder"></param>
		/// <exception cref="ArgumentException">serializedGraph为null或空白字符串</exception>
		/// <exception cref="NotSupportedException"></exception>
		/// <returns></returns>
		public static T DeserializeObject<T>(string serializedGraph, SerializationFormatterType formatterType, SerializationBinder binder = null)
		{
			if (string.IsNullOrWhiteSpace(serializedGraph)) throw new ArgumentException($"{nameof(serializedGraph)} IsNullOrWhiteSpace");

			if (SerializationFormatterType.Xml == formatterType) return XmlHelper.DeserializeObject<T>(serializedGraph);
			if (SerializationFormatterType.Json == formatterType) return JsonHelper.DeserializeObject<T>(serializedGraph);

			var formatter = GetFormatter(formatterType);
			if (binder != null) formatter.Binder = binder;

			byte[] arrGraph = Convert.FromBase64String(serializedGraph);
			using (MemoryStream memoryStream = new MemoryStream(arrGraph))
			{
				return (T)formatter.Deserialize(memoryStream);
			}
		}

		/// <summary>
		/// 将文件内容反序列化成对象实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path"></param>
		/// <param name="formatterType"></param>
		/// <param name="binder"></param>
		/// <exception cref="FileNotFoundException">文件不存在</exception>
		/// <exception cref="NotSupportedException"></exception>
		/// <returns></returns>
		public static T DeserializeObjectFromFile<T>(string path, SerializationFormatterType formatterType, SerializationBinder binder = null)
		{
			if (!File.Exists(path)) throw new FileNotFoundException(nameof(path));

			if (SerializationFormatterType.Xml == formatterType) return XmlHelper.DeserializeObjectFromFile<T>(path);
			if (SerializationFormatterType.Json == formatterType) return JsonHelper.DeserializeObjectFromFile<T>(path);

			var formatter = GetFormatter(formatterType);
			if (binder != null) formatter.Binder = binder;

			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				return (T)formatter.Deserialize(fileStream);
			}
		}

		/// <summary>
		/// 通过序列化复制对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="graph"></param>
		/// <exception cref="ArgumentNullException">graph为null</exception>
		/// <returns></returns>
		public static T CloneObject<T>(T graph)
		{
			//if (graph == null || string.IsNullOrEmpty(graph.ToString())) throw new ArgumentNullException(nameof(graph));
			if (graph == null) throw new ArgumentNullException(nameof(graph));

			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(memoryStream, graph);

				memoryStream.Position = 0;
				return (T)formatter.Deserialize(memoryStream);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="formatterType"></param>
		/// <exception cref="NotSupportedException">不支持的序列化类型</exception>
		/// <returns></returns>
		private static IFormatter GetFormatter(SerializationFormatterType formatterType)
		{
			switch (formatterType)
			{
				case SerializationFormatterType.Binary:
					return new BinaryFormatter();
#if IS_FRAMEWORK
				case SerializationFormatterType.Soap:
					return new SoapFormatter();
#endif
				default:
					throw new NotSupportedException();
			}
		}
	}

	/// <summary>
	/// 序列化格式编码
	/// </summary>
	public enum SerializationFormatterType
	{
		/// <summary>
		/// 二进制消息格式编码
		/// </summary>
		Binary = 0,
		/// <summary>
		/// Xml消息格式编码
		/// </summary>
		Xml = 1,
		/// <summary>
		/// Json消息格式编码
		/// </summary>
		Json = 2,
#if IS_FRAMEWORK
		/// <summary>
		/// SOAP消息格式编码
		/// </summary>
		Soap = 3,
#endif
	}
}
