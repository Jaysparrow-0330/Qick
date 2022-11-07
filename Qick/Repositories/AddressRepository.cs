using Microsoft.EntityFrameworkCore;
using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly QickDatabaseManangementContext _context;
        public AddressRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Province>> GetAllProvince()
        {
            try
            {
                var response = await _context.Provinces
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<District>> GetDistrictByProvinceId(int ProvinceId)
        {
            try
            {
                var response = await _context.Districts
                    .Where(x => x.ProvinceId == ProvinceId)
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public  async Task<IEnumerable<Ward>> GetWardByDistrictId(int DistricId)
        {
            try
            {
                var response = await _context.Wards
                    .Where(x => x.DistrictId == DistricId)
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
