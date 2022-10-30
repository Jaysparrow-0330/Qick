﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/major")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly IMajorRepository _repo;
        private readonly IMapper _mapper;

        public MajorController(IMajorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Get all major
        [AllowAnonymous]
        [HttpGet("get-major")]
        public async Task<IActionResult> GetJob()
        {
            try
            {
                var response = await _repo.GetAllMajor();
                var ListJobResponse = _mapper.Map<IEnumerable<MajorResponse>>(response);

                return Ok(ListJobResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get all Job
        [AllowAnonymous]
        [HttpGet("get-major-career")]
        public async Task<IActionResult> GetMajorByJobId(int jobId)
        {
            try
            {
                var response = await _repo.GetMajorByJobId(jobId);
                var ListJobResponse = _mapper.Map<IEnumerable<CareerMajorResponse>>(response);

                return Ok(ListJobResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
