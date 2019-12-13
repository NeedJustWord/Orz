using System;
using Orz.Unity.Interfaces;

namespace Orz.Unity.Implement
{
    public class Microphone : IMicrophone
    {
        public Microphone()
        {
            Console.WriteLine("Microphone 被构造");
        }
    }
}
