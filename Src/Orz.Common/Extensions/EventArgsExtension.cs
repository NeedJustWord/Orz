using System;
using System.Threading;

namespace Orz.Common.Extensions
{
	/// <summary>
	/// 事件数据TEventArgs的扩展方法，事件数据TEventArgs要求继承自<see cref="EventArgs"/>。
	/// 对事件数据TEventArgs无要求的扩展方法在Orz.Common.Extensions.NoEventArgs命名空间下。
	/// </summary>
	public static class EventArgsExtension
	{
		/// <summary>
		/// 以线程安全的方式引发事件
		/// </summary>
		/// <typeparam name="TEventArgs">继承自<see cref="System.EventArgs"/></typeparam>
		/// <param name="e"></param>
		/// <param name="sender"></param>
		/// <param name="eventHandler"></param>
		public static void InvokeThreadSafe<TEventArgs>(this TEventArgs e, object sender, ref EventHandler<TEventArgs> eventHandler) where TEventArgs : EventArgs
		{
			Volatile.Read(ref eventHandler)?.Invoke(sender, e);
		}

		/// <summary>
		/// 以线程安全的方式引发事件
		/// </summary>
		/// <typeparam name="TEventArgs">继承自<see cref="System.EventArgs"/></typeparam>
		/// <param name="e"></param>
		/// <param name="eventHandler"></param>
		public static void InvokeThreadSafe<TEventArgs>(this TEventArgs e, ref EventHandlerWithOutSender<TEventArgs> eventHandler) where TEventArgs : EventArgs
		{
			Volatile.Read(ref eventHandler)?.Invoke(e);
		}
	}
}

namespace Orz.Common.Extensions.NoEventArgs
{
	/// <summary>
	/// 事件数据TEventArgs的扩展方法
	/// </summary>
	public static class EventArgsExtension
	{
		/// <summary>
		/// 以线程安全的方式引发事件
		/// </summary>
		/// <typeparam name="TEventArgs"></typeparam>
		/// <param name="e"></param>
		/// <param name="sender"></param>
		/// <param name="eventHandler"></param>
		public static void InvokeThreadSafe<TEventArgs>(this TEventArgs e, object sender, ref EventHandler<TEventArgs> eventHandler)
		{
			Volatile.Read(ref eventHandler)?.Invoke(sender, e);
		}

		/// <summary>
		/// 以线程安全的方式引发事件
		/// </summary>
		/// <typeparam name="TEventArgs"></typeparam>
		/// <param name="e"></param>
		/// <param name="eventHandler"></param>
		public static void InvokeThreadSafe<TEventArgs>(this TEventArgs e, ref EventHandlerWithOutSender<TEventArgs> eventHandler)
		{
			Volatile.Read(ref eventHandler)?.Invoke(e);
		}
	}
}