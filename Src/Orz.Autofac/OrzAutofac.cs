using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;

namespace Orz.Autofac
{
    /// <summary>
    /// 封装<see cref="IContainer"/>的IoC容器类。
    /// 可使用<see cref="LazySingleton{T}"/>获取单例实例，其中T是<see cref="OrzAutofac"/>。
    /// </summary>
    public class OrzAutofac
    {
        /// <summary>
        /// IoC容器创建者
        /// </summary>
        public readonly ContainerBuilder ContainerBuilder;
        /// <summary>
        /// IoC容器
        /// </summary>
        public IContainer Container { get; private set; }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrzAutofac()
        {
            ContainerBuilder = new ContainerBuilder();
        }
        #endregion

        #region 注册类型

        #region Register
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac Register<T>(Func<IComponentContext, T> func, string name = null)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (name == null) ContainerBuilder.Register(func).AsSelf();
            else ContainerBuilder.Register(func).Named<T>(name);
            return this;
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="TFrom">父类或父接口</typeparam>
        /// <typeparam name="TTo">子类</typeparam>
        /// <param name="func"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac Register<TFrom, TTo>(Func<IComponentContext, TTo> func, string name = null) where TTo : TFrom
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (name == null) ContainerBuilder.Register(func).As<TFrom>();
            else ContainerBuilder.Register(func).Named<TFrom>(name);
            return this;
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac Register<T>(Func<IComponentContext, IEnumerable<Parameter>, T> func, string name = null)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (name == null) ContainerBuilder.Register(func).AsSelf();
            else ContainerBuilder.Register(func).Named<T>(name);
            return this;
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="TFrom">父类或父接口</typeparam>
        /// <typeparam name="TTo">子类</typeparam>
        /// <param name="func"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac Register<TFrom, TTo>(Func<IComponentContext, IEnumerable<Parameter>, TTo> func, string name = null) where TTo : TFrom
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (name == null) ContainerBuilder.Register(func).As<TFrom>();
            else ContainerBuilder.Register(func).Named<TFrom>(name);
            return this;
        }
        #endregion

        #region RegisterType
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac RegisterType(Type type, string name = null)
        {
            if (name == null) ContainerBuilder.RegisterTypes(type).AsSelf();
            else ContainerBuilder.RegisterTypes(type).Named(name, type);
            return this;
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="from">父类或父接口</param>
        /// <param name="to">子类</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac RegisterType(Type from, Type to, string name = null)
        {
            if (name == null) ContainerBuilder.RegisterTypes(to).As(from);
            else ContainerBuilder.RegisterTypes(to).Named(name, from);
            return this;
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac RegisterType<T>(string name = null)
        {
            if (name == null) ContainerBuilder.RegisterType<T>().AsSelf();
            else ContainerBuilder.RegisterType<T>().Named<T>(name);
            return this;
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="TFrom">父类或父接口</typeparam>
        /// <typeparam name="TTo">子类</typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac RegisterType<TFrom, TTo>(string name = null) where TTo : TFrom
        {
            if (name == null) ContainerBuilder.RegisterType<TTo>().As<TFrom>();
            else ContainerBuilder.RegisterType<TTo>().Named<TFrom>(name);
            return this;
        }
        #endregion

        #region RegisterInstance
        /// <summary>
        /// 注册实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac RegisterInstance<T>(T instance, string name = null) where T : class
        {
            if (name == null) ContainerBuilder.RegisterInstance(instance).AsSelf();
            else ContainerBuilder.RegisterInstance(instance).Named<T>(name);
            return this;
        }

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <typeparam name="TFrom">父类或父接口</typeparam>
        /// <typeparam name="TTo">子类</typeparam>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac RegisterInstance<TFrom, TTo>(TTo instance, string name = null) where TTo : class, TFrom
        {
            if (name == null) ContainerBuilder.RegisterInstance(instance).As<TFrom>();
            else ContainerBuilder.RegisterInstance(instance).Named<TFrom>(name);
            return this;
        }
        #endregion

        #region RegisterSingleton
        /// <summary>
        /// 注册单例类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac RegisterSingleton(Type type, string name = null)
        {
            if (name == null) ContainerBuilder.RegisterTypes(type).SingleInstance().AsSelf();
            else ContainerBuilder.RegisterTypes(type).SingleInstance().Named(name, type);
            return this;
        }

        /// <summary>
        /// 注册单例类型
        /// </summary>
        /// <param name="from">父类或父接口</param>
        /// <param name="to">子类</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac RegisterSingleton(Type from, Type to, string name = null)
        {
            if (name == null) ContainerBuilder.RegisterTypes(to).SingleInstance().As(from);
            else ContainerBuilder.RegisterTypes(to).SingleInstance().Named(name, from);
            return this;
        }

        /// <summary>
        /// 注册单例类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac RegisterSingleton<T>(string name = null)
        {
            if (name == null) ContainerBuilder.RegisterType<T>().SingleInstance().AsSelf();
            else ContainerBuilder.RegisterType<T>().SingleInstance().Named<T>(name);
            return this;
        }

        /// <summary>
        /// 注册单例类型
        /// </summary>
        /// <typeparam name="TFrom">父类或父接口</typeparam>
        /// <typeparam name="TTo">子类</typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrzAutofac RegisterSingleton<TFrom, TTo>(string name = null) where TTo : TFrom
        {
            if (name == null) ContainerBuilder.RegisterType<TTo>().SingleInstance().As<TFrom>();
            else ContainerBuilder.RegisterType<TTo>().SingleInstance().Named<TFrom>(name);
            return this;
        }
        #endregion

        #endregion

        #region 解析对象

        #region Resolve
        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Resolve<T>(string name = null)
        {
            return name == null ? Container.Resolve<T>() : Container.ResolveNamed<T>(name);
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T Resolve<T>(string name, IEnumerable<Parameter> parameters)
        {
            if (parameters == null) parameters = Enumerable.Empty<Parameter>();
            return name == null ? Container.Resolve<T>(parameters) : Container.ResolveNamed<T>(name, parameters);
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T Resolve<T>(string name, params Parameter[] parameters)
        {
            return Resolve<T>(name, (IEnumerable<Parameter>)parameters);
        }
        #endregion

        #region TryResolve
        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TryResolve<T>(out T instance, string name = null)
        {
            return name == null ? Container.TryResolve(out instance) : Container.TryResolveNamed(name, out instance);
        }
        #endregion

        #endregion

        #region 判断是否注册
        /// <summary>
        /// 判断指定类型是否注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsRegistered(Type type)
        {
            return Container.IsRegistered(type);
        }

        /// <summary>
        /// 判断指定类型是否注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsRegistered<T>()
        {
            return Container.IsRegistered<T>();
        }
        #endregion

        #region Build
        /// <summary>
        /// 创建IoC容器
        /// </summary>
        /// <returns></returns>
        public OrzAutofac BuildContainer(ContainerBuildOptions options = ContainerBuildOptions.None)
        {
            Container = ContainerBuilder.Build(options);
            return this;
        }
        #endregion
    }
}
