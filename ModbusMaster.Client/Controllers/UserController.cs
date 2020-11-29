using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Client.Factories.User;
using ModbusMaster.Client.Models.User;
using ModbusMaster.Client.Services.Interfaces;

namespace ModbusMaster.Client.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserFactory _userFactory;
        private readonly IUserService _userService;

        public UserController(IUserFactory userFactory, IUserService userService)
        {
            _userFactory = userFactory;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userFactory.GetUserListViewModel());
        }

        public async Task<IActionResult> Remove(string key)
        {
            ApplicationUser user = await _userService.GetUser(key);

            if (user != null)
            {
                await _userService.Remove(user);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View(_userFactory.GetUserCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = await _userService.Add(_userFactory.GetUser(model));

                if(result) { return RedirectToAction("Index"); }

                ModelState.AddModelError("", "Error adding user");
            }

            return View(model);
        }
    }
}
