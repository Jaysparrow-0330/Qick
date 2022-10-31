﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;
using System.Security.Claims;

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/application")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository _repo;
        private readonly IMapper _mapper;

        public ApplicationController(IApplicationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Create Academic profile
        [HttpPost("create-application")]
        public async Task<IActionResult> CreateApplication(CreateApplicationRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repo.CreateApplication(request);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return Ok(new HttpStatusCodeResponse(204));
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Create Academic profile
        [HttpPost("create-application-detail")]
        public async Task<IActionResult> CreateApplicationDetail(CreateApplicationDetailRequest AppDetail)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repo.CreateApplicationDetail(AppDetail);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return Ok(new HttpStatusCodeResponse(204));
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // Get list all question by test id
        [HttpGet("get-all-application")]
        public async Task<IActionResult> GetAllApplicationByUniId(Guid? UniId)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string Role = User.FindFirst(ClaimTypes.Role).Value.ToString();
                var applicationList = await _repo.GetApplication(UniId);
                var applicationListResponse = _mapper.Map<IEnumerable<ListApplicationResponse>>(applicationList) ;
                return Ok(applicationListResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
