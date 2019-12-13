using System;
using Orz.Unity.Interfaces;
using Unity.Attributes;

namespace Orz.Unity.Implement
{
    public class ApplePhone : IPhone
    {
        private IHeadphone headphone;
        //属性注入
        [Dependency]
        public IHeadphone Headphone
        {
            get
            {
                return headphone;
            }
            set
            {
                headphone = value;
                WriteLineWithColor($"属性注入{value.GetType().Name}类型");
            }
        }
        public IMicrophone Microphone { get; set; }
        public IPower Power { get; set; }

        //构造函数注入
        [InjectionConstructor]
        public ApplePhone(IMicrophone microphone)
        {
            Microphone = microphone;
            WriteLineWithColor($"构造函数注入{microphone.GetType().Name}类型");
        }

        public void Print()
        {
            Console.WriteLine($"Headphone == null?  {Headphone == null}");
            Console.WriteLine($"Microphone == null? {Microphone == null}");
            Console.WriteLine($"Power == null?      {Power == null}");
            Console.WriteLine();
        }

        //方法注入
        [InjectionMethod]
        public void Init(IPower power)
        {
            Power = power;
            WriteLineWithColor($"方法注入{power.GetType().Name}类型");
        }

        private void WriteLineWithColor(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }
    }
}
