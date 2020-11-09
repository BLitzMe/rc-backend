using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepairConsole.Data.Models;

namespace RepairConsole.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkController : Controller
    {
        private readonly ILinkRepository _linkRepository;

        public LinkController(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> PostLink([FromBody] Link link)
        {
            if (link == null)
                return BadRequest(new {message = "No Link object found in request body"});

            return Ok(await _linkRepository.AddLinkAsync(link));
        }

        [HttpPost("{id}/rating")]
        public async Task<IActionResult> AddRating([FromRoute] int id, [FromQuery] int value)
        {
            if (value < 1 || value > 5)
                return BadRequest(new {message = "Values for rating must range from 1 to 5"});

            var link = await _linkRepository.GetLinkByIdAsync(id);
            if (link == null)
                return NotFound();

            return Ok(await _linkRepository.AddRatingAsync(id, value));
        }
    }
}