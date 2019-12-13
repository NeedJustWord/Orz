using System;
using Orz.Autofac.Interfaces;

namespace Orz.Autofac.Implement
{
    public class ApplePhone : IPhone
    {
        public string Id { get; set; }
        private IHeadphone headphone;
        //属性注入
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
        public ApplePhone(IMicrophone microphone)
        {
            Microphone = microphone;
            WriteLineWithColor($"构造函数注入{microphone.GetType().Name}类型");

            Id = Guid.NewGuid().ToString();
            Console.WriteLine($"ApplePhone 被构造，Id:{Id}");
        }

        public void Print()
        {
            Console.WriteLine($"Headphone == null?  {Headphone == null}");
            Console.WriteLine($"Microphone == null? {Microphone == null}");
            Console.WriteLine($"Power == null?      {Power == null}");
            Console.WriteLine();
        }

        public void PrintId()
        {
            Console.WriteLine($"ApplePhone Id:{Id}");
            Console.WriteLine($"Headphone  Id:{Headphone?.Id ?? null}");
            Console.WriteLine($"Microphone Id:{Microphone?.Id ?? null}");
            Console.WriteLine($"Power      Id:{Power?.Id ?? null}");
            Console.WriteLine();
        }

        //方法注入
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
