using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepairConsole.Data.Models
{
    public interface IRepairDocumentRepository
    {
        RepairDocument GetRepairDocument(int id);
        ICollection<RepairDocument> GetAllRepairDocuments();
        RepairDocument AddRepairDocument(RepairDocument document);
        Task<ICollection<RepairDocument>> AddMultipleRepairDocumentsAsync(ICollection<RepairDocument> documents);
    }
}