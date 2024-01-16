using Microsoft.AspNetCore.Mvc;
using WebApplication1.Source.Svc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BloodController : Controller
    {
        private readonly IBloodSearchService _bloodSearchService;

        public BloodController(IBloodSearchService bloodSearchService)
        {
            _bloodSearchService = bloodSearchService;
        }
        [HttpPost("NightlySearchForBloodRequests")]
        public IActionResult RequestBlood([FromBody] BloodRequest request)
        {
             _bloodSearchService.NightlySearchForBloodRequests(request);
            return Ok();
        }
    }
}
