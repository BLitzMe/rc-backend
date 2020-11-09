using System.Threading.Tasks;

namespace RepairConsole.Data.Models
{
    public interface ILinkRepository
    {
        Task<Link> AddLinkAsync(Link link);
        Task<Link> AddRatingAsync(int linkId, int rating);
        Task<Link> GetLinkByIdAsync(int id);
    }
}