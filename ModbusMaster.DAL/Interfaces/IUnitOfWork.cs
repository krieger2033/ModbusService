using ModbusMaster.Domain.Entities;

namespace ModbusMaster.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Channel> ChannelsRepository { get; }
        IRepository<Device> DevicesRepository { get; }
        IRepository<Register> RegistersRepository { get; }
        IDumpRepository DumpsRepository { get; }
        int SaveChanges();
    }
}
