using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairConsole.Data.Models
{
    public class RepairDevice
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        [NotMapped]
        public List<RepairDocument> Documents { get; set; }

        [NotMapped]
        public List<UserDevice> UserDevices { get; set; }

        public bool ShouldSerializeUserDevices() => false;
    }
}