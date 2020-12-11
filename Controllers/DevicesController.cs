using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepairConsole.Data.Models;

namespace RepairConsole.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IUserDeviceRepository _userDeviceRepository;
        private readonly IRepairDeviceRepository _repairDeviceRepository;

        public DevicesController(IUserDeviceRepository userDeviceRepository, IRepairDeviceRepository repairDeviceRepository)
        {
            _userDeviceRepository = userDeviceRepository;
            _repairDeviceRepository = repairDeviceRepository;
            _httpClient = new HttpClient();
        }

        public async Task<ICollection<UserDevice>> RefreshDevices()
        {
            var response = await _httpClient.GetAsync("https://effizientnutzen-reparatur.de/getDeviceTable/");

            if (!response.IsSuccessStatusCode)
                return null;

            var userDevicesJson = await response.Content.ReadAsStringAsync();
            var deviceDtos = JsonConvert.DeserializeObject<List<UserDeviceDto>>(userDevicesJson);
            var userDevices = new List<UserDevice>();
            foreach (var dto in deviceDtos)
            {
                var device = new UserDevice
                {
                    Id = int.Parse(dto.FormId.Split("-").Last()),
                    Category = dto.DeviceCat,
                    Model = dto.DeviceModel,
                    SerialNumber = dto.DeviceNo,
                    Description = dto.DeviceDescription,
                    Manufacturer = dto.DeviceManufacturer,
                    Age = dto.DeviceAge,
                    Defect = dto.DeviceDefect,
                    Manual = dto.DeviceManual.Normalize() == "y".Normalize(),
                    Powersupply = dto.DevicePowersupply.Normalize() == "y".Normalize(),
                    DeliveryDay = DateTime.TryParse(dto.DelivDay, out var deliveryDay) ? deliveryDay : (DateTime?) null
                };
                userDevices.Add(device);
            }

            foreach (var device in userDevices)
            {
                var userDevice = _userDeviceRepository.GetAllUserDevices().FirstOrDefault(ud => ud.Id == device.Id);
                if (userDevice == null)
                    _userDeviceRepository.AddUserDevice(device);
            }

            return userDevices;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var devices = _userDeviceRepository.GetAllUserDevices();
            if (devices.Count <= 0)
            {
                await RefreshDevices();
                devices = _userDeviceRepository.GetAllUserDevices();
                //RecurringJob.AddOrUpdate("device-refresh", () => RefreshDevices(), Cron.Minutely);
            }

            return Ok(devices);
        }

        [HttpPatch("{userDeviceId}/setRepairDevice")]
        public async Task<IActionResult> SetRepairDevice([FromRoute] int userDeviceId, [FromQuery] int id)
        {
            var userDevice = _userDeviceRepository.GetAllUserDevices()
                .FirstOrDefault(d => d.Id == userDeviceId);
            if (userDevice == null)
                return NotFound(new {message = $"User device with id {userDeviceId} not found"});

            var repairDevice = await _repairDeviceRepository.GetRepairDevice(id);
            if (repairDevice == null)
                return NotFound(new {message = $"Repair device with id {id} not found"});

            userDevice.RepairDeviceId = repairDevice.Id;
            userDevice.RepairDevice = repairDevice;
            userDevice = _userDeviceRepository.UpdateUserDevice(userDevice);

            return Ok(userDevice);
        }

        [HttpPatch("{userDeviceId}/setTimeTaken")]
        public IActionResult SetTimeTaken([FromRoute] int userDeviceId, [FromQuery] TimeSpan timeSpan)
        {
            var device = _userDeviceRepository.GetAllUserDevices()
                .FirstOrDefault(d => d.Id == userDeviceId);
            if (device == null)
                return NotFound(new {message = "Device unknown"});

            if (timeSpan <= TimeSpan.Zero)
                return BadRequest(new {message = "TimeSpan must be set to a positive value"});

            device.TimeTaken = timeSpan;
            device = _userDeviceRepository.UpdateUserDevice(device);

            return Ok(device);
        }
    }
}