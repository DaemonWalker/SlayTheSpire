using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SlayTheSpire.Shared;

namespace SlayTheSpire.Web
{
    /// <summary>
    /// WebHost Builder启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 获取config信息
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions
                    .AddPageRoute("/index", "")
                    .AddPageRoute("/about", "about.html");
            });
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="lifetime"></param>
        /// <param name="configuration"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            app.RegisterConsul(lifetime,
                new Consul.ServiceEntry()
                {
                    Service = new Consul.AgentService()
                    {
                        Port = configuration.GetValue<string>("urls").Split(':').Length > 2 ? Convert.ToInt32(configuration.GetValue<string>("urls").Split(':')[2]) : 80,
                        Address = configuration.GetValue<string>("urls").Split(':')[1].Replace("//", ""),
                        Service = Constances.SAVE_WEB_NAME
                    }
                });
        }
    }
}
