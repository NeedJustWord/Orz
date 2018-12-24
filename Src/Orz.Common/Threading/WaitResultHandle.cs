﻿using System;
using System.Threading;

namespace Orz.Common.Threading
{
	/// <summary>
	/// 封装线程等待结果的对象
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class WaitResultHandle<T> : IDisposable
	{
		private T result;
		private bool isCommit;
		private readonly object lockObj;
		private ManualResetEvent manualResetEvent;

		/// <summary>
		/// 构造函数
		/// </summary>
		public WaitResultHandle()
		{
			isCommit = false;
			lockObj = new object();
			manualResetEvent = new ManualResetEvent(false);
		}

		/// <summary>
		/// 等待结果，成功返回true，失败或超时返回false
		/// </summary>
		/// <param name="overTime">超时时间</param>
		/// <returns></returns>
		public bool WaitResult(TimeSpan overTime)
		{
			return manualResetEvent.WaitOne(overTime) && isCommit;
		}

		/// <summary>
		/// 提交结果
		/// </summary>
		/// <param name="result"></param>
		public void CommitResult(T result)
		{
			lock (lockObj)
			{
				isCommit = true;
				this.result = result;
			}
			manualResetEvent.Set();
		}

		/// <summary>
		/// 取消等待
		/// </summary>
		public void Cancel()
		{
			manualResetEvent.Set();
		}

		/// <summary>
		/// 获取结果
		/// </summary>
		/// <returns></returns>
		public T GetResult()
		{
			lock (lockObj)
			{
				return result;
			}
		}

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			manualResetEvent.Set();
			manualResetEvent.Close();
			manualResetEvent.Dispose();
		}
	}
}
