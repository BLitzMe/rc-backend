using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairConsole.Data.Models
{
    public class UserDevice
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public string Description { get; set; }

        public string Manufacturer { get; set; }

        public int Age { get; set; }

        public string Defect { get; set; }

        public bool Manual { get; set; }

        public bool Powersupply { get; set; }

        public DateTime? DeliveryDay { get; set; }

        public int RepairDeviceId { get; set; }
        public RepairDevice RepairDevice { get; set; }

        public IEnumerable<WorkDuration> Durations { get; set; }
    }
}