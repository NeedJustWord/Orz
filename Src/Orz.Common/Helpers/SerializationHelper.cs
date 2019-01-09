using System;
using System.IO;

#if HAVE_IFORMATTER
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
#endif

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
#if HAVE_IFORMATTER
		/// <summary>
		/// 将对象实例序列化成字符串
		/// </summary>
		/// <param name="graph">序列化对象</param>
		/// <param name="formatterType">序列化格式编码</param>
		/// <param name="binder"></param>
		/// <exception cref="ArgumentNullException"><paramref name="graph"/>为null</exception>
		/// <exception cref="NotSupportedException"><paramref name="formatterType"/>为不支持的序列化类型</exception>
		/// <exception cref="Exception">序列化失败</exception>
		/// <returns></returns>
		public static string SerializeObject(object graph, SerializationFormatterType formatterType, SerializationBinder binder = null)
#else
		/// <summary>
		/// 将对象实例序列化成字符串
		/// </summary>
		/// <param name="graph">序列化对象</param>
		/// <param name="formatterType">序列化格式编码</param>
		/// <exception cref="ArgumentNullException"><paramref name="graph"/>为null</exception>
		/// <exception cref="NotSupportedException"><paramref name="formatterType"/>为不支持的序列化类型</exception>
		/// <exception cref="Exception">序列化失败</exception>
		/// <returns></returns>
		public static string SerializeObject(object graph, SerializationFormatterType formatterType)
#endif
		{
			if (graph == null) throw new ArgumentNullException(nameof(graph));

			if (SerializationFormatterType.Xml == formatterType) return XmlHelper.SerializeObject(graph);
			if (SerializationFormatterType.Json == formatterType) return JsonHelper.SerializeObject(graph);

#if HAVE_IFORMATTER
			var formatter = GetFormatter(formatterType, binder);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				formatter.Serialize(memoryStream, graph);
				byte[] arrGraph = memoryStream.ToArray();
				return Convert.ToBase64String(arrGraph);
			}
#endif
			throw new Exception("序列化失败");
		}

#if HAVE_IFORMATTER
		/// <summary>
		/// 将对象实例序列化到文件里
		/// </summary>
		/// <param name="graph">序列化对象</param>
		/// <param name="formatterType">序列化格式编码</param>
		/// <param name="path">序列化文件路径</param>
		/// <param name="binder"></param>
		/// <exception cref="ArgumentNullException"><paramref name="graph"/>为null</exception>
		/// <exception cref="NotSupportedException"><paramref name="formatterType"/>为不支持的序列化类型</exception>
		/// <exception cref="Exception">序列化失败</exception>
		/// <returns></returns>
		public static bool SerializeObjectToFile(object graph, SerializationFormatterType formatterType, string path, SerializationBinder binder = null)
#else
		/// <summary>
		/// 将对象实例序列化到文件里
		/// </summary>
		/// <param name="graph">序列化对象</param>
		/// <param name="formatterType">序列化格式编码</param>
		/// <param name="path">序列化文件路径</param>
		/// <exception cref="ArgumentNullException"><paramref name="graph"/>为null</exception>
		/// <exception cref="NotSupportedException"><paramref name="formatterType"/>为不支持的序列化类型</exception>
		/// <exception cref="Exception">序列化失败</exception>
		/// <returns></returns>
		public static bool SerializeObjectToFile(object graph, SerializationFormatterType formatterType, string path)
#endif
		{
			if (graph == null) throw new ArgumentNullException(nameof(graph));

			if (SerializationFormatterType.Xml == formatterType) return XmlHelper.SerializeObjectToFile(graph, path);
			if (SerializationFormatterType.Json == formatterType) return JsonHelper.SerializeObjectToFile(graph, path);

#if HAVE_IFORMATTER
			var formatter = GetFormatter(formatterType, binder);
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				formatter.Serialize(fileStream, graph);
				fileStream.Flush();
				return true;
			}
#endif
			throw new Exception("序列化失败");
		}

#if HAVE_IFORMATTER
		/// <summary>
		/// 将字符串反序列化成对象实例
		/// </summary>
		/// <typeparam name="T">反序列化类型</typeparam>
		/// <param name="serializedGraph">反序列化字符串</param>
		/// <param name="formatterType">序列化格式编码</param>
		/// <param name="binder"></param>
		/// <exception cref="ArgumentException"><paramref name="serializedGraph"/>为null或空白字符串</exception>
		/// <exception cref="NotSupportedException"><paramref name="formatterType"/>为不支持的序列化类型</exception>
		/// <exception cref="Exception">序列化失败</exception>
		/// <returns></returns>
		public static T DeserializeObject<T>(string serializedGraph, SerializationFormatterType formatterType, SerializationBinder binder = null)
#else
		/// <summary>
		/// 将字符串反序列化成对象实例
		/// </summary>
		/// <typeparam name="T">反序列化类型</typeparam>
		/// <param name="serializedGraph">反序列化字符串</param>
		/// <param name="formatterType">序列化格式编码</param>
		/// <exception cref="ArgumentException"><paramref name="serializedGraph"/>为null或空白字符串</exception>
		/// <exception cref="NotSupportedException"><paramref name="formatterType"/>为不支持的序列化类型</exception>
		/// <exception cref="Exception">序列化失败</exception>
		/// <returns></returns>
		public static T DeserializeObject<T>(string serializedGraph, SerializationFormatterType formatterType)
#endif
		{
			if (string.IsNullOrWhiteSpace(serializedGraph)) throw new ArgumentException($"{nameof(serializedGraph)}为null或空白字符串");

			if (SerializationFormatterType.Xml == formatterType) return XmlHelper.DeserializeObject<T>(serializedGraph);
			if (SerializationFormatterType.Json == formatterType) return JsonHelper.DeserializeObject<T>(serializedGraph);

#if HAVE_IFORMATTER
			var formatter = GetFormatter(formatterType, binder);
			if (binder != null) formatter.Binder = binder;

			byte[] arrGraph = Convert.FromBase64String(serializedGraph);
			using (MemoryStream memoryStream = new MemoryStream(arrGraph))
			{
				return (T)formatter.Deserialize(memoryStream);
			}
#endif
			throw new Exception("序列化失败");
		}

#if HAVE_IFORMATTER
		/// <summary>
		/// 将文件内容反序列化成对象实例
		/// </summary>
		/// <typeparam name="T">反序列化类型</typeparam>
		/// <param name="path">反序列化文件路径</param>
		/// <param name="formatterType">序列化格式编码</param>
		/// <param name="binder"></param>
		/// <exception cref="FileNotFoundException"><paramref name="path"/>指定的文件不存在</exception>
		/// <exception cref="NotSupportedException"><paramref name="formatterType"/>为不支持的序列化类型</exception>
		/// <exception cref="Exception">序列化失败</exception>
		/// <returns></returns>
		public static T DeserializeObjectFromFile<T>(string path, SerializationFormatterType formatterType, SerializationBinder binder = null)
#else
		/// <summary>
		/// 将文件内容反序列化成对象实例
		/// </summary>
		/// <typeparam name="T">反序列化类型</typeparam>
		/// <param name="path">反序列化文件路径</param>
		/// <param name="formatterType">序列化格式编码</param>
		/// <exception cref="FileNotFoundException"><paramref name="path"/>指定的文件不存在</exception>
		/// <exception cref="NotSupportedException"><paramref name="formatterType"/>为不支持的序列化类型</exception>
		/// <exception cref="Exception">序列化失败</exception>
		/// <returns></returns>
		public static T DeserializeObjectFromFile<T>(string path, SerializationFormatterType formatterType)
#endif
		{
			if (!File.Exists(path)) throw new FileNotFoundException($"{nameof(path)}指定的文件不存在", path);

			if (SerializationFormatterType.Xml == formatterType) return XmlHelper.DeserializeObjectFromFile<T>(path);
			if (SerializationFormatterType.Json == formatterType) return JsonHelper.DeserializeObjectFromFile<T>(path);

#if HAVE_IFORMATTER
			var formatter = GetFormatter(formatterType, binder);
			if (binder != null) formatter.Binder = binder;

			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				return (T)formatter.Deserialize(fileStream);
			}
#endif
			throw new Exception("序列化失败");
		}

		/// <summary>
		/// 通过序列化复制对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="graph"></param>
		/// <exception cref="ArgumentNullException"><paramref name="graph"/>为null</exception>
		/// <returns></returns>
		public static T CloneObject<T>(T graph)
		{
			//if (graph == null || string.IsNullOrEmpty(graph.ToString())) throw new ArgumentNullException(nameof(graph));
			if (graph == null) throw new ArgumentNullException(nameof(graph));

#if HAVE_IFORMATTER
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(memoryStream, graph);

				memoryStream.Position = 0;
				return (T)formatter.Deserialize(memoryStream);
			}
#else
			var json = JsonHelper.SerializeObject(graph);
			return JsonHelper.DeserializeObject<T>(json);
#endif
		}

#if HAVE_IFORMATTER
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formatterType"></param>
		/// <param name="binder"></param>
		/// <exception cref="NotSupportedException"><paramref name="formatterType"/>为不支持的序列化类型</exception>
		/// <returns></returns>
		private static IFormatter GetFormatter(SerializationFormatterType formatterType, SerializationBinder binder)
		{
			switch (formatterType)
			{
				case SerializationFormatterType.Binary:
					return new BinaryFormatter() { Binder = binder };
#if IS_FRAMEWORK
				case SerializationFormatterType.Soap:
					return new SoapFormatter();
#endif
				default:
					throw new NotSupportedException();
			}
		}
#endif
	}

	/// <summary>
	/// 序列化格式编码
	/// </summary>
	public enum SerializationFormatterType
	{
		/// <summary>
		/// Xml消息格式编码
		/// </summary>
		Xml = 0,
		/// <summary>
		/// Json消息格式编码
		/// </summary>
		Json = 1,
#if HAVE_IFORMATTER
		/// <summary>
		/// 二进制消息格式编码
		/// </summary>
		Binary = 2,
#if IS_FRAMEWORK
		/// <summary>
		/// SOAP消息格式编码
		/// </summary>
		Soap = 3,
#endif
#endif
	}
}
