using System.ComponentModel.DataAnnotations;

namespace RepairConsole.Data.Models
{
    public class LinkRating
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Value { get; set; }

        public Link Link { get; set; }
        
        public int LinkId { get; set; }
    }
}