//using ModbusMaster.Client.Domain.Entities;

namespace ModbusMaster.Client.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        //IRepository<Channel> ChannelsRepository { get; }
        //IRepository<Device> DevicesRepository { get; }
        //IRepository<Register> RegistersRepository { get; }
        //IRepository<Dump> DumpsRepository { get; }

        int SaveChanges();
    }
}
