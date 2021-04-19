using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSaver.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSaver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Task.Run(async () =>
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    await waitForDb(services);

                    try
                    {
                        var dbContext = services.GetRequiredService<UserContext>();
                        if (dbContext.Database.IsSqlServer())
                        {
                            dbContext.Database.Migrate();
                        }
                    }
                    catch (Exception ex)
                    {
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                    }
                }
            }).Wait();

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static async Task waitForDb(IServiceProvider services)
        {
            var healthChecker = services.GetRequiredService<UserContext>();
            var maxAttemps = 12;
            var delay = 5000;

            for (int i = 0; i < maxAttemps; i++)
            {
                if (healthChecker.Database.CanConnect())
                {
                    return;
                }
                await Task.Delay(delay);
            }

        }
    }
}
