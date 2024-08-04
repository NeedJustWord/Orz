using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orz.Common.MSTest
{
    [TestClass]
    public class LazySingletonTest
    {
        [TestMethod]
        public void LazySingleton()
        {
            var test = LazySingleton<Test>.Instance;

            int count = 100;
            Test[] array = new Test[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = LazySingleton<Test>.Instance;
                Assert.IsTrue(test.Guid == array[i].Guid);
            }
        }

        private class Test
        {
            public readonly string Guid;

            public Test()
            {
                Guid = new Guid().ToString();
            }
        }
    }
}
