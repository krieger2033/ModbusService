using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using ModbusMaster.Client.DAL.Interfaces;
//using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Client.Services.Interfaces;

namespace ModbusMaster.Client.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /*public Task<AppUser> GetUser(string username, string password)
        {
            return _unitOfWork.UsersRepository.GetSingle(u => u.Username == username && u.Password == password);
        }*/
    }
}   