using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepairConsole.Data.Models
{
    public class LinkRepository : ILinkRepository
    {
        private readonly RepairContext _context;

        public LinkRepository(RepairContext context)
        {
            _context = context;
        }

        public async Task<Link> AddLinkAsync(Link link)
        {
            var repairDevice = await _context.RepairDevices.FirstOrDefaultAsync(dev => dev.Id == link.RepairDeviceId);
            if (repairDevice == null)
                return null;

            link.RepairDevice = repairDevice;

            await _context.Links.AddAsync(link);
            await _context.SaveChangesAsync();

            return link;
        }

        public async Task<Link> AddRatingAsync(int linkId, int rating)
        {
            await _context.LinkRatings.AddAsync(new LinkRating
            {
                LinkId = linkId,
                Value = rating
            });
            await _context.SaveChangesAsync();

            var link = await _context.Links
                .Include(l => l.Ratings)
                .FirstAsync(l => l.Id == linkId);

            return link;
        }

        public async Task<Link> GetLinkByIdAsync(int id)
        {
            return await _context.Links.Include(l => l.Ratings).FirstOrDefaultAsync();
        }
    }
}