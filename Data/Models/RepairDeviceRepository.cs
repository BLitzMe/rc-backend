using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepairConsole.Data.Models
{
    public class RepairDeviceRepository : IRepairDeviceRepository
    {
        private readonly RepairContext _context;

        public RepairDeviceRepository(RepairContext context)
        {
            _context = context;
        }

        public async Task<RepairDevice> GetRepairDevice(int id)
        {
            var device = await _context.RepairDevices
                .Include(dev => dev.UserDevices)
                .Include(dev => dev.Documents)
                .Include(dev => dev.Links)
                .ThenInclude(link => link.Ratings)
                .FirstAsync(dev => dev.Id == id);

            device.AverageTimeTaken = TimeSpan.Zero;
            device.UserDevices ??= new List<UserDevice>();
            var num = 0;
            foreach (var userDevice in device.UserDevices)
            {
                if (!userDevice.TimeTaken.HasValue)
                    continue;

                device.AverageTimeTaken += userDevice.TimeTaken;
                num++;
            }

            if (num > 0)
                device.AverageTimeTaken /= num;

            return device;
        }

        public async Task<ICollection<RepairDevice>> GetAllRepairDevices()
        {
            var devices = await _context.RepairDevices
                .Include(dev => dev.Documents)
                .Include(dev => dev.Links)
                .ThenInclude(link => link.Ratings)
                .ToListAsync();

            return devices;
        }

        public RepairDevice AddRepairDevice(RepairDevice repairDevice)
        {
            _context.RepairDevices.Add(repairDevice);
            _context.SaveChanges();
            return repairDevice;
        }

        public RepairDevice UpdateRepairDevice(RepairDevice repairDevice)
        {
            _context.Entry(repairDevice).State = EntityState.Detached;
            var device = _context.RepairDevices.Attach(repairDevice);
            device.State = EntityState.Modified;
            _context.SaveChanges();

            return repairDevice;
        }
    }
}