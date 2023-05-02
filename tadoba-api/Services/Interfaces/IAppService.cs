using tadoba_api.Models;

namespace tadoba_api.Services
{
    public interface IAppService
    {
        Task<Response<List<DropDownModel>>> GetCountries();
        Task<Response<List<DropDownModel>>> GetStates(long countryId);
        Task<Response<List<DropDownModel>>> GetCities(long stateId);
    }
}
