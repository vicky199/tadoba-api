using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tadoba_api.Services;

namespace tadoba_api.Controllers
{
    public class AppController : BaseController
    {
        private readonly IAppService _appService;
        public AppController(IAppService appService)
        {
            _appService = appService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _appService.GetCountries());
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetStates(long countryId)
        {
            return Ok(await _appService.GetStates(countryId));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCities(long stateId)
        {
            return Ok(await _appService.GetCities(stateId));
        }
    }
}
