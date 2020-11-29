using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ModbusMaster.Client.Factories.Modbus;
using ModbusMaster.Client.Models.Modbus;
using ModbusMaster.Client.Services.Interfaces;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Controllers
{
    [Authorize]
    public class ModbusController : Controller
    {
        private readonly IModbusFactory _modbusFactory;
        private readonly IModbusService _modbusService;

        public ModbusController(IModbusFactory modbusFactory, IModbusService modbusService)
        {
            _modbusFactory = modbusFactory;
            _modbusService = modbusService;
        }

        // GET: ModbusController
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetConfig()
        {
            return PartialView("_ModbusConfigPartial", _modbusFactory.GetChannelsListViewModel().Result);
        }

        public IActionResult ChannelCreate(ChannelType type = ChannelType.Tcp)
        {
            if (type == ChannelType.SerialPort)
            {
                return PartialView("_SerialChannelCreatePartial", _modbusFactory.GetSerialChannelCreateViewModel());
            }

            return PartialView("_TcpChannelCreatePartial", _modbusFactory.GetTcpChannelCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TcpChannelCreate(TcpChannelCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _modbusService.Create(_modbusFactory.GetChannel(model));
                model = _modbusFactory.GetTcpChannelCreateViewModel();
                //RedirectToAction(nameof(Index));
            }

            return PartialView("_TcpChannelCreatePartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SerialChannelCreate(SerialChannelCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _modbusService.Create(_modbusFactory.GetChannel(model));
                model = _modbusFactory.GetSerialChannelCreateViewModel();
                //RedirectToAction(nameof(Index));
            }

            return PartialView("_SerialChannelCreatePartial", model);
        }

        // GET: ModbusController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModbusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ModbusController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModbusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
