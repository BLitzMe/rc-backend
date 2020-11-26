using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RepairConsole.Data.Models
{
    public class RepairDocument
    {
        public int Id { get; set; }

        public int RepairDeviceId { get; set; }

        public string FileName { get; set; }

        [NotMapped]
        public RepairDevice RepairDevice { get; set; }

        public bool ShouldSerializeRepairDevice() => false;
        public bool ShouldSerializeRepairDeviceId() => false;

        [NotMapped]
        public ICollection<DocumentRating> Ratings { get; set; }

        [NotMapped, Range(1, 5)]
        public double? AverageRating => Ratings == null || Ratings.Count == 0 ? (double?)null : Ratings.Average(r => r.Value);

        public bool ShouldSerializeRatings() => false;

        public bool ShouldSerializeAverageRating() => AverageRating != null;
    }
}