using MetricsManagerService.Models;
using MetricsManagerService.Repositories;
using MetricsManagerService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetricsManagerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortLinkController : ControllerBase
    {
        private readonly MDbContext _context;
        public ShortLinkController(MDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<ShortLinkModel>> GetAll() => await _context.ShortLink.ToListAsync();

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var enty = _context.ShortLink.FirstOrDefault(i => i.Id == id);
            try
            {
                _ = _context.Remove(enty);
                _context.SaveChanges();
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult Post([FromBody] ShortLinkModel shortLink)
        {
            _context.Add(shortLink);
            _context.SaveChanges();
            return Ok(shortLink);
        }

        [HttpGet("gen")]
        public string Gen() => ShortLinkGeneratorService.Generate();
    }
}
