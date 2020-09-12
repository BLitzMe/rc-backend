using System.Collections.Generic;

namespace RepairConsole.Data.Models
{
    public interface IRepairDocumentRepository
    {
        RepairDocument GetRepairDocument(int id);
        ICollection<RepairDocument> GetAllRepairDocuments();
        RepairDocument AddRepairDocument(RepairDocument document);
    }
}