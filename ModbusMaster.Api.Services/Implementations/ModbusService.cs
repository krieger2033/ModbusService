using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModbusMaster.Api.DAL.Interfaces;
using ModbusMaster.Api.Services.Interfaces;

namespace ModbusMaster.Api.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}


