using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        // get all Province 
        Task<IEnumerable<Province>> GetAllProvince();
        // get Jobs by character Id
        Task<IEnumerable<District>> GetDistrictByProvinceId(int ProvinceId);
        // get Jobs by character Id
        Task<IEnumerable<Ward>> GetWardByDistrictId(int DistricId);
    }
}
