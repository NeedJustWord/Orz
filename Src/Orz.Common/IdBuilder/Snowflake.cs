using System;

namespace Orz.Common.IdBuilder
{
    /// <summary>
    /// Twitter的Snowflake分布式ID生成算法
    /// <para>默认结构：0-0000000000 0000000000 0000000000 0000000000 0-0000000000-0000000000 00</para>
    /// <para>1位符号位，默认0正数</para>
    /// <para>41位时间戳(毫秒级)，存储时间戳的差值(当前时间戳-开始时间戳)</para>
    /// <para>10位机器id，其中5位数据中心id，5位工作机器id</para>
    /// <para>12位序列号，毫秒内的计数，每毫秒可生成4096个序列号</para>
    /// </summary>
    /// <remarks>
    /// https://github.com/twitter-archive/snowflake/blob/scala_28/src/main/scala/com/twitter/service/snowflake/IdWorker.scala
    /// </remarks>
    public sealed class Snowflake
    {
        #region readonly常量
        /// <summary>
        /// 开始时间戳(毫秒)
        /// </summary>
        public readonly long StartTimeStamp;
        /// <summary>
        /// 数据中心id位数
        /// </summary>
        public readonly byte DataCenterIdBits;
        /// <summary>
        /// 工作机器id位数
        /// </summary>
        public readonly byte WorkerIdBits;
        /// <summary>
        /// 序列号位数
        /// </summary>
        public readonly byte SequenceBits;
        /// <summary>
        /// 数据中心id
        /// </summary>
        public readonly byte DataCenterId;
        /// <summary>
        /// 工作机器id
        /// </summary>
        public readonly byte WorkerId;
        /// <summary>
        /// 序列号最大值，也可用作掩码
        /// </summary>
        public readonly int MaxSequence;

        /// <summary>
        /// 时间戳向左移动的位数
        /// </summary>
        private readonly byte TimeStampLeftShift;
        /// <summary>
        /// 数据中心id向左移动的位数
        /// </summary>
        private readonly byte DataCenterIdLeftShift;
        /// <summary>
        /// 工作机器id向左移动的位数
        /// </summary>
        private readonly byte WorkerIdLeftShift;
        /// <summary>
        /// 数据中心id掩码
        /// </summary>
        private readonly long DataCenterIdMask;
        /// <summary>
        /// 工作机器id掩码
        /// </summary>
        private readonly long WorkerIdMask;
        /// <summary>
        /// 锁对象
        /// </summary>
        private readonly object lockObj = new object();
        #endregion

        #region 属性
        /// <summary>
        /// 当前id序列号
        /// </summary>
        public int Sequence { get; private set; } = 0;
        /// <summary>
        /// 上一次获取id的时间戳
        /// </summary>
        public long LastTimeStamp { get; private set; } = -1L;
        /// <summary>
        /// 当前id
        /// </summary>
        public long CurrentId { get; private set; }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="startTimeStamp">开始时间戳</param>
        /// <param name="dataCenterId">数据中心id</param>
        /// <param name="workerId">工作机器id</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Snowflake(long startTimeStamp, byte dataCenterId, byte workerId) : this(startTimeStamp, 5, 5, 12, dataCenterId, workerId)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="startTimeStamp">开始时间戳</param>
        /// <param name="dataCenterIdBits">数据中心id位数</param>
        /// <param name="workerIdBits">工作机器id位数</param>
        /// <param name="sequenceBits">序列号位数</param>
        /// <param name="dataCenterId">数据中心id</param>
        /// <param name="workerId">工作机器id</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Snowflake(long startTimeStamp, byte dataCenterIdBits, byte workerIdBits, byte sequenceBits, byte dataCenterId, byte workerId)
        {
            if (dataCenterIdBits < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(dataCenterIdBits), $"{nameof(dataCenterIdBits)}不能小于1");
            }
            if (workerIdBits < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(workerIdBits), $"{nameof(workerIdBits)}不能小于1");
            }
            if (sequenceBits < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(sequenceBits), $"{nameof(sequenceBits)}不能小于1");
            }

            WorkerIdLeftShift = sequenceBits;
            DataCenterIdLeftShift = (byte)(WorkerIdLeftShift + workerIdBits);
            TimeStampLeftShift = (byte)(DataCenterIdLeftShift + dataCenterIdBits);
            if (TimeStampLeftShift < 19 || TimeStampLeftShift > 22)
            {
                throw new ArgumentOutOfRangeException($"{nameof(dataCenterIdBits)}、{nameof(workerIdBits)}、{nameof(sequenceBits)}的总位数不能小于19或者大于22", (Exception)null);
            }

            var MaxDataCenterId = GetMaxValue(dataCenterIdBits);
            var MaxWorkerId = GetMaxValue(workerIdBits);
            if (dataCenterId > MaxDataCenterId)
            {
                throw new ArgumentOutOfRangeException(nameof(dataCenterId), $"{nameof(dataCenterId)}不能大于{MaxDataCenterId}");
            }
            if (workerId > MaxWorkerId)
            {
                throw new ArgumentOutOfRangeException(nameof(workerId), $"{nameof(workerId)}不能大于{MaxWorkerId}");
            }

            DataCenterIdMask = ~(long.MaxValue >> TimeStampLeftShift << TimeStampLeftShift);
            WorkerIdMask = ~(long.MaxValue >> DataCenterIdLeftShift << DataCenterIdLeftShift);
            MaxSequence = GetMaxValue(sequenceBits);
            StartTimeStamp = startTimeStamp;
            DataCenterIdBits = dataCenterIdBits;
            WorkerIdBits = workerIdBits;
            SequenceBits = sequenceBits;
            DataCenterId = dataCenterId;
            WorkerId = workerId;
        }

        /// <summary>
        /// 获取唯一id
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public long GetNextId()
        {
            lock (lockObj)
            {
                return IntervalGetNextId();
            }
        }

        /// <summary>
        /// 批量获取唯一id
        /// </summary>
        /// <param name="count">获取个数</param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public long[] GetNextIds(int count)
        {
            lock (lockObj)
            {
                var result = new long[count];
                var i = 0;
                long id;
                int canGetCount, getCount;

                do
                {
                    id = IntervalGetNextId();
                    canGetCount = MaxSequence - Sequence + 1;
                    getCount = Math.Min(canGetCount, count - i);
                    for (int j = 0; j < getCount; j++)
                    {
                        result[i++] = id++;
                    }
                    // 更新序号，IntervalGetNextId里序号加过1，此处需要减1
                    Sequence = (Sequence + getCount - 1) & MaxSequence;
                } while (i < count);

                return result;
            }
        }

        /// <summary>
        /// 获取唯一id字符串
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public string GetNextIdStr()
        {
            return GetNextId().ToString();
        }

        /// <summary>
        /// 批量获取唯一id字符串
        /// </summary>
        /// <param name="count">获取个数</param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public string[] GetNextIdStrs(int count)
        {
            var ids = GetNextIds(count);
            var result = new string[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = ids[i].ToString();
            }
            return result;
        }

        /// <summary>
        /// 获取创建id时的时间戳(毫秒)
        /// </summary>
        /// <param name="id"><paramref name="id"/></param>
        /// <returns></returns>
        public long GetCreateMillisecond(long id)
        {
            return (id >> TimeStampLeftShift) + StartTimeStamp;
        }

        /// <summary>
        /// 获取创建id时的数据中心id
        /// </summary>
        /// <param name="id"><paramref name="id"/></param>
        /// <returns></returns>
        public long GetCreateDataCenterId(long id)
        {
            return (id & DataCenterIdMask) >> DataCenterIdLeftShift;
        }

        /// <summary>
        /// 获取创建id时的工作机器id
        /// </summary>
        /// <param name="id"><paramref name="id"/></param>
        /// <returns></returns>
        public long GetCreateWorkerId(long id)
        {
            return (id & WorkerIdMask) >> WorkerIdLeftShift;
        }

        /// <summary>
        /// 获取创建id时的序列号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public long GetCreateSequence(long id)
        {
            return id & MaxSequence;
        }

        /// <summary>
        /// 获取指定位数的最大值
        /// </summary>
        /// <param name="bits">位数</param>
        /// <returns></returns>
        private int GetMaxValue(byte bits)
        {
            return -1 ^ (-1 << bits);
        }

        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <returns></returns>
        private long GetTimeStamp()
        {
            return (DateTime.UtcNow.Ticks - 621355968000000000) / 10000;
        }

        /// <summary>
        /// 循环获取直到获取到大于<see cref="LastTimeStamp"/>的时间戳
        /// </summary>
        /// <returns></returns>
        private long TillNextMillisecond()
        {
            long timeStamp;
            do
            {
                timeStamp = GetTimeStamp();
            } while (timeStamp <= LastTimeStamp);
            return timeStamp;
        }

        /// <summary>
        /// 获取唯一id
        /// </summary>
        /// <returns></returns>
        private long IntervalGetNextId()
        {
            var timeStamp = GetTimeStamp();

            if (timeStamp == LastTimeStamp)
            {
                // 同一毫秒序列号递增
                Sequence = (Sequence + 1) & MaxSequence;
                // 同一毫秒序列号已经达到最大值，等待下一毫秒
                if (Sequence == 0) timeStamp = TillNextMillisecond();
            }
            else
            {
                // 不同毫秒序列号置0
                Sequence = 0;
            }

            if (timeStamp < LastTimeStamp) throw new Exception($"时间戳比上一次生成id时的时间戳还小，{LastTimeStamp - timeStamp}毫秒内拒绝生成id");

            LastTimeStamp = timeStamp;

#pragma warning disable CS0675 // 对进行了带符号扩展的操作数使用了按位或运算符
            CurrentId = ((timeStamp - StartTimeStamp) << TimeStampLeftShift) // 时间戳部分
                | (DataCenterId << DataCenterIdLeftShift)                    // 数据中心id部分
                | (WorkerId << WorkerIdLeftShift)                            // 工作机器id部分
                | Sequence;                                                  // 序列号部分
#pragma warning restore CS0675 // 对进行了带符号扩展的操作数使用了按位或运算符
            return CurrentId;
        }
    }
}
