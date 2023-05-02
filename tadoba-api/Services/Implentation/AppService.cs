using AutoMapper;
using tadoba_api.Entity;
using tadoba_api.Models;
using tadoba_api.Repository;

namespace tadoba_api.Services
{
    public class AppService : IAppService
    {
        private readonly IGenericRepository<DropDownMaster> _dropDownRepo;
        private readonly IMapper _mapper;
        public AppService(IGenericRepository<DropDownMaster> dropDownRepo, IMapper mapper)
        {
            _dropDownRepo = dropDownRepo;
            _mapper = mapper;
        }
        public async Task<Response<List<DropDownModel>>> GetCities(long stateId)
        {
            List<DropDownMaster> cities = (await _dropDownRepo.ListAllAsync(x => x.ParentId == stateId && x.IsActive && x.Type == enumDropdownMaster.City.ToString())).ToList();
            List<DropDownModel> result = _mapper.Map<List<DropDownModel>>(cities);
            return await Response<List<DropDownModel>>.GenerateResponse(true, result);
        }

        public async Task<Response<List<DropDownModel>>> GetCountries()
        {
            List<DropDownMaster> cities = (await _dropDownRepo.ListAllAsync(x => x.Type == enumDropdownMaster.Country.ToString() && x.IsActive)).ToList();
            List<DropDownModel> result = _mapper.Map<List<DropDownModel>>(cities);
            return await Response<List<DropDownModel>>.GenerateResponse(true, result);
        }

        public async Task<Response<List<DropDownModel>>> GetStates(long countryId)
        {
            List<DropDownMaster> cities = (await _dropDownRepo.ListAllAsync(x => x.ParentId == countryId && x.Type == enumDropdownMaster.State.ToString() && x.IsActive)).ToList();
            List<DropDownModel> result = _mapper.Map<List<DropDownModel>>(cities);
            return await Response<List<DropDownModel>>.GenerateResponse(true, result);
        }
    }
}
