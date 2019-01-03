using System;
using Orz.Unity.Implement;
using Orz.Unity.Interfaces;

namespace Orz.Unity.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			LoadConfigFile();

			Console.WriteLine("按任意键退出!");
			Console.ReadKey();
		}

		/// <summary>
		/// 测试此方法时需要添加Orz.Unity.Implement的引用
		/// </summary>
		static void RegisterType()
		{
			Console.WriteLine("RegisterType:");

			var orzUnity = LazySingleton<OrzUnity>.Instance;
			orzUnity.RegisterType<IPhone, ApplePhone>();
			orzUnity.RegisterType<IHeadphone, Headphone>();
			orzUnity.RegisterType<IMicrophone, Microphone>();
			orzUnity.RegisterType<IPower, Power>();

			Print();
		}

		/// <summary>
		/// 测试此方法时需要去掉Orz.Unity.Implement的引用
		/// </summary>
		static void LoadDefaultConfigFile()
		{
			Console.WriteLine("LoadDefaultConfigFile:");

			var orzUnity = LazySingleton<OrzUnity>.Instance;
			orzUnity.LoadDefaultConfigFile();

			Print();
		}

		/// <summary>
		/// 测试此方法时需要去掉Orz.Unity.Implement的引用
		/// </summary>
		static void LoadConfigFile()
		{
			Console.WriteLine("LoadConfigFile:");

			var orzUnity = LazySingleton<OrzUnity>.Instance;
			orzUnity.LoadConfigFile();

			Print();
		}

		static void Print()
		{
			var phone = LazySingleton<OrzUnity>.Instance.Resolve<IPhone>();
			phone.Print();
		}
	}
}
