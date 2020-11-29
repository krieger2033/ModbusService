using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Client.Models.User;
using ModbusMaster.Client.Services.Interfaces;

namespace ModbusMaster.Client.Factories.User
{
    public class UserFactory : IUserFactory
    {
        private readonly IUserService _userService;

        public UserFactory(IUserService userService)
        {
            _userService = userService;
        }

        public ApplicationUser GetUser(UserCreateViewModel viewModel)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = viewModel.Email,
                Email = viewModel.Email,
                EmailConfirmed = true
            };

            var ph = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = ph.HashPassword(user, viewModel.Password);

            return user;
        }

        public async Task<UserListViewModel> GetUserListViewModel()
        {
            return new UserListViewModel()
            {
                UserIndexViewModels = UsersToUserIndexViewModels(await _userService.ReadAll())
            };
        }

        public UserCreateViewModel GetUserCreateViewModel()
        {
            return new UserCreateViewModel();
        }

        private List<UserIndexViewModel> UsersToUserIndexViewModels(List<ApplicationUser> users)
        {
            var userIndexViewModels = new List<UserIndexViewModel>();

            users.ForEach(user =>
            {
                var role = user.UserRoles.FirstOrDefault();

                userIndexViewModels.Add(new UserIndexViewModel()
                {
                    Username = user.UserName,
                    Role = role?.Role.Name
                });
            });

            return userIndexViewModels;
        }
    }
}