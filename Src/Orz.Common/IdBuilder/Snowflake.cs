using System;

namespace Orz.Common.IdBuilder
{
	/// <summary>
	/// Twitter的Snowflake分布式ID生成算法
	/// </summary>
	public sealed class Snowflake
	{
		#region const常量
		/// <summary>
		/// 起始的时间戳(毫秒) 1546272000000L(2019-01-01)
		/// </summary>
		private const long StartTimeStamp = 1546272000000L;
		/// <summary>
		/// 数据中心占用的位数
		/// </summary>
		private const int DataCenterIdBits = 5;
		/// <summary>
		/// 机器标识占用的位数
		/// </summary>
		private const int MachineIdBits = 5;
		/// <summary>
		/// 序列号占用的位数
		/// </summary>
		private const int SequenceBits = 12;
		/// <summary>
		/// 数据中心最大值
		/// </summary>
		private const long MaxDataCenterId = -1L ^ (-1L << DataCenterIdBits);
		/// <summary>
		/// 机器标识最大值
		/// </summary>
		private const long MaxMachineId = -1L ^ (-1L << MachineIdBits);
		/// <summary>
		/// 序列号最大值
		/// </summary>
		private const long MaxSequence = -1L ^ (-1L << SequenceBits);
		/// <summary>
		/// 机器标识向左移动的位数
		/// </summary>
		private const int MachineIdLeftShift = SequenceBits;
		/// <summary>
		/// 数据中心向左移动的位数
		/// </summary>
		private const int DataCenterIdLeftShift = SequenceBits + MachineIdBits;
		/// <summary>
		/// 时间戳向左移动的位数
		/// </summary>
		private const int TimeStampLeftShift = SequenceBits + MachineIdBits + DataCenterIdBits;
		#endregion

		#region 变量
		/// <summary>
		/// 锁对象
		/// </summary>
		private readonly object lockObj = new object();
		/// <summary>
		/// 数据中心
		/// </summary>
		public readonly long DataCenterId;
		/// <summary>
		/// 机器标识
		/// </summary>
		public readonly long MachineId;
		/// <summary>
		/// 序列号
		/// </summary>
		private long sequence = 0L;
		/// <summary>
		/// 上一次的时间戳
		/// </summary>
		private long lastTimeStamp = -1L;
		#endregion

		#region 属性
		/// <summary>
		/// 当前id
		/// </summary>
		public long CurrentId { get; private set; }
		#endregion

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="dataCenterId">数据中心id</param>
		/// <param name="machineId">机器标识id</param>
		public Snowflake(long dataCenterId, long machineId)
		{
			if (dataCenterId > MaxDataCenterId || dataCenterId < 0) throw new ArgumentOutOfRangeException(nameof(dataCenterId), $"{nameof(dataCenterId)}不能大于{MaxDataCenterId}或者小于0");
			if (machineId > MaxMachineId || machineId < 0) throw new ArgumentOutOfRangeException(nameof(machineId), $"{nameof(machineId)}不能大于{MaxMachineId}或者小于0");

			DataCenterId = dataCenterId;
			MachineId = machineId;
		}

		/// <summary>
		/// 获取唯一id
		/// </summary>
		/// <returns></returns>
		public long GetNextId()
		{
			lock (lockObj)
			{
				var timeStamp = GetTimeStamp();

				if (timeStamp == lastTimeStamp)
				{
					// 同一毫秒序列号递增
					sequence = (sequence + 1) & MaxSequence;
					// 同一毫秒序列号已经达到最大值，等待下一毫秒
					if (sequence == 0) timeStamp = TillNextMillisecond();
				}
				else
				{
					// 不同毫秒序列号置0
					sequence = 0L;
				}

				if (timeStamp < lastTimeStamp) throw new Exception($"时间戳比上一次生成id时的时间戳还小，{lastTimeStamp - timeStamp}毫秒内拒绝生成id");

				lastTimeStamp = timeStamp;

				CurrentId = ((timeStamp - StartTimeStamp) << TimeStampLeftShift) // 时间戳部分
					| (DataCenterId << DataCenterIdLeftShift)               // 数据中心部分
					| (MachineId << MachineIdLeftShift)                     // 机器标识部分
					| sequence;                                             // 序列号部分
				return CurrentId;
			}
		}

		/// <summary>
		/// 获取唯一id字符串
		/// </summary>
		/// <returns></returns>
		public string GetNextIdStr()
		{
			return GetNextId() + "";
		}

		/// <summary>
		/// 获取创建id时的Unix毫秒戳
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
			return (id & (~(long.MaxValue >> TimeStampLeftShift << TimeStampLeftShift))) >> DataCenterIdLeftShift;
		}

		/// <summary>
		/// 获取创建id时的机器标识id
		/// </summary>
		/// <param name="id"><paramref name="id"/></param>
		/// <returns></returns>
		public long GetCreateMachineId(long id)
		{
			return (id & (~(long.MaxValue >> DataCenterIdLeftShift << DataCenterIdLeftShift))) >> MachineIdLeftShift;
		}

		/// <summary>
		/// 获取创建id时的序列号
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public long GetCreateSequence(long id)
		{
			return id & (~(long.MaxValue >> MachineIdLeftShift << MachineIdLeftShift));
		}

		/// <summary>
		/// 获取当前时间的Unix毫秒戳
		/// </summary>
		/// <returns></returns>
		private long GetTimeStamp()
		{
			return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		}

		/// <summary>
		/// 循环获取直到获取到大于lastTimeStamp的Unix毫秒戳
		/// </summary>
		/// <returns></returns>
		private long TillNextMillisecond()
		{
			var timeStamp = GetTimeStamp();
			while (timeStamp <= lastTimeStamp)
			{
				timeStamp = GetTimeStamp();
			}
			return timeStamp;
		}
	}
}
