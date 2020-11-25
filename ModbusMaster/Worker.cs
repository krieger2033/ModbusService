using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ModbusMaster.PollAgent.DAL.Interfaces;
using ModbusMaster.Domain.Entities;
using ModbusMaster.PollAgent.Services.Interfaces;

namespace ModbusMaster.PollAgent
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
            List<Channel> channels;
            try
            {
                channels = _unitOfWork.ChannelsRepository.GetConfig();
            }
            catch(Exception e)
            {
                _logger.LogError("{date} - Error getting configuration: {exception}", DateTimeOffset.Now, e.Message);
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                Task task = Task.Run(() => _modbusService.StartWalkaround(channels, stoppingToken), stoppingToken); // execution time <=1000 
                //_logger.LogInformation("{date} - Walkaround start", DateTimeOffset.Now);

                await Task.WhenAll(task, Task.Delay(1000));
            }
        }
    }
}
