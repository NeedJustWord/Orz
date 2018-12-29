using System;
using Orz.Unity.Interfaces;
using Unity.Attributes;

namespace Orz.Unity.Implement
{
	public class ApplePhone : IPhone
	{
		//属性注入
		[Dependency]
		public IHeadphone Headphone { get; set; }
		public IMicrophone Microphone { get; set; }
		public IPower Power { get; set; }

		[InjectionConstructor]
		public ApplePhone(IMicrophone microphone)
		{
			Microphone = microphone;
			Console.WriteLine($"{GetType().Name}带参数构造函数注入{microphone.GetType().Name}类型");
		}

		public void Print()
		{
			Console.WriteLine($"Headphone == null?  {Headphone == null}");
			Console.WriteLine($"Microphone == null? {Microphone == null}");
			Console.WriteLine($"Power == null?      {Power == null}");
		}

		[InjectionMethod]
		public void Init(IPower power)
		{
			Power = power;
			Console.WriteLine($"方法注入{power.GetType().Name}类型");
		}
	}
}
