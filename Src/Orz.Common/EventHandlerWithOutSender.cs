namespace System
{
    /// <summary>
    /// 不带sender的泛型事件委托
    /// </summary>
    /// <typeparam name="TEventArgs">事件数据类型</typeparam>
    /// <param name="e">事件数据实例</param>
    public delegate void EventHandlerWithOutSender<TEventArgs>(TEventArgs e);
}
