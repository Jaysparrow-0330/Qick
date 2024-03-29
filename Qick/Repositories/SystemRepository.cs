﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        private readonly QickDatabaseManangementContext _context;
        private readonly IMapper _mapper;

        public SystemRepository(QickDatabaseManangementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

            public async Task<bool> CreateJob(JobRequest request)
        {
            try
            {
                Job addJob = new() 
                {
                    JobName = request.JobName,
                    Status = Status.ACTIVE,
                    Description = request.Description,
                    ImageUrl = request.ImageUrl
                };
                await _context.Jobs.AddAsync(addJob);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateJobCharMapping(JobCharMappingRequest request)
        {
            try
            {
                foreach (var Id in request.JobIds)
                {
                    JobMapping addJobMapping = new()
                    {
                        CharacterId = request.CharacterId,
                        JobId = Id,
                        Status = Status.ACTIVE,
                    };
                    await _context.JobMappings.AddAsync(addJobMapping);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateJobMajorMapping(JobMajorMappingRequest request)
        {
            try
            {
                foreach (var Id in request.MajorIds)
                {
                    JobMajor addJobMajor = new()
                    {
                       JobId = request.JobId,
                       MajorId = Id,
                       Status = Status.ACTIVE
                    };
                    await _context.JobMajors.AddAsync(addJobMajor);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateMajor(MajorRequest request)
        {
            try
            {
                Major addMajor = new()
                {
                    Id = Guid.NewGuid(),
                    MajorName = request.MajorName,
                    Status = Status.ACTIVE,
                    Description = request.Description,
                    MajorCode = request.MajorCode
                };
                await _context.Majors.AddAsync(addMajor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateSpec(SpecRequest request)
        {
            try
            {
                Specialization addSpec = new()
                {
                    Id = Guid.NewGuid(),
                    MajorId = request.MajorId,
                    Description = request.Description,
                    SpecName = request.SpecName,
                    Status = Status.ACTIVE
                };
                await _context.Specializations.AddAsync(addSpec);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Major> UpdateMajor(UpdateMajorRequest request)
        {
            try
            {
                var major = await _context.Majors
                    .Where(u => u.Id == request.Id)
                    .FirstOrDefaultAsync();

                if (major != null)
                {
                    major.MajorName = request.MajorName;
                    major.MajorCode = request.MajorCode;
                    major.Description = request.Description;
                    major.Status = request.Status;
                }
                else
                {
                    { throw new Exception("Major does not exist"); }
                }

                await _context.SaveChangesAsync();
                return major;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Character> UpdateCharacter(UpdateCharacterRequest request)
        {
            try
            {
                var character = await _context.Characters
                    .Include(m => m.JobMappings)
                    .Where(u => u.Id == request.Id)
                    .FirstOrDefaultAsync();

                if (character != null)
                {
                    _context.RemoveRange(character.JobMappings);
                    _mapper.Map(request, character);
                    character.ResultSummary = request.ResultSummary;
                    character.ResultCareer = request.ResultCareer;
                    character.ResultSuccessRule = request.ResultSuccessRule;
                    character.ResultRelationship = request.ResultRelationship;
                    character.ResultShortName = request.ResultShortName;
                    character.ResultName = request.ResultName;
                    character.ResultPictureUrl = request.ResultPictureUrl;
                    character.Value = request.Value;
                }
                else
                {
                    { throw new Exception("Character does not exist"); }
                }

                if (await _context.SaveChangesAsync() <= 0) throw new Exception("Nothing changes");
                return character;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Character> DeleteChar(Guid charId)
        {
            try
            {
                var user = await _context.Characters
                    .Where(u => u.Id == charId)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    if (user.Status == Status.ACTIVE)
                    {
                        user.Status = Status.DISABLE;
                    }
                    else if (user.Status == Status.DISABLE)
                    {
                        user.Status = Status.ACTIVE;
                    }

                }
                else
                {
                    { throw new Exception("Character does not exist"); }
                }

                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Character> GetCharacterById(Guid Id)
        {
            try
            {
                var response = await _context.Characters
                    .Where(u => u.Id == Id)
                    .FirstOrDefaultAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<DashboardAdminResponse> GetDashboardAdmin()
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.Status == Status.ACTIVE && (u.RoleId == Roles.MEMBER || u.RoleId == Roles.STUDENT))
                    .ToListAsync();

                var uni = await _context.Universities
                    .Where(u => u.Status == Status.ACTIVE)
                    .ToListAsync();

                var attempt =  await _context.Attempts
                    .Where(u => u.Status == Status.ACTIVE)
                    .ToListAsync();

                var test = await _context.Tests
                    .Where(u => u.Status == Status.ACTIVE)
                    .ToListAsync();

                DashboardAdminResponse response = new()
                {
                totalAttempt = attempt.Count(),
                totalTest = test.Count(),
                totalUni = uni.Count(),
                totalUser = user.Count()
                };
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Specialization> UpdateSpec(UpdateSpecRequest request)
        {
            try
            {
                var spec = await _context.Specializations
                    .Where(u => u.Id == request.Id)
                    .FirstOrDefaultAsync();

                if (spec != null)
                {
                    spec.SpecName = request.SpecName;
                    spec.MajorId = request.MajorId;
                    spec.Status = request.Status;
                }
                else
                {
                    { throw new Exception("Spec does not exist"); }
                }

                await _context.SaveChangesAsync();
                return spec;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
