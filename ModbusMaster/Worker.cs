using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ModbusMaster.DAL.Interfaces;
using ModbusMaster.Service.Interfaces;

namespace ModbusMaster
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IModbusService _modbusService;
        private readonly IUnitOfWork _unitOfWork;

        public Worker(ILogger<Worker> logger, IModbusService modbusService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _modbusService = modbusService;
            _unitOfWork = unitOfWork;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channels = _unitOfWork.ChannelsRepository.GetConfig();

            while (!stoppingToken.IsCancellationRequested)
            {
                Task task = Task.Run(() => _modbusService.StartWalkaround(channels, stoppingToken), stoppingToken); // execution time <=1000 
                _logger.LogInformation("{date} - Walkaround start", DateTimeOffset.Now);

                await Task.WhenAll(task, Task.Delay(1000));
            }
        }
    }
}
