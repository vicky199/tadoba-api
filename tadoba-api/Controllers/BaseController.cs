using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using tadoba_api.Models;

namespace tadoba_api.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        protected async Task<long> GetUserId()
        {
            return await Task.Run(() =>
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    string userId = identity.FindFirst("UserId").Value;
                    return long.Parse(userId);
                }
                return 0;
            });
        }
    }
}
