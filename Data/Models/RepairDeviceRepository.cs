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
                .Include(dev => dev.Documents)
                .Include(dev => dev.Links)
                .ThenInclude(link => link.Ratings)
                .FirstAsync(dev => dev.Id == id);

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