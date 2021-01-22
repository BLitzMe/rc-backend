using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepairConsole.Data.Models
{
    public interface IUserDeviceRepository
    {
        UserDevice GetUserDevice(int id);
        ICollection<UserDevice> GetAllUserDevices();
        UserDevice AddUserDevice(UserDevice userDevice);
        UserDevice UpdateUserDevice(UserDevice userDevice);
        UserDevice DeleteUserDevice(int id);
        Task<WorkDuration> AddOrUpdateWorkDurationAsync(WorkDuration duration);
    }
}