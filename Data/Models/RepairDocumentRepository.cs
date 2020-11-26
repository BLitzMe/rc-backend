using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepairConsole.Data.Models
{
    public class RepairDocumentRepository : IRepairDocumentRepository
    {
        private readonly RepairContext _context;

        public RepairDocumentRepository(RepairContext context)
        {
            _context = context;
        }

        public RepairDocument GetRepairDocument(int id)
        {
            return _context.RepairDocuments.Where(d => d.Id ==id).Include(d => d.Ratings).FirstOrDefault();
        }

        public ICollection<RepairDocument> GetAllRepairDocuments()
        {
            return _context.RepairDocuments.Include(d => d.Ratings).ToList();
        }

        public RepairDocument AddRepairDocument(RepairDocument document)
        {
            _context.RepairDocuments.Add(document);
            _context.SaveChanges();
            return document;
        }

        public async Task<ICollection<RepairDocument>> AddMultipleRepairDocumentsAsync(ICollection<RepairDocument> documents)
        {
            await _context.RepairDocuments.AddRangeAsync(documents);
            await _context.SaveChangesAsync();
            return documents;
        }

        public async Task<RepairDocument> AddRatingAsync(int docId, int rating)
        {
            await _context.DocumentRatings.AddAsync(new DocumentRating
            {
                DocumentId = docId,
                Value = rating
            });
            await _context.SaveChangesAsync();

            var doc = await _context.RepairDocuments
                .Include(d => d.Ratings)
                .FirstAsync(d => d.Id == docId);

            return doc;
        }
    }
}