using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Gamification
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateDB(host);
            host.Run();
        }

        private static void CreateDB(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var servises = scope.ServiceProvider;
                try
                {
                    var context = servises.GetRequiredService<MyContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
