using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModbusMaster.DAL;
using ModbusMaster.DAL.Interfaces;
using ModbusMaster.DAL.Implementations;
using ModbusMaster.Service.Interfaces;
using ModbusMaster.Service.Implemetations;

namespace ModbusMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration config = hostContext.Configuration;

                    services.AddDbContext<ModbusDumpContext>(
                        options => options.UseLazyLoadingProxies()
                                    .UseOracle(config.GetConnectionString("DefaultConnection")), 
                        ServiceLifetime.Transient, 
                        ServiceLifetime.Transient
                    );

                    services.AddSingleton<IUnitOfWork, UnitOfWork>();

                    services.AddSingleton<IModbusService, ModbusService>();

                    services.AddHostedService<Worker>();
                });
    }
}
