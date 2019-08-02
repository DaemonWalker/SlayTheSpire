using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlayTheSpire.Shared
{
    public static class ConsulUtils
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder application, IHostApplicationLifetime lifetime, ServiceEntry entry)
        {
            var consulClient = new ConsulClient(x => new Uri("http://127.0.0.1:8500"));
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://{entry.Service.Address}:{entry.Service.Port}/api/health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(5)
            };

            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = entry.Service.ID,
                Name = entry.Service.Service,
                Address = entry.Service.Address,
                Port = entry.Service.Port,
                Tags = new[] { $"urlprefix-/{entry.Service.Service}" }//	添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });

            return application;
        }
        public static string GetSaveServiceUrl()
        {
            var consulClient = new ConsulClient(x => new Uri("http://127.0.0.1:8500"));
            var saveService = consulClient.Agent.Services().Result.Response[Constances.SAVE_SERVICE_NAME];
            return $"http://{saveService.Address}:{saveService.Port}";
        }
    }
}
