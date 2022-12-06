﻿using Microsoft.EntityFrameworkCore;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class MajorRepository : IMajorRepository
    {
        private readonly QickDatabaseManangementContext _context;
        public MajorRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Major>> GetAllMajor()
        {
            try
            {
                var response = await _context.Majors
                    .ToListAsync();
                return response;
            }
            catch (Exception ex)    
            {

                throw ex;
            }
        }

        public async Task<Major> GetMajorById(Guid majorId)
        {
            try
            {
                var response = await _context.Majors
                    .Where(a => a.Id == majorId)
                    .FirstOrDefaultAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Specialization> GetSpecById(Guid specId)
        {
            try
            {
                var response = await _context.Specializations
                    .Where(a => a.Id == specId)
                    .FirstOrDefaultAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Specialization>> GetAllSpecDb()
        {
            try
            {
                var response = await _context.Specializations
                    .ToListAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Major>> GetMajorByJobId(int JobId)
        {
            try
            {
                var response = await _context.Majors
                    .Where(x => x.Id == x.JobMajors
                    .Where(a => a.JobId == JobId)
                    .FirstOrDefault().MajorId)
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Major>> GetMajorByUniId(Guid uniId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Specialization>> GetSpecByMajorId(Guid? MajorId)
        {
            try
            {
                var response = await _context.Specializations
                    .Where(x => x.MajorId == MajorId)
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
