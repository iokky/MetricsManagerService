using MetricsManagerService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerService.Controllers
{
    [Route("/")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private MDbContext _dbContext;
        public RedirectController(MDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet("{shortLink}")]
        public IActionResult Get(string shortLink)
        {
            try
            {
                var response = _dbContext.ShortLink.FirstOrDefault(i => i.ShortLink == shortLink).Url;
                return Redirect(response);
            }catch (Exception ex) { return BadRequest("https://www.microsoft.com/ru-ru"); }
        }

    }
}
