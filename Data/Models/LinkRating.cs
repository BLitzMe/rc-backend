using System.ComponentModel.DataAnnotations;

namespace RepairConsole.Data.Models
{
    public class LinkRating : Rating
    {
        public Link Link { get; set; }
        
        public int LinkId { get; set; }
    }
}