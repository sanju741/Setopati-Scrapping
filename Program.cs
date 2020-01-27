using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using WebScrapping.Data;
using WebScrapping.Services;
using WebScrapping.Services.Setopati;

namespace WebScrapping
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File($"Log/Log{DateTime.Now:yyyy-MM-dd}.log")
                .CreateLogger();

            var connectionString = @"Data Source=DESKTOP-D4GLHFV\SQLEXPRESS;Initial Catalog=Scrapping;Integrated Security=True";
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IApplicationService, ApplicationService>()
                .AddSingleton<ISetopatiService, SetopatiService>()
                .AddSingleton<IChromeSeleniumService, ChromeSeleniumService>()

                .AddSingleton<IProductRepository, ProductRepository>()
                .AddLogging(configure => configure.AddSerilog())
                .AddDbContext<Context>(o => o.UseSqlServer(connectionString))
                .BuildServiceProvider();

            //do the actual work here
            var application = serviceProvider.GetService<IApplicationService>();
            application.Start();
        }

    }
}