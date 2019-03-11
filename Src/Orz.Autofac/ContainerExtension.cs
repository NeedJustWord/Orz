using System;
using Autofac;

namespace Orz.Autofac
{
	/// <summary>
	/// <see cref="IContainer"/>扩展
	/// </summary>
	public static class ContainerExtension
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="container"></param>
		/// <param name="name"></param>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static bool TryResolveNamed<T>(this IContainer container, string name, out T instance)
		{
			if (container == null) throw new ArgumentNullException(nameof(container));
			if (name == null) throw new ArgumentNullException(nameof(name));

			var flag = container.TryResolveNamed(name, typeof(T), out var obj);
			instance = flag ? (T)obj : default(T);
			return flag;
		}
	}
}
