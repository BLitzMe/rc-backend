using System.Collections.Generic;

namespace RepairConsole.Data.Models
{
    public interface IRepairDeviceRepository
    {
        RepairDevice GetRepairDevice(int id);
        ICollection<RepairDevice> GetAllRepairDevices();
        RepairDevice AddRepairDevice(RepairDevice repairDevice);
        RepairDevice UpdateRepairDevice(RepairDevice repairDevice);
    }
}