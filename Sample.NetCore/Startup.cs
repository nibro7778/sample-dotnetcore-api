using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Sample.API.Entity;
using Sample.API.Services;

namespace Sample.NetCore
{
    public class Startup
    {
        //dotnet core 1.0
        //public static IConfigurationRoot configuration { get; private set; }
        //public Startup(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //            .SetBasePath(env.ContentRootPath)
        //            .AddJsonFile("app.json", optional: false, reloadOnChange: true)
        //            .AddJsonFile($"app.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
        //    configuration = builder.Build();
        //}

        public static IConfiguration configuration { get; set; }
        
        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            var connection = Startup.configuration["connectionStrings:branchInfoDBContextString"];
            services.AddDbContext<BranchInfoContext>(o => o.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            //loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
