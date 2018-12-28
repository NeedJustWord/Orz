#if HAVE_UNITY_CONFIGURATION
using System;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration;
using Unity;

namespace Orz.Unity
{
	/// <summary>
	/// <see cref="IUnityContainer"/>扩展
	/// </summary>
	public static class UnityContainerExtension
	{
		/// <summary>
		/// 加载指定配置文件里的类型注册信息到IoC容器
		/// </summary>
		/// <param name="container">IoC容器</param>
		/// <param name="configFilename">配置文件名</param>
		/// <param name="sectionName"></param>
		/// <param name="containerName"></param>
		/// <exception cref="ArgumentNullException">container为null</exception>
		/// <returns></returns>
		public static IUnityContainer LoadConfiguration(this IUnityContainer container, string configFilename, string sectionName, string containerName)
		{
			if (container == null) throw new ArgumentNullException(nameof(container));

			ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = configFilename };
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
			UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(sectionName);
			return container.LoadConfiguration(section, containerName);
		}
	}
}
#endif