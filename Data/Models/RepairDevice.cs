using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RepairConsole.Data.Models
{
    public class RepairDevice
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        [NotMapped]
        public ICollection<RepairDocument> Documents { get; set; }

        [NotMapped]
        public ICollection<UserDevice> UserDevices { get; set; }

        [NotMapped]
        public ICollection<Link> Links { get; set; }

        [NotMapped]
        public TimeSpan? AverageTimeTaken { get; set; }

        public bool ShouldSerializeUserDevices()
        {
            if (UserDevices == null)
                return false;

            if (UserDevices.Count > 0 && UserDevices.ToList()[0].RepairDevice != null)
                return false;

            return true;
        }
    }
}