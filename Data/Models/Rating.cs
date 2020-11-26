using System.ComponentModel.DataAnnotations;

namespace RepairConsole.Data.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Value { get; set; }
    }
}