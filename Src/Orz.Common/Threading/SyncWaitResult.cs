﻿using System;
using System.Collections.Generic;

namespace Orz.Common.Threading
{
	/// <summary>
	/// 多线程同步等待结果
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SyncWaitResult<T> : IDisposable
	{
		private Dictionary<string, WaitResultHandle<T>> dict;
		private readonly object lockObj;

		#region 单例模式
		/// <summary>
		/// 静态延迟加载特性
		/// </summary>
		private static readonly Lazy<SyncWaitResult<T>> lazy = new Lazy<SyncWaitResult<T>>(() => new SyncWaitResult<T>());

		/// <summary>
		/// 静态实例属性
		/// </summary>
		public static SyncWaitResult<T> Instance => lazy.Value;

		/// <summary>
		/// 私有构造函数
		/// </summary>
		private SyncWaitResult()
		{
			dict = new Dictionary<string, WaitResultHandle<T>>();
			lockObj = new object();
		}
		#endregion

		/// <summary>
		/// 等待结果，成功返回true，失败或超时返回false
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="overTime">超时时间</param>
		/// <param name="result">结果</param>
		/// <returns></returns>
		public bool WaitResult(string key, TimeSpan overTime, out T result)
		{
			key = key ?? "";

			WaitResultHandle<T> handle = null;
			lock (lockObj)
			{
				if (dict.ContainsKey(key))
				{
					handle = dict[key];
				}
				else
				{
					handle = new WaitResultHandle<T>();
					dict.Add(key, handle);
				}
			}

			var flag = handle.WaitResult(overTime);
			result = flag ? handle.GetResult() : default(T);
			return flag;
		}

		/// <summary>
		/// 提交结果
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="result">结果</param>
		public void CommmitResult(string key, T result)
		{
			key = key ?? "";

			lock (lockObj)
			{
				if (dict.Remove(key, out var handle)) handle.CommitResult(result);
			}
		}

		/// <summary>
		/// 取消指定key的等待
		/// </summary>
		/// <param name="key"></param>
		public void Cancel(string key)
		{
			key = key ?? "";

			lock (lockObj)
			{
				if (dict.Remove(key, out var handle)) handle.Cancel();
			}
		}

		/// <summary>
		/// 取消所有等待
		/// </summary>
		public void Cancel()
		{
			lock (lockObj)
			{
				foreach (var handle in dict.Values)
				{
					handle.Cancel();
				}
				dict.Clear();
			}
		}

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			lock (lockObj)
			{
				foreach (var handle in dict.Values)
				{
					handle.Dispose();
				}
				dict.Clear();
			}
		}
	}
}
