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
                _logger.LogInformation("{date} - Walkaround start", DateTimeOffset.Now);
                _modbusService.StartWalkaround(stoppingToken);

                await Task.Delay(1000, stoppingToken); // опрос не чаще секунды
                continue;
            }
        }
    }
}
