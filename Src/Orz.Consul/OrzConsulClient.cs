using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Consul;

namespace Orz.Consul
{
    /// <summary>
    /// 封装<see cref="ConsulClient"/>的操作类
    /// </summary>
    public class OrzConsulClient : IDisposable
    {
        private readonly ConsulClient consulClient;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configAction"><see cref="ConsulClient"/>的配置方法</param>
        public OrzConsulClient(Action<ConsulClientConfiguration> configAction)
        {
            if (configAction == null) throw new ArgumentNullException(nameof(configAction));
            consulClient = new ConsulClient(configAction);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uri"><see cref="ConsulClient"/>的<see cref="Uri"/>地址</param>
        public OrzConsulClient(Uri uri) : this(config => config.Address = uri)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uriString"><see cref="ConsulClient"/>的uri字符串</param>
        public OrzConsulClient(string uriString) : this(config => config.Address = new Uri(uriString))
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip"><see cref="ConsulClient"/>的ip地址</param>
        /// <param name="port"><see cref="ConsulClient"/>的端口号</param>
        public OrzConsulClient(string ip, int port) : this($"http://{ip}:{port}")
        {
        }
        #endregion

        #region 注册服务
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceId">服务id</param>
        /// <param name="serviceName">服务名</param>
        /// <param name="serviceAddress">服务地址</param>
        /// <param name="servicePort">服务端口</param>
        /// <param name="check">健康检查</param>
        /// <param name="checks"></param>
        /// <param name="enableTagOverride"></param>
        /// <param name="tags"></param>
        /// <param name="meta"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public OrzConsulClient ServiceRegister(string serviceId, string serviceName, string serviceAddress, int servicePort, AgentServiceCheck check = null, AgentServiceCheck[] checks = null, bool enableTagOverride = false, string[] tags = null, IDictionary<string, string> meta = null, CancellationToken ct = default(CancellationToken))
        {
            var asr = new AgentServiceRegistration
            {
                ID = serviceId,
                Name = serviceName,
                Address = serviceAddress,
                Port = servicePort,
                Check = check,
                Checks = checks,
                EnableTagOverride = enableTagOverride,
                Tags = tags,
                Meta = meta,
            };
            return ServiceRegister(asr, ct);
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="service">服务注册信息</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public OrzConsulClient ServiceRegister(AgentServiceRegistration service, CancellationToken ct = default(CancellationToken))
        {
            if (service != null) consulClient.Agent.ServiceRegister(service, ct).Wait(ct);
            return this;
        }

        /// <summary>
        /// 批量注册服务
        /// </summary>
        /// <param name="services">服务注册信息集合</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public OrzConsulClient ServiceRegister(IEnumerable<AgentServiceRegistration> services, CancellationToken ct = default(CancellationToken))
        {
            if (services != null)
            {
                List<Task> list = new List<Task>();
                foreach (var service in services)
                {
                    list.Add(consulClient.Agent.ServiceRegister(service, ct));
                }
                Task.WaitAll(list.ToArray(), ct);
            }
            return this;
        }
        #endregion

        #region 注销服务
        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="serviceId">服务id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public OrzConsulClient ServiceDeregister(string serviceId, CancellationToken ct = default(CancellationToken))
        {
            consulClient.Agent.ServiceDeregister(serviceId, ct).Wait(ct);
            return this;
        }

        /// <summary>
        /// 批量注销服务
        /// </summary>
        /// <param name="serviceIds">服务id集合</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public OrzConsulClient ServiceDeregister(IEnumerable<string> serviceIds, CancellationToken ct = default(CancellationToken))
        {
            if (serviceIds != null)
            {
                List<Task> list = new List<Task>();
                foreach (var serviceId in serviceIds)
                {
                    list.Add(consulClient.Agent.ServiceDeregister(serviceId, ct));
                }
                Task.WaitAll(list.ToArray(), ct);
            }
            return this;
        }
        #endregion

        #region 获取服务
        /// <summary>
        /// 获取所有服务列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AgentService> GetAllServices()
        {
            return consulClient.Agent.Services().Result.Response.Values;
        }

        /// <summary>
        /// 根据服务id获取服务
        /// </summary>
        /// <param name="serviceId">服务id</param>
        /// <returns></returns>
        public AgentService GetServiceByServiceId(string serviceId)
        {
            return consulClient.Agent.Services().Result.Response.TryGetValue(serviceId, out var service) ? service : null;
        }

        /// <summary>
        /// 根据服务名获取服务列表
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="comparison">服务名比较规则</param>
        /// <returns></returns>
        public IEnumerable<AgentService> GetServicesByServiceName(string serviceName, StringComparison comparison = StringComparison.Ordinal)
        {
            return consulClient.Agent.Services().Result.Response.Values.Where(t => t.Service.Equals(serviceName, comparison));
        }

        /// <summary>
        /// 根据服务名获取第一个服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="comparison">服务名比较规则</param>
        /// <returns></returns>
        public AgentService GetFirstServiceByServiceName(string serviceName, StringComparison comparison = StringComparison.Ordinal)
        {
            return GetServicesByServiceName(serviceName, comparison).FirstOrDefault();
        }

        /// <summary>
        /// 根据服务名获取最后一个服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="comparison">服务名比较规则</param>
        /// <returns></returns>
        public AgentService GetLastServiceByServiceName(string serviceName, StringComparison comparison = StringComparison.Ordinal)
        {
            return GetServicesByServiceName(serviceName, comparison).LastOrDefault();
        }

        /// <summary>
        /// 根据服务名随机获取服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="comparison">服务名比较规则</param>
        /// <param name="random">随机函数</param>
        /// <returns></returns>
        public AgentService GetRandomServiceByServiceName(string serviceName, StringComparison comparison = StringComparison.Ordinal, Random random = null)
        {
            if (random == null) random = new Random(Guid.NewGuid().GetHashCode());

            var services = GetServicesByServiceName(serviceName, comparison).ToArray();
            if (services.Length == 0) return null;

            var index = random.Next(services.Length);
            return services[index];
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            consulClient?.Dispose();
        }
        #endregion
    }
}
