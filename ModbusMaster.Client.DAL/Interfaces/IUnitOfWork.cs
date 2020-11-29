using System.Threading.Tasks;
using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IChannelRepository ChannelsRepository { get; }
        IRepository<DeviceConfig> DevicesRepository { get; }
        IRepository<RegisterConfig> RegistersRepository { get; }
        IRepository<DumpConfig> DumpsRepository { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
