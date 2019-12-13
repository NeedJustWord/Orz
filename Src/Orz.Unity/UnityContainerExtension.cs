#if HAVE_UNITY_CONFIGURATION
using System;
using System.Configuration;
using Unity;

namespace Microsoft.Practices.Unity.Configuration
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
        /// <param name="configFilename">配置文件的相对路径(相对当前工作目录)或绝对路径</param>
        /// <param name="sectionName"></param>
        /// <param name="containerName"></param>
        /// <exception cref="ArgumentNullException"><paramref name="container"/>为null</exception>
        /// <returns></returns>
        public static IUnityContainer LoadConfiguration(this IUnityContainer container, string configFilename, string sectionName, string containerName)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = configFilename };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var section = (UnityConfigurationSection)configuration.GetSection(sectionName);
            return container.LoadConfiguration(section, containerName);
        }
    }
}
#endif