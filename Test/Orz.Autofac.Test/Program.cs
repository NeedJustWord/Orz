using System;
using Autofac;
using Orz.Autofac.Implement;
using Orz.Autofac.Interfaces;

namespace Orz.Autofac.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterType();

            Console.WriteLine("按任意键退出!");
            Console.ReadKey();
        }

        static void RegisterType()
        {
            Console.WriteLine("RegisterType:开始注册");

            var orzAutofac = LazySingleton<OrzAutofac>.Instance;
            orzAutofac.RegisterType<IHeadphone, Headphone>();
            orzAutofac.RegisterType<IMicrophone, Microphone>();
            orzAutofac.RegisterType<IPower, Power>();
            orzAutofac.Register<IPhone, ApplePhone>(n =>
            {
                var apple = new ApplePhone(n.Resolve<IMicrophone>())
                {
                    Headphone = n.Resolve<IHeadphone>()
                };
                apple.Init(n.Resolve<IPower>());
                return apple;
            });

            orzAutofac.BuildContainer();

            Console.WriteLine("第一次获取对象：");
            Print();

            Console.WriteLine("第二次获取对象：");
            Print();
        }

        static void Print()
        {
            Console.WriteLine();
            Console.WriteLine("类型已注册，开始实例化");
            var phone = LazySingleton<OrzAutofac>.Instance.Resolve<IPhone>();

            Console.WriteLine("类型已实例化，开始调用方法");
            phone.Print();
            phone.PrintId();
        }
    }
}
