﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;

namespace Qick.Controllers
{
    [Route("api/guest")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly ITestRepository _repo;
        private readonly IMapper _mapper;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repo"></param>
        /// 
        public GuestController(ITestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list test by guest
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-list-test-guest")]
        public async Task<IActionResult> GetAllTest()
        {
            try
            {
                var testList = await _repo.GetListTestGuest();
                var testListResponse = _mapper.Map<IEnumerable<ListTestResponse>>(testList);
                return Ok(testListResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        /// <summary>
        /// Get test detail by guest
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-test-detail-guest")]
        public async Task<IActionResult> GetTestDetail(int testId)
        {
            try
            { 
                var testDetail = await _repo.GetTestById(testId);
                var testDetailResponse = _mapper.Map<TestDetailResponse>(testDetail);
                return Ok(testDetailResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        /// <summary>
        /// Get test to attemp by guest
        /// </summary>
        /// <returns></returns>
        [HttpGet("taking-test-guest")]
        public async Task<IActionResult> TakingTest(int testId)
        {
            try
            {
                var takingTest = await _repo.GetTestToAttempForGuest(testId);
                var takingTestResponse = _mapper.Map<TakingTestResponse>(takingTest);
                return Ok(takingTestResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }
    }
}
