namespace System
{
	/// <summary>
	/// 不带sender的泛型事件委托
	/// </summary>
	/// <typeparam name="TEventArgs"></typeparam>
	/// <param name="e">事件数据</param>
	public delegate void EventHandlerWithOutSender<TEventArgs>(TEventArgs e);
}
