﻿using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly QickDatabaseManangementContext _context;

        public ApplicationRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public async Task<Application> ChangeStatusApplication(string status, Guid? AppId)
        {
            try
            {
                var App = await _context.Applications
                    .Where(u => u.Id == AppId)
                    .FirstOrDefaultAsync();
                if (App != null)
                {
                    App.Status = status;
                }
                else
                {
                    { throw new Exception("Application does not exist"); }
                }

                await _context.SaveChangesAsync();
                return App;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Application> CreateApplication(CreateApplicationRequest request)
        {
            try
            {
                Application addApp = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    UniId = request.UniId,
                    UniSpecId = request.UniSpecId,
                    ApplyDate = DateTime.Now,
                    Status = Status.PENDING
                };
                await _context.Applications.AddAsync(addApp);
                await _context.SaveChangesAsync();
                return addApp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApplicationDetail> CreateApplicationDetail(CreateApplicationDetailRequest request)
        {
            try
            {
                ApplicationDetail addApp = new()
                {
                   Id = Guid.NewGuid(),
                   ApplicationId = request.ApplicationId,
                   AcademicRank = request.AcademicRank,
                   AvarageScore = request.AvarageScore,
                   CredentialBackImgUrl =request.CredentialBackImgUrl,
                   CredentialFrontImgUrl = request.CredentialFrontImgUrl,
                   GraduationYear = request.GraduationYear,
                   HighSchoolAddress = request.HighSchoolAddress,
                   HighSchoolCode = request.HighSchoolCode,
                   HighSchoolName = request.HighSchoolName,
                    SchoolReport1Url = request.SchoolReport1Url,
                    SchoolReport2Url = request.SchoolReport2Url,
                    SchoolReport3Url = request.SchoolReport3Url,
                    SchoolReport4Url = request.SchoolReport4Url
                };
                await _context.ApplicationDetails.AddAsync(addApp);
                await _context.SaveChangesAsync();
                return addApp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Application>> GetApplicationByUniId(Guid? uniId)
        {
            try
            {
                var result = await _context.Applications
                                    .Include(u => u.User).Where(a => a.UserId == a.User.Id)
                                    .Include(x => x.UniSpec).Where(a => a.UniId == a.Uni.Id)
                                    .Include(a => a.Uni).Where(a => a.UniSpecId == a.UniSpec.Id)
                                    .Where(a => a.UniId == uniId)
                                    .ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Application>> GetApplicationByUserId(Guid? userId)
        {
            try
            {
                var result = await _context.Applications
                                    .Include(u => u.User).Where(a => a.UserId == a.User.Id)
                                    .Include(x => x.UniSpec).Where(a => a.UniId == a.Uni.Id)
                                    .Include(a => a.Uni).Where(a => a.UniSpecId == a.UniSpec.Id)
                                    .Where(a => a.UserId == userId)
                                    .ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Application> GetApplicationDetail(Guid? appId)
        {
            try
            {
                var result = await _context.Applications
                             .Include(u => u.Uni).Where(a => a.UniSpecId == a.UniSpec.Id)
                             .Include(u => u.User).Where(a => a.UserId == a.User.Id)
                             .Include(u => u.ApplicationDetails)
                             .Where(a => a.Id == appId)
                             .FirstOrDefaultAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
