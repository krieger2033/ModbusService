using ModbusMaster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ModbusMaster.DAL.Interfaces
{
    public interface IDumpRepository: IRepository<Dump>
    {
        void AddRegisterResult(Register register, bool[] result);

        void AddRegisterResult(Register register, ushort[] result);

        void AddEmptyChannelResult(Channel channel, bool isTimedOut);

        void AddEmptyChannelResult(Channel channel);

        void AddEmptyDeviceResult(Device device);

        void AddEmptyRegisterResult(Register register);
    }
}
