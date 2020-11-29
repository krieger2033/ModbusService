using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using ModbusMaster.Client.DAL.Interfaces;
using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Client.Services.Interfaces;

namespace ModbusMaster.Client.Services.Implementations
{
    public class ModbusService : IModbusService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IUnitOfWork _unitOfWork;

        public ModbusService(IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
        {
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
        }

        private string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task<List<ChannelConfig>> GetModbusConfig()
        {
            return await _unitOfWork.ChannelsRepository.GetConfig(GetCurrentUserId());
        }

        public async Task Create(ChannelConfig channel)
        {
            channel.UserId = GetCurrentUserId();

            _unitOfWork.ChannelsRepository.Insert(channel);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}   