using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModbusMaster.Service.Interfaces;

namespace ModbusMaster
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IModbusService _modbusService;

        public Worker(ILogger<Worker> logger, IModbusService modbusService)
        {
            _logger = logger;
            _modbusService = modbusService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Task task = Task.Run(() => _modbusService.StartWalkaround(stoppingToken), stoppingToken); // execution time <=1000 
                _logger.LogInformation("{date} - Walkaround start", DateTimeOffset.Now);

                await Task.WhenAll(task, Task.Delay(1000));
            }
        }
    }
}
