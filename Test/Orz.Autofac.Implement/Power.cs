using System;
using Orz.Autofac.Interfaces;

namespace Orz.Autofac.Implement
{
    public class Power : IPower
    {
        public string Id { get; set; }

        public Power()
        {
            Id = Guid.NewGuid().ToString();
            Console.WriteLine($"Power 被构造，Id:{Id}");
        }

    }
}
