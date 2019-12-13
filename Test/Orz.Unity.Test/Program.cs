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
            Console.WriteLine("RegisterType:开始注册");

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
            Console.WriteLine("LoadDefaultConfigFile:开始注册");

            var orzUnity = LazySingleton<OrzUnity>.Instance;
            orzUnity.LoadDefaultConfigFile();

            Print();
        }

        /// <summary>
        /// 测试此方法时需要去掉Orz.Unity.Implement的引用
        /// </summary>
        static void LoadConfigFile()
        {
            Console.WriteLine("LoadConfigFile:开始注册");

            var orzUnity = LazySingleton<OrzUnity>.Instance;
            orzUnity.LoadConfigFile();

            Print();
        }

        static void Print()
        {
            Console.WriteLine();
            Console.WriteLine("类型已注册，开始实例化");
            var phone = LazySingleton<OrzUnity>.Instance.Resolve<IPhone>();

            Console.WriteLine();
            Console.WriteLine("类型已实例化，开始调用方法");
            phone.Print();
        }
    }
}
