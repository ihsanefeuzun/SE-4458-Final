using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Model;
using WebApplication1.Model.Dto;
using WebApplication1.Source.Svc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DonationController : Controller
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        [HttpGet("getBloodDonations")]
        public List<BloodDonation> Get()
        {
            List<BloodDonation> datas = _donationService.getBloodDonations();

            return datas;
        }
        

        [HttpPost("addBlood")]
        public IActionResult AddBlood([FromBody] BloodRequest request)
        {
            var result = _donationService.AddBlood(request.DonorName, request.Units);
            return Ok(result);
        }

        [HttpPost("requestBlood")]
        public IActionResult RequestBlood([FromBody] BloodRequest request)
        {
            var result = _donationService.RequestBlood(request);
            return Ok(result);
        }
    }

    public class BloodRequest
    {
        public string DonorName { get; set; }
        public int Units { get; set; }
    }
}
