using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Services;
using Microsoft.Extensions.DependencyInjection;
using System.IO; 

namespace WebApplication
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); 
        } 

        public static IHostBuilder CreateHostBuilder(string[] args) => 
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            var env = hostingContext.HostingEnvironment;
            var appConfigPath = Path.Combine(env.ContentRootPath, "appsettings.json");
            config.AddJsonFile(appConfigPath, optional: false, reloadOnChange: true);
        });
    }
}
