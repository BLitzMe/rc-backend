namespace RepairConsole.Data.Models
{
    public class DocumentRating : Rating
    {
        public RepairDocument Document { get; set; }

        public int DocumentId { get; set; }
    }
}