namespace PhungDKH.Identity.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PhungDKH.Identity.Api.Domain;
    using PhungDKH.Identity.Api.Models;
    using PhungDKH.Identity.Api.Services;

    [Route("api/account")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService<ApplicationUser> _identityService;

        public AccountController(IIdentityService<ApplicationUser> identityService)
        {
            _identityService = identityService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            var result = await _identityService.LoginAsync(model);

            if (result != null)
            {
                return result;
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var result = await _identityService.RegisterAsync(model);

            if (result != null)
            {
                return result;
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }
    }
}
