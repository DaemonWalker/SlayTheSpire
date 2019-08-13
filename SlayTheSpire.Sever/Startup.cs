using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SlayTheSpire.Sever.Abstracts;
using SlayTheSpire.Sever.Extenssions;
using SlayTheSpire.Sever.Services;
using SlayTheSpire.Shared;

namespace SlayTheSpire.Sever
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InjectServices();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo() { Title = "É±Â¾¼âËþ´æµµÐÞ¸ÄÆ÷½Ó¿ÚÎÄµµ", Version = "1.0" });
                options.DocInclusionPredicate((docName, description) => true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.RegisterConsul(lifetime,
                new Consul.ServiceEntry()
                {
                    Service = new Consul.AgentService()
                    {
                        Port = Convert.ToInt32(configuration.GetValue<string>("urls").Split(':')[2]),
                        Address = configuration.GetValue<string>("urls").Split(':')[1].Replace("//", ""),
                        Service = Constances.SAVE_SERVICE_NAME
                    }
                });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "É±Â¾¼âËþ´æµµÐÞ¸ÄÆ÷½Ó¿ÚÎÄµµ");
            });

        }
    }
}
