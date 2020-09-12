using System.Collections.Generic;
using System.Linq;
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

        public RepairDevice GetRepairDevice(int id)
        {
            return _context.RepairDevices.Find(id);
        }

        public ICollection<RepairDevice> GetAllRepairDevices()
        {
            return _context.RepairDevices.ToList();
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