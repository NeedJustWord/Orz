using System;
using Orz.Autofac.Interfaces;

namespace Orz.Autofac.Implement
{
    public class Microphone : IMicrophone
    {
        public string Id { get; set; }

        public Microphone()
        {
            Id = Guid.NewGuid().ToString();
            Console.WriteLine($"Microphone 被构造，Id:{Id}");
        }

    }
}
