using AttributeRouting.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Model.Dto;
using WebApplication1.Source.Svc;
using static System.Net.WebRequestMethods;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class DonorController : ControllerBase
    {

        private IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        //gets all donors
        [HttpGet]
        //[Authorize]
        public List<DonorDto> Get()
        {
            List<Donor> datas = _donorService.getDonors();
            List<DonorDto> ret = new List<DonorDto>();
            datas.ForEach(data => ret.Add(createDonorDto(data)));
            return ret;

        }

        //gets a donor with given id
        [HttpGet("{id}")]
        [Authorize]
        public DonorDto Get(long id)
        {
            Donor data = _donorService.getDonorById(id);
            return createDonorDto(data);
        }

        // inserts a donor 
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] DonorDto donorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
            
                _donorService.insertDonor(createDonor(donorDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        

        // updates a donor with given id
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] DonorDto donorDto)
        {
            if (donorDto.Id == 0)
            {
                return BadRequest("Cant update a donor with no id");
            }
            if (donorDto != null)
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        _donorService.updateDonor(createDonor(donorDto));
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete([FromBody] DonorDto donorDto)
        {
            if (donorDto.Id == 0)
            {
                return BadRequest("Cant delete a donor with no id");
            }
            if (donorDto != null)
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        _donorService.deleteDonor(createDonor(donorDto));
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        private DonorDto createDonorDto(Donor donor)
        {
            DonorDto dto = new DonorDto()
            {
               Id = donor.Id,
               FullName = donor.FullName,
               BloodType = donor.BloodType,
               Town = donor.Town,
               City = donor.City,
               PhoneNo = donor.PhoneNo,
                BranchId = donor.BranchId,
               PhotoUrl = "https://19070006002cdn.blob.core.windows.net/container/icon.jpg"
               
    

    };
            return dto;
        }

        private Donor createDonor(DonorDto dto)
        {
            Donor donor = new Donor()
            {
               Id = dto.Id,
               FullName = dto.FullName,
               BloodType = dto.BloodType,
               Town = dto.Town,
               City = dto.City,
               PhoneNo = dto.PhoneNo,
               BranchId = dto.BranchId,
               PhotoUrl = "https://19070006002cdn.blob.core.windows.net/container/icon.jpg"
            };
            return donor;
        }

    }
}
