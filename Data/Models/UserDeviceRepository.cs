using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RepairConsole.Data.Models
{
    public class UserDeviceRepository : IUserDeviceRepository
    {
        private readonly RepairContext _repairContext;

        public UserDeviceRepository(RepairContext repairContext)
        {
            _repairContext = repairContext;
        }

        public UserDevice GetUserDevice(int id)
        {
            return _repairContext.UserDevices.Where(d => d.Id == id)
                .Include(d => d.RepairDevice)
                .ThenInclude(r => r.Documents)
                .FirstOrDefault();
        }

        public ICollection<UserDevice> GetAllUserDevices()
        {
            var devices = _repairContext.UserDevices.ToList();
            var repairDevices = _repairContext.RepairDevices.ToList();

            foreach (var repairDevice in repairDevices)
            {
                var devs = devices.Where(d => d.RepairDeviceId == repairDevice.Id).ToList();
                foreach (var dev in devs)
                {
                    dev.RepairDevice = repairDevice;
                }
            }

            return devices;
        }

        public UserDevice AddUserDevice(UserDevice userDevice)
        {
            var device = _repairContext.UserDevices.Find(userDevice.Id);
            if (device != null)
                return device;

            _repairContext.UserDevices.Add(userDevice);
            _repairContext.SaveChanges();
            return userDevice;
        }

        public UserDevice UpdateUserDevice(UserDevice userDevice)
        {
            _repairContext.Entry(userDevice).State = EntityState.Detached;
            var device = _repairContext.UserDevices.Attach(userDevice);
            device.State = EntityState.Modified;
            _repairContext.SaveChanges();

            return userDevice;
        }

        public UserDevice DeleteUserDevice(int id)
        {
            var device = _repairContext.UserDevices.Find(id);
            if (device != null)
            {
                _repairContext.UserDevices.Remove(device);
                _repairContext.SaveChanges();
            }

            return device;
        }
    }
}