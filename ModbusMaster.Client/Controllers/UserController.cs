using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ModbusMaster.Client.Factories.User;
using ModbusMaster.Client.Models.User;

namespace ModbusMaster.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserFactory _userFactory;

        public UserController(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public async Task<IActionResult> Index(UserListViewModel model)
        {
            return View(await _userFactory.GetUserListViewModel(model));
        }
    }
}
