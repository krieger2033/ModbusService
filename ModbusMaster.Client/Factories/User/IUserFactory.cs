using System.Threading.Tasks;
using ModbusMaster.Client.Models.User;

namespace ModbusMaster.Client.Factories.User
{
    public interface IUserFactory
    {
        Task<UserListViewModel> GetUserListViewModel(UserListViewModel viewModel);
    }
}