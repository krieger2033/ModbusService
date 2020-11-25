using ModbusMaster.Client.DAL.Interfaces;
using ModbusMaster.Client.Services.Interfaces;

namespace ModbusMaster.Client.Services.Implementations
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


