using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AT_Core.Filters;
using AT_Core.Models;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AT_Core
{
    public class Startup
    {
        public static ILoggerRepository LoggerRepository { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LoggerRepository = LogManager.CreateRepository("NETCoreLogRepository");
            XmlConfigurator.Configure(LoggerRepository,new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ATDbContext>(opt=>opt.UseMySql(Configuration.GetConnectionString("ATDbConnectionString")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession();
            services.AddLogging();
            services.AddMvc(n =>
            {
                n.Filters.Add<ATActionFilter>();
                n.Filters.Add<AuthFilter>();
                n.Filters.Add<ATExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSpaStaticFiles();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            
            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
