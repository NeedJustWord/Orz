using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orz.Common.IdBuilder;

namespace Orz.Common.MSTest.IdBuilder
{
    [TestClass]
    public class SnowflakeTest
    {
        [TestMethod]
        public void Get19700101UtcTicks()
        {
            Debug.WriteLine($"时间戳起始Ticks：{new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks}");
        }

        [TestMethod]
        public void GetInfo()
        {
            var snowflake = GetSnowflake();
            var count = snowflake.MaxSequence + 1;
            for (int i = 0; i < count; i++)
            {
                var id = snowflake.GetNextId();
                var timeStamp = snowflake.GetCreateMillisecond(id);
                var centerId = snowflake.GetCreateDataCenterId(id);
                var workerId = snowflake.GetCreateWorkerId(id);
                var sequence = snowflake.GetCreateSequence(id);

                Assert.IsTrue(timeStamp == snowflake.LastTimeStamp);
                Assert.IsTrue(centerId == snowflake.DataCenterId);
                Assert.IsTrue(workerId == snowflake.WorkerId);
                Assert.IsTrue(sequence == snowflake.Sequence);
            }
        }

        [TestMethod]
        public void GetIds()
        {
            var snowflake = GetSnowflake();
            var count = 10_0000;
            var ids = snowflake.GetNextIds(count);
            long id1 = ids[0], time1 = snowflake.GetCreateMillisecond(id1);
            long id2 = 0, time2;
            int timeCount = 1;
            for (int i = 1; i < ids.Length; i++)
            {
                id2 = ids[i];
                time2 = snowflake.GetCreateMillisecond(id2);

                if (time1 == time2)
                {
                    Assert.IsTrue(id2 == id1 + 1);
                }
                else
                {
                    timeCount++;
                    Assert.IsTrue(snowflake.GetCreateSequence(id2) == 0);
                }

                id1 = id2;
                time1 = time2;
            }

            Assert.IsTrue(snowflake.GetCreateSequence(id2) == snowflake.Sequence);

            Debug.WriteLine($"时间个数：{timeCount}");
            Debug.WriteLine($"最后序号：{snowflake.Sequence}");
        }

        private Snowflake GetSnowflake()
        {
            return new Snowflake(1546272000000L, 10, 20);
        }
    }
}
