using System.Threading.Tasks;
using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Client.Models.User;

namespace ModbusMaster.Client.Factories.User
{
    public interface IUserFactory
    {
        Task<UserListViewModel> GetUserListViewModel();

        UserCreateViewModel GetUserCreateViewModel();

        ApplicationUser GetUser(UserCreateViewModel viewModel);
    }
}