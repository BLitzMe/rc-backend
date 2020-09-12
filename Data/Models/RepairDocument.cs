using System.ComponentModel.DataAnnotations.Schema;

namespace RepairConsole.Data.Models
{
    public class RepairDocument
    {
        public int Id { get; set; }

        public string Uri { get; set; }

        public int RepairDeviceId { get; set; }

        [NotMapped]
        public RepairDevice RepairDevice { get; set; }

        public bool ShouldSerializeRepairDevice() => false;
        public bool ShouldSerializeRepairDeviceId() => false;
    }
}