﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;
using System.Security.Claims;

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/mail")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailRepository _repo;
        private readonly IMapper _mapper;

        public MailController(IMailRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Create Result for test
        [HttpPost("send")]
        public async Task<IActionResult> Send(CreateMessRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var response = await _repo.CreateMail(request,userId);

                if (response != null)
                {
                    var messResponse = await _repo.CreateMess(response.Id, request.MessageContent, MailType.SEND);
                    if (messResponse)
                    {
                        return Ok(new HttpStatusCodeResponse(200));
                    }
                    else
                    {
                        return Ok(new HttpStatusCodeResponse(204));
                    }      
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

        //Create Result for test
        [HttpPost("reply")]
        public async Task<IActionResult> Reply(CreateReplyRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var messResponse = await _repo.CreateMess(request.MailBoxId, request.MessageContent, MailType.REPLY);

                if (messResponse)
                {
                    return Ok(new HttpStatusCodeResponse(200));
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
        [HttpGet("mailbox")]
        public async Task<IActionResult> GetAllMailBoxById()
        {
            try
            {
                string Role = User.FindFirst(ClaimTypes.Role).Value.ToString();
                if (Role.Equals(Roles.MEMBER) || Role.Equals(Roles.STUDENT))
                {
                    Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var list = await _repo.GetMailBoxByUserId(userId);
                    return Ok(list);
                }
                else if (Role.Equals(Roles.STAFF) || Role.Equals(Roles.MANAGER))
                {
                    Guid uniId = Guid.Parse(User.FindFirst("university").Value);
                    var list = await _repo.GetMailBoxByUniId(uniId);
                    return Ok(list);
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
        [HttpGet("message")]
        public async Task<IActionResult> GetAllMessageById(Guid MailId)
        {
            try
            {
                    var list = await _repo.GetMessByMailId(MailId);
                    return Ok(list);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
