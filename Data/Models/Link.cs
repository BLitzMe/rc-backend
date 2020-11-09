using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace RepairConsole.Data.Models
{
    public class Link
    {
        public int Id { get; set; }

        public Uri Uri { get; set; }

        public string Description { get; set; }

        public RepairDevice RepairDevice { get; set; }
        public int RepairDeviceId { get; set; }

        [NotMapped]
        public ICollection<LinkRating> Ratings { get; set; }

        [NotMapped, Range(1, 5)]
        public double? AverageRating => Ratings == null || Ratings.Count == 0 ? (double?) null : Ratings.Average(r => r.Value);

        public bool ShouldSerializeRatings() => false;

        public bool ShouldSerializeAverageRating() => AverageRating != null;
    }
}