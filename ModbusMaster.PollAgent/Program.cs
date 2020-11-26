using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ModbusMaster.DAL;
using ModbusMaster.PollAgent.DAL.Implementations;
using ModbusMaster.PollAgent.DAL.Interfaces;
using ModbusMaster.PollAgent.Services.Implementations;
using ModbusMaster.PollAgent.Services.Interfaces;

namespace ModbusMaster.PollAgent
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
                        options => options.UseOracle(config.GetConnectionString("DefaultConnection")), 
                        ServiceLifetime.Transient, 
                        ServiceLifetime.Transient
                    );

                    services.AddSingleton<IUnitOfWork, UnitOfWork>();

                    services.AddTransient<IModbusService, ModbusService>();

                    services.AddHostedService<Worker>();
                });
    }
}
