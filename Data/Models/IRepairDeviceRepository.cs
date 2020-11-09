using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepairConsole.Data.Models
{
    public interface IRepairDeviceRepository
    {
        Task<RepairDevice> GetRepairDevice(int id);
        Task<ICollection<RepairDevice>> GetAllRepairDevices();
        RepairDevice AddRepairDevice(RepairDevice repairDevice);
        RepairDevice UpdateRepairDevice(RepairDevice repairDevice);
    }
}