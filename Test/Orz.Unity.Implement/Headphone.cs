using System;
using Orz.Unity.Interfaces;

namespace Orz.Unity.Implement
{
	public class Headphone : IHeadphone
	{
		public Headphone()
		{
			Console.WriteLine("Headphone 被构造");
		}
	}
}
