using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Repositories.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace Qick.Controllers
{
    [Authorize]
    [Route("api/university")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _repo;
        private readonly INewsRepository _repoNews;
        private readonly IFQARepository _repoFqa;
        private readonly IUserRepository _repoUser;
        private readonly IMajorRepository _repoMajor;
        private readonly IMapper _mapper;

        public UniversityController(IFQARepository repoFqa,IMajorRepository repoMajor,IUserRepository repoUser,INewsRepository repoNews,IUniversityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _repoNews= repoNews;
            _mapper = mapper;
            _repoUser = repoUser;
            _repoFqa = repoFqa;
            _repoMajor = repoMajor;
        }

        //Get all University
        [AllowAnonymous]
        [HttpGet("get-university")]
        public async Task<IActionResult> GetUniversity(string? status, Guid? UniId)
        {
            try
            {
                if(UniId != null)
                {
                    var response = await _repo.GetUniversityDetail(UniId);
                    var detailResponse = _mapper.Map<UniDetailResponse>(response);
                    return Ok(detailResponse);
                }
                else
                {
                    var response = await _repo.GetListAllUniversity(status);
                    var ListUniResponse = _mapper.Map<IEnumerable<ListUniResponse>>(response);
                    return Ok(ListUniResponse);
                }
                
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpGet("staffs")]
        public async Task<IActionResult> GetAllStaff()
        {
            try
            {
                Guid uniId = Guid.Parse(User.FindFirst("university").Value);
                var response = await _repoUser.GetListAllStaff(uniId);
                var profile = _mapper.Map<IEnumerable<ListUserResponse>>(response);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpGet("active-staffs")]
        public async Task<IActionResult> GetAllActiveStaff()
        {
            try
            {
                Guid uniId = Guid.Parse(User.FindFirst("university").Value);
                var response = await _repoUser.GetListActiveStaff(uniId);
                var profile = _mapper.Map<IEnumerable<ListUserResponse>>(response);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        //Get all University
        [AllowAnonymous]
        [HttpGet("get-university-spec")]
        public async Task<IActionResult> GetUniversitySpec(Guid? UniId)
        {                            
            try
            {
                    var response = await _repo.GetListAllUniversitySpec(UniId);
                    return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //GET all news
        [AllowAnonymous]
        [HttpGet("news")]
        public async Task<IActionResult> GetAllNews(Guid? UniId)
        {
            try
            {
                if(UniId != null)
                {
                    var news = await _repoNews.GetNewsByUniId(UniId);
                    var response = _mapper.Map<IEnumerable<ListNewsResponse>>(news);
                    return Ok(response);
                }
                else {
                    var news = await _repoNews.GetAllNews();
                    var response = _mapper.Map<IEnumerable<ListNewsResponse>>(news);
                    return Ok(response);
                }
            }
            catch (Exception ex )
            {

                return Ok(ex.Message) ;
            }
        }

        //TODO : GetNewsById

        //GET all candidates by staff
        [Authorize(Roles = Roles.STAFF)]
        [HttpGet("candidates")]
        public async Task<IActionResult> GetAllCandidate()
        {
            try
            {
                Guid uniId = Guid.Parse(User.FindFirst("university").Value);
                var list = await _repoUser.GetListAllCandidate(uniId);
                var response = _mapper.Map<IEnumerable<ListCandidateResponse>>(list);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Ok(ex.Message);
            }
        }

        //GET candidate detail by staff
        [Authorize(Roles = Roles.STAFF)]
        [HttpGet("candidate-detail")]
        public async Task<IActionResult> GetCandidate(Guid userId)
        {
            try
            {
                Guid uniId = Guid.Parse(User.FindFirst("university").Value);
                var user = await _repoUser.GetCandidate(userId);
                var response = _mapper.Map<CandidateResponse>(user);
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Get University by Major Id
        [AllowAnonymous]
        [HttpGet("get-uni-major")]
        public async Task<IActionResult> GetUniversityByMajorId(Guid majorId)
        {
            try
            {
                var response = await _repo.GetUniversityByMajorId(majorId);
                var ListUniResponse = _mapper.Map<IEnumerable<ListUniResponse>>(response);
                return Ok(ListUniResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Get dashboard data for uni's account
        [Authorize(Roles = Roles.STAFF + "," + Roles.MANAGER)]
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            try
            {
                Guid uniId = Guid.Parse(User.FindFirst("university").Value);
                var response = await _repo.GetDashboardUni(uniId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //GET all fqas of a university
        [AllowAnonymous]
        [HttpGet("fqas")]
        public async Task<IActionResult> GetAllFQAByUni(Guid? UniId,string? status)
        {
            try
            {
                if (UniId != null)
                {
                    if (status == null)
                    {
                        var list = await _repoFqa.GetListUniFQA(UniId);
                        var response = _mapper.Map<IEnumerable<FQAResponse>>(list);
                        return Ok(response);
                    }
                    else
                    {
                        // Result will serve the format of :
                        // Topic {... FQA[] }[]
                        var list = await _repoFqa.GetUniFQAById(UniId);
                        var response = _mapper.Map<IEnumerable<ListFqaResponse>>(list);
                        return Ok(response);
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
        //GET all fqas of a university
        [AllowAnonymous]
        [HttpGet("fqa")]
        public async Task<IActionResult> GetFQAByUni(int contentId)
        {
            try
            {
                if (contentId != null)
                {
                    var list = await _repoFqa.GetFqaById(contentId);
                    if (list != null)
                    {
                        var response = _mapper.Map<FQAResponse>(list);
                        return Ok(response);
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
        [AllowAnonymous]
        [HttpGet("majoruni")]
        public async Task<IActionResult> GetMajorUni(Guid uniId)
        {
            try
            {
                var response = await _repoMajor.GetMajorByUniId(uniId);
                var listResponse = _mapper.Map<IEnumerable<ListMajorUniResponse>>(response);
                return Ok(listResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //Create Test step one create basic information of test , return test to create questions, option, etc.
        [Authorize(Roles = Roles.STAFF + "," + Roles.MANAGER)]
        [HttpPost("unispec-create")]
        public async Task<IActionResult> CreateUniSpec(CreateUniSpecRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                string status = User.FindFirst("status").Value;
                if(status == Status.ACTIVE)
                {
                    var response = await _repo.CreateUniversitySpec(request);
                    if (response)
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
                    return Ok(new HttpStatusCodeResponse(210));
                }
                
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [Authorize(Roles = Roles.STAFF)]
        [HttpPost("create-fqa")]
        public async Task<IActionResult> CreateFqa(CreateFQARequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                Guid uniId = Guid.Parse(User.FindFirst("university").Value);
                var response = await _repoFqa.CreateFQA(request, uniId,userId);
                if (response)
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

        //Create news by staff
        [Authorize(Roles = Roles.STAFF)]
        [HttpPost("create-news")]
        public async Task<IActionResult> CreateNews(CreateNewsRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                Guid uniId = Guid.Parse(User.FindFirst("university").Value);
                var response = await _repoNews.CreateNews(request,uniId,userId);
                if (response)
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

        [Authorize(Roles = Roles.MANAGER)]
        //Approve news by uni manager
        [HttpPut("approve-news")]
        public async Task<IActionResult> ApproveNews(int newsId,string status)
        {
            try
            {
                var response = await _repoNews.ApproveNews(newsId, status);
                if (response != null)
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

        [Authorize(Roles = Roles.STAFF)]
        //Delete news by staff
        [HttpPut("delete-news")]
        public async Task<IActionResult> DeleteNews(int newsId)
        {
            try
            {
                var response = await _repoNews.DeleteNews(newsId);
                if (response)
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

        [Authorize(Roles = Roles.STAFF)]
        //Update news by staff
        [HttpPut("update-news")]
        public async Task<IActionResult> UpdateNews(UpdateNewsRequest request)
        {
            try
            {
              
                var response = await _repoNews.UpdateNews(request);
                if (response != null)
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

        [Authorize(Roles = Roles.STAFF)]
        //Update fqa by staff
        [HttpPut("update-fqa")]
        public async Task<IActionResult> UpdateFqa(UpdateFQARequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                Guid uniId = Guid.Parse(User.FindFirst("university").Value);
                string status = User.FindFirst("status").Value;
                if (status == Status.ACTIVE)
                {
                    if(userId != null)
                    {
                        var response = await _repoFqa.UpdateFQA(request);
                        
                        if(response != null)
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
                else
                {
                    return Ok(new HttpStatusCodeResponse(210));
                }
            }
            catch (Exception ex)
            {

                return Ok(ex.Message);
            }
        }

        [Authorize(Roles = Roles.STAFF)]
        //Delete fqa by staff
        [HttpPut("delete-fqa")]
        public async Task<IActionResult> DeleteFqa(int fqaId)
        {
            try
            {
                var response = await _repoFqa.DeleteFQA(fqaId);
                if (response != null)
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

        [Authorize(Roles = Roles.MANAGER)]
        //Update User Profiel
        [HttpPut("update-uni")]
        public async Task<IActionResult> UpdateUniversity(UpdateUniRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                Guid uniId = Guid.Parse(User.FindFirst("university").Value);
                string status = User.FindFirst("status").Value;
                if (status == Status.ACTIVE)
                {
                    if (userId != null)
                    {
                        var uni = await _repo.UpdateUni(request, uniId);
                        return Ok(uni);
                    }
                    else
                    {
                        return Ok(new HttpStatusCodeResponse(204));
                    }
                }
                else
                {
                    return Ok(new HttpStatusCodeResponse(210));
                }
                

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        //Update ACc Profiel
        [Authorize(Roles = Roles.MANAGER)]
        [HttpPut("ban/unban-staff")]
        public async Task<IActionResult> BanUnbanStaff(Guid userId)
        {
            try
            {
                string status = User.FindFirst("status").Value;
                if (status == Status.ACTIVE)
                {
                    if (userId != null)
                    {
                        var check = await _repoUser.BanUser(userId);
                        if (check != null)
                        {
                            return Ok(check);
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
                else
                {
                    return Ok(new HttpStatusCodeResponse(210));
                }
                

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
