﻿using System;

namespace Orz.Common
{
	/// <summary>
	/// 具有延迟加载特性的泛型单例类
	/// </summary>
	/// <typeparam name="T">要求具有公共的无参构造函数</typeparam>
	public sealed class OrzLazySingleton<T> where T : new()
	{
		/// <summary>
		/// 静态延迟加载特性
		/// </summary>
		private static readonly Lazy<OrzLazySingleton<T>> lazy = new Lazy<OrzLazySingleton<T>>(() => new OrzLazySingleton<T>());

		/// <summary>
		/// 静态泛型实例属性
		/// </summary>
		public static T Instance => lazy.Value.instance;

		/// <summary>
		/// 私有泛型实例
		/// </summary>
		private T instance;

		/// <summary>
		/// 私有构造函数
		/// </summary>
		private OrzLazySingleton() => instance = new T();
	}
}