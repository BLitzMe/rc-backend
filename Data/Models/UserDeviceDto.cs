using System;

namespace RepairConsole.Data.Models
{
    public class UserDeviceDto
    {
        public string FormId { get; set; }

        public string DeviceCat { get; set; }

        public string DeviceModel { get; set; }

        public string DeviceNo { get; set; }

        public string DeviceDescription { get; set; }

        public string DeviceManufacturer { get; set; }

        public int DeviceAge { get; set; }

        public string DeviceDefect { get; set; }

        public string DeviceManual { get; set; }

        public string DevicePowersupply { get; set; }

        public string DelivDay { get; set; }
    }
}