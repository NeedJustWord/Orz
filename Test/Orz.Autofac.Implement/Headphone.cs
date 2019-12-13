using System;
using Orz.Autofac.Interfaces;

namespace Orz.Autofac.Implement
{
    public class Headphone : IHeadphone
    {
        public string Id { get; set; }

        public Headphone()
        {
            Id = Guid.NewGuid().ToString();
            Console.WriteLine($"Headphone 被构造，Id:{Id}");
        }
    }
}
