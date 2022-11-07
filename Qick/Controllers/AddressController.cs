using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Repositories.Interfaces;

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _repo;
        private readonly IMapper _mapper;

        public AddressController(IAddressRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Get all Province
        [HttpGet("province")]
        public async Task<IActionResult> GetProvince()
        {
            try
            {
                var response = await _repo.GetAllProvince();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get district by province id
        [HttpGet("district")]
        public async Task<IActionResult> GetDistrictByProvinceId(int ProvinceId)
        {
            try
            {
                var response = await _repo.GetDistrictByProvinceId(ProvinceId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get ward by district id
        [HttpGet("ward")]
        public async Task<IActionResult> GetWardByDistrictId(int DistrictId)
        {
            try
            {
                var response = await _repo.GetWardByDistrictId(DistrictId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
