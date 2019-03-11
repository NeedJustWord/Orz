using System;
using System.Collections.Generic;
using Unity;
using Unity.Lifetime;
using Unity.Registration;
using Unity.Resolution;

#if HAVE_UNITY_CONFIGURATION
using System.Configuration;
using Microsoft.Practices.Unity.Configuration;
#endif

namespace Orz.Unity
{
	/// <summary>
	/// 封装<see cref="IUnityContainer"/>的IoC容器类。
	/// 可使用<see cref="LazySingleton{T}"/>获取单例实例，其中T是<see cref="OrzUnity"/>。
	/// </summary>
	public class OrzUnity
	{
		/// <summary>
		/// IoC容器
		/// </summary>
		public readonly IUnityContainer Container;

		#region 构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		public OrzUnity()
		{
			Container = new UnityContainer();
		}
		#endregion

		#region 注册类型

		#region RegisterType
		/// <summary>
		/// 注册类型
		/// </summary>
		/// <param name="type"></param>
		/// <param name="name">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <param name="lifetimeManager"></param>
		/// <param name="injectionMembers"></param>
		/// <returns></returns>
		public OrzUnity RegisterType(Type type, string name = null, LifetimeManager lifetimeManager = null, params InjectionMember[] injectionMembers)
		{
			Container.RegisterType(type, name, lifetimeManager, injectionMembers);
			return this;
		}

		/// <summary>
		/// 注册类型
		/// </summary>
		/// <param name="from">父类或父接口</param>
		/// <param name="to">子类</param>
		/// <param name="name">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <param name="lifetimeManager"></param>
		/// <param name="injectionMembers"></param>
		/// <returns></returns>
		public OrzUnity RegisterType(Type from, Type to, string name = null, LifetimeManager lifetimeManager = null, params InjectionMember[] injectionMembers)
		{
			Container.RegisterType(from, to, name, lifetimeManager, injectionMembers);
			return this;
		}

		/// <summary>
		/// 注册类型
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <param name="lifetimeManager"></param>
		/// <param name="injectionMembers"></param>
		/// <returns></returns>
		public OrzUnity RegisterType<T>(string name = null, LifetimeManager lifetimeManager = null, params InjectionMember[] injectionMembers)
		{
			Container.RegisterType<T>(name, lifetimeManager, injectionMembers);
			return this;
		}

		/// <summary>
		/// 注册类型
		/// </summary>
		/// <typeparam name="TFrom">父类或父接口</typeparam>
		/// <typeparam name="TTo">子类</typeparam>
		/// <param name="name">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <param name="lifetimeManager"></param>
		/// <param name="injectionMembers"></param>
		/// <returns></returns>
		public OrzUnity RegisterType<TFrom, TTo>(string name = null, LifetimeManager lifetimeManager = null, params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			Container.RegisterType<TFrom, TTo>(name, lifetimeManager, injectionMembers);
			return this;
		}
		#endregion

		#region RegisterInstance
		/// <summary>
		/// 注册实例
		/// </summary>
		/// <param name="type"></param>
		/// <param name="instance"></param>
		/// <param name="name">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <param name="lifetime"></param>
		/// <returns></returns>
		public OrzUnity RegisterInstance(Type type, object instance, string name = null, LifetimeManager lifetime = null)
		{
			lifetime = lifetime ?? new ContainerControlledLifetimeManager();
			Container.RegisterInstance(type, name, instance, lifetime);
			return this;
		}

		/// <summary>
		/// 注册实例
		/// </summary>
		/// <typeparam name="TInterface"></typeparam>
		/// <param name="instance"></param>
		/// <param name="name">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <param name="lifetime"></param>
		/// <returns></returns>
		public OrzUnity RegisterInstance<TInterface>(TInterface instance, string name = null, LifetimeManager lifetime = null)
		{
			lifetime = lifetime ?? new ContainerControlledLifetimeManager();
			Container.RegisterInstance(name, instance, lifetime);
			return this;
		}
		#endregion

		#region RegisterSingleton
		/// <summary>
		/// 注册单例类型
		/// </summary>
		/// <param name="type"></param>
		/// <param name="name">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <param name="injectionMembers"></param>
		/// <returns></returns>
		public OrzUnity RegisterSingleton(Type type, string name = null, params InjectionMember[] injectionMembers)
		{
			Container.RegisterSingleton(type, name, injectionMembers);
			return this;
		}

		/// <summary>
		/// 注册单例类型
		/// </summary>
		/// <param name="from">父类或父接口</param>
		/// <param name="to">子类</param>
		/// <param name="name">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <param name="injectionMembers"></param>
		/// <returns></returns>
		public OrzUnity RegisterSingleton(Type from, Type to, string name = null, params InjectionMember[] injectionMembers)
		{
			Container.RegisterSingleton(from, to, name, injectionMembers);
			return this;
		}

		/// <summary>
		/// 注册单例类型
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <param name="injectionMembers"></param>
		/// <returns></returns>
		public OrzUnity RegisterSingleton<T>(string name = null, params InjectionMember[] injectionMembers)
		{
			Container.RegisterSingleton<T>(name, injectionMembers);
			return this;
		}

		/// <summary>
		/// 注册单例类型
		/// </summary>
		/// <typeparam name="TFrom">父类或父接口</typeparam>
		/// <typeparam name="TTo">子类</typeparam>
		/// <param name="name">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <param name="injectionMembers"></param>
		/// <returns></returns>
		public OrzUnity RegisterSingleton<TFrom, TTo>(string name = null, params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			Container.RegisterSingleton<TFrom, TTo>(name, injectionMembers);
			return this;
		}
		#endregion

#if HAVE_UNITY_CONFIGURATION
		#region LoadConfigFile
		/// <summary>
		/// 加载默认配置文件（App.config或Web.config）里的类型注册信息
		/// </summary>
		/// <param name="sectionName"></param>
		/// <param name="containerName"></param>
		/// <returns></returns>
		public OrzUnity LoadDefaultConfigFile(string sectionName = "unity", string containerName = "")
		{
			UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(sectionName);
			Container.LoadConfiguration(section, containerName);
			return this;
		}

		/// <summary>
		/// 加载指定配置文件里的类型注册信息
		/// </summary>
		/// <param name="configFilename">配置文件的相对路径(相对当前工作目录)或绝对路径</param>
		/// <param name="sectionName"></param>
		/// <param name="containerName"></param>
		/// <returns></returns>
		public OrzUnity LoadConfigFile(string configFilename = "unity.config", string sectionName = "unity", string containerName = "")
		{
			Container.LoadConfiguration(configFilename, sectionName, containerName);
			return this;
		}
		#endregion
#endif

		#endregion

		#region 解析对象
		/// <summary>
		/// 解析对象
		/// </summary>
		/// <param name="type"></param>
		/// <param name="name">null和string.Empty表示解析默认对象(内部都是使用null)，否则表示解析命名对象</param>
		/// <param name="resolverOverrides"></param>
		/// <returns></returns>
		public object Resolve(Type type, string name = null, params ResolverOverride[] resolverOverrides)
		{
			return Container.Resolve(type, name, resolverOverrides);
		}

		/// <summary>
		/// 解析对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name">null和string.Empty表示解析默认对象(内部都是使用null)，否则表示解析命名对象</param>
		/// <param name="resolverOverrides"></param>
		/// <returns></returns>
		public T Resolve<T>(string name = null, params ResolverOverride[] resolverOverrides)
		{
			return Container.Resolve<T>(name, resolverOverrides);
		}

		/// <summary>
		/// 解析所有命名对象
		/// </summary>
		/// <param name="type"></param>
		/// <param name="resolverOverrides"></param>
		/// <returns></returns>
		public IEnumerable<object> ResolveAll(Type type, params ResolverOverride[] resolverOverrides)
		{
			return Container.ResolveAll(type, resolverOverrides);
		}

		/// <summary>
		/// 解析所有命名对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="resolverOverrides"></param>
		/// <returns></returns>
		public IEnumerable<T> ResolveAll<T>(params ResolverOverride[] resolverOverrides)
		{
			return Container.ResolveAll<T>(resolverOverrides);
		}
		#endregion

		#region 判断是否注册
		/// <summary>
		/// 判断类型在指定命名下是否注册
		/// </summary>
		/// <param name="typeToCheck"></param>
		/// <param name="nameToCheck">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <returns></returns>
		public bool IsRegistered(Type typeToCheck, string nameToCheck = null)
		{
			return Container.IsRegistered(typeToCheck, nameToCheck);
		}

		/// <summary>
		/// 判断类型在指定命名下是否注册
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="nameToCheck">null和string.Empty表示默认注册(内部都是使用null)，否则表示命名注册</param>
		/// <returns></returns>
		public bool IsRegistered<T>(string nameToCheck = null)
		{
			return Container.IsRegistered<T>(nameToCheck);
		}
		#endregion
	}
}
