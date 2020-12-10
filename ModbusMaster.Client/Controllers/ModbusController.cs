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

        private IActionResult _getDefaultForm()
        {
            return PartialView("_TcpChannelCreatePartial", _modbusFactory.GetTcpChannelCreateViewModel());
        }

        // GET: ModbusController
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetConfig()
        {
            return PartialView("_ModbusConfigPartial", await _modbusFactory.GetChannelsListViewModel());
        }

        #region Channel

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

        public async Task<IActionResult> ChannelRemove(int id)
        {
            await _modbusService.Remove(await _modbusService.GetChannelById(id));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChannelDetails(int id)
        {
            var channel = await _modbusService.GetChannelById(id);

            if (channel.Type == ChannelType.SerialPort)
            {
                return PartialView("_SerialChannelEditPartial", _modbusFactory.GetSerialChannelEditViewModel(channel));
            }

            return PartialView("_TcpChannelEditPartial", _modbusFactory.GetTcpChannelEditViewModel(channel));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TcpChannelUpdate(TcpChannelEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _modbusService.Update(await _modbusFactory.UpdateChannel(model));
                return _getDefaultForm();
            }

            return PartialView("_TcpChannelEditPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SerialChannelUpdate(SerialChannelEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _modbusService.Update(await _modbusFactory.UpdateChannel(model));
                return _getDefaultForm();
            }

            return PartialView("_SerialChannelEditPartial", model);
        }

        #endregion

        #region Device

        public async Task<IActionResult> DeviceCreate(int channelId)
        {
            //TODO: channel existence check;

            ChannelType channelType = await _modbusService.GetChannelType(channelId);

            if (channelType == ChannelType.SerialPort)
            {
                return PartialView("_RtuDeviceCreatePartial", await _modbusFactory.GetRtuDeviceCreateViewModel(channelId));
            }

            return PartialView("_TcpDeviceCreatePartial", await _modbusFactory.GetTcpDeviceCreateViewModel(channelId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TcpDeviceCreate(TcpDeviceCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _modbusService.Create(_modbusFactory.GetDevice(model));
                return _getDefaultForm();
            }

            return PartialView("_TcpDeviceCreatePartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RtuDeviceCreate(RtuDeviceCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _modbusService.Create(_modbusFactory.GetDevice(model));
                return _getDefaultForm();
            }

            return PartialView("_RtuDeviceCreatePartial", model);
        }

        public async Task<IActionResult> DeviceRemove(int id)
        {
            await _modbusService.Remove(await _modbusService.GetDeviceById(id));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeviceDetails(int id)
        {
            var device = await _modbusService.GetDeviceById(id);
            ChannelType channelType = await _modbusService.GetChannelType(device.ChannelId);

            if (channelType == ChannelType.SerialPort)
            {
                return PartialView("_RtuDeviceEditPartial", _modbusFactory.GetRtuDeviceEditViewModel(device));
            }

            return PartialView("_TcpDeviceEditPartial", _modbusFactory.GetTcpDeviceEditViewModel(device));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TcpDeviceUpdate(TcpDeviceEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var device = await _modbusService.GetDeviceById(model.Id);
                await _modbusService.Update(await _modbusFactory.UpdateDevice(model));
                return _getDefaultForm();
            }

            return PartialView("_TcpDeviceEditPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RtuDeviceUpdate(RtuDeviceEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var device = await _modbusService.GetDeviceById(model.Id);
                await _modbusService.Update(await _modbusFactory.UpdateDevice(model));
                return _getDefaultForm();
            }

            return PartialView("_RtuDeviceEditPartial", model);
        }

        #endregion

        #region Register

        public async Task<IActionResult> RegisterCreate(int deviceId)
        {
            //TODO: device existence check

            return PartialView("_RegisterCreatePartial", await _modbusFactory.GetRegisterCreateViewModel(deviceId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCreate(RegisterCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _modbusService.Create(_modbusFactory.GetRegister(model));
                return _getDefaultForm();
            }

            return PartialView("_RegisterCreatePartial", model);
        }

        public async Task<IActionResult> RegisterRemove(int id)
        {
            await _modbusService.Remove(await _modbusService.GetRegisterById(id));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RegisterDetails(int id)
        {
            var register = await _modbusService.GetRegisterById(id); 
            return PartialView("_RegisterEditPartial", _modbusFactory.GetRegisterEditViewModel(register));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUpdate(RegisterEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _modbusService.Update(await _modbusFactory.UpdateRegister(model));
                return _getDefaultForm();
            }

            return PartialView("_RegisterEditPartial", model);
        }

        #endregion
    }
}
