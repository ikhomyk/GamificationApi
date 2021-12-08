using Gamification.BLL.DTO;
using Gamification.BLL.Services.Interfaces;
using Gamification.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuthController(IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationUserDTO>> AuthenticateAsync([FromBody] Login login, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            try
            {
                AuthenticationUserDTO userNewToken = await _authService.AuthenticateAsync(login.UserName, login.Password, cancellationToken);
                if (userNewToken == null)
                {
                    return Unauthorized();
                }
                else
                {
                    SetRefreshTokenInCookie(userNewToken.RefreshToken.ToString(), userNewToken.Token);
                    return Ok(userNewToken);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult<AuthenticationUserDTO>> RefreshTokenAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            try
            {
                var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
                var response = await _authService.RefreshTokenAsync(Guid.Parse(refreshToken), cancellationToken);
                if (!string.IsNullOrEmpty(response.RefreshToken.ToString()))
                {
                    SetRefreshTokenInCookie(response.RefreshToken.ToString(), response.Token);

                }
                return Ok(response);
            }
            catch
            {
                return StatusCode(500);
            }

        }


        private void SetRefreshTokenInCookie(string refreshToken, string accessToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
            Response.Cookies.Append("accessToken", accessToken, cookieOptions);
        }
    }
}
