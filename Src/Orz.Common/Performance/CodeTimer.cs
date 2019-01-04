using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

#if IS_FRAMEWORK || IS_NETCOREAPP2
using System.Threading;
#endif

namespace Orz.Common.Performance
{
	/// <summary>
	/// 性能计数器
	/// 用法：先调用Initialize方法初始化和预热，再调用Run方法测试性能
	/// </summary>
	/// <remarks>
	/// http://www.cnblogs.com/JeffreyZhao/archive/2009/03/10/codetimer.html
	/// http://www.cnblogs.com/eaglet/archive/2009/03/10/1407791.html
	/// </remarks>
	public static class CodeTimer
	{
		/// <summary>
		/// 初始化
		/// </summary>
		public static void Initialize()
		{
			Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
#if IS_FRAMEWORK || IS_NETCOREAPP2
			Thread.CurrentThread.Priority = ThreadPriority.Highest;
#endif
			Run("", 1, () => { });
			Run("", 1, (ICodeTimerAction)null);
		}

		/// <summary>
		/// 性能计数
		/// </summary>
		/// <param name="name">名称，不能为null或空串</param>
		/// <param name="iteration">循环次数，不能小于1</param>
		/// <param name="action">测试性能的方法委托，不能为null</param>
		public static void Run(string name, int iteration, Action action)
		{
			if (string.IsNullOrEmpty(name) || iteration < 1 || action == null) return;

			//1.打印name
			ConsoleColor currentForeColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(name);

			//2.强制垃圾回收，获取每代垃圾回收次数
			GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
			int[] gcCounts = new int[GC.MaxGeneration + 1];
			for (int i = 0; i < gcCounts.Length; i++)
			{
				gcCounts[i] = GC.CollectionCount(i);
			}

			//3.执行action
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			ulong cycleTime = GetThreadCycleTime();
			for (int i = 0; i < iteration; i++) action();
			ulong cpuCycles = GetThreadCycleTime() - cycleTime;
			stopwatch.Stop();

			//4.打印计时信息
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine($"\tIteration:\t\t{iteration}");
			Console.WriteLine($"\tTime Elapsed:\t\t{stopwatch.ElapsedMilliseconds.ToString("N0")}ms");
			Console.WriteLine($"\tTime Elapsed(one time):\t{((double)stopwatch.ElapsedMilliseconds / iteration).ToString("N3")}ms");
			Console.WriteLine($"\tCPU Cycles:\t\t{cpuCycles.ToString("N0")}");
			Console.WriteLine($"\tCPU Cycles(one time):\t{((double)cpuCycles / iteration).ToString("N3")}");

			//5.打印GC信息
			for (int i = 0; i < gcCounts.Length; i++)
			{
				int count = GC.CollectionCount(i) - gcCounts[i];
				Console.WriteLine($"\tGen {i}: \t\t\t{count}");
			}

			Console.WriteLine();
			Console.ForegroundColor = currentForeColor;
		}

		/// <summary>
		/// 性能计数，推荐用此方法
		/// </summary>
		/// <param name="name">名称，不能为null或空串</param>
		/// <param name="iteration">循环次数，不能小于1</param>
		/// <param name="action">测试性能的方法接口，不能为null</param>
		public static void Run(string name, int iteration, ICodeTimerAction action)
		{
			if (string.IsNullOrEmpty(name) || iteration < 1 || action == null) return;

			//1.打印name
			ConsoleColor currentForeColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(name);

			//2.强制垃圾回收，获取每代垃圾回收次数
			GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
			int[] gcCounts = new int[GC.MaxGeneration + 1];
			for (int i = 0; i < gcCounts.Length; i++)
			{
				gcCounts[i] = GC.CollectionCount(i);
			}

			//3.执行action
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			ulong cycleTime = GetThreadCycleTime();
			for (int i = 0; i < iteration; i++) action.Action();
			ulong cpuCycles = GetThreadCycleTime() - cycleTime;
			stopwatch.Stop();

			//4.打印计时信息
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine($"\tIteration:\t\t{iteration}");
			Console.WriteLine($"\tTime Elapsed:\t\t{stopwatch.ElapsedMilliseconds.ToString("N0")}ms");
			Console.WriteLine($"\tTime Elapsed(one time):\t{((double)stopwatch.ElapsedMilliseconds / iteration).ToString("N3")}ms");
			Console.WriteLine($"\tCPU Cycles:\t\t{cpuCycles.ToString("N0")}");
			Console.WriteLine($"\tCPU Cycles(one time):\t{((double)cpuCycles / iteration).ToString("N3")}");

			//5.打印GC信息
			for (int i = 0; i < gcCounts.Length; i++)
			{
				int count = GC.CollectionCount(i) - gcCounts[i];
				Console.WriteLine($"\tGen {i}: \t\t\t{count}");
			}

			Console.WriteLine();
			Console.ForegroundColor = currentForeColor;
		}

		#region 获取线程使用的CPU时钟周期数
		/// <summary>
		/// 获取线程使用的CPU时钟周期数
		/// </summary>
		/// <returns></returns>
		private static ulong GetThreadCycleTime()
		{
			ulong cycleTime = 0;
			QueryThreadCycleTime(GetCurrentThread(), ref cycleTime);
			return cycleTime;
		}

		/// <summary>
		/// 查询指定线程使用的CPU时钟周期数
		/// </summary>
		/// <param name="threadHandle"></param>
		/// <param name="cycleTime"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);

		/// <summary>
		/// 获取当前线程的句柄
		/// </summary>
		/// <returns></returns>
		[DllImport("kernel32.dll")]
		static extern IntPtr GetCurrentThread();
		#endregion
	}

	/// <summary>
	/// 性能计数器接口
	/// </summary>
	public interface ICodeTimerAction
	{
		/// <summary>
		/// 方法体
		/// </summary>
		void Action();
	}
}
