using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tadoba_api.Models;
using tadoba_api.Services;

namespace tadoba_api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            return Ok(await _authService.Login(loginModel));
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            return Ok(await _authService.Register(userModel));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserModel userModel)
        {
            return Ok(await _authService.UpdateUser(userModel));
        }
        [HttpPost]
        public async Task<IActionResult> AddAddress(UserAddressModel userAddressModel)
        {
            userAddressModel.UserId = await GetUserId();
            return Ok(await _authService.AddAddress(userAddressModel));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAddress(UserAddressModel userAddressModel)
        {
            userAddressModel.UserId = await GetUserId();
            return Ok(await _authService.UpdateAddress(userAddressModel));
        }
        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            long userId = await GetUserId();
            return Ok(await _authService.GetAddresses(userId));
        }
    }
}
