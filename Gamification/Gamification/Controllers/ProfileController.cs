using Gamification.BLL.DTO;
using Gamification.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private IAchievementService _achievementService { get; set; }
        private IUserService _userService { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(IAchievementService achievementService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _achievementService = achievementService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("current")]
        public async Task<ActionResult<AuthenticationUserDTO>> GetCurrentUserAsync(CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                AuthenticationUserDTO currentUser = await _userService.GetCurrentUserAsync(userId, cancellationToken);

                currentUser.Token = _httpContextAccessor.HttpContext.Request.Cookies["accessToken"];
                currentUser.RefreshToken = Guid.Parse(_httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]);

                SetRefreshTokenInCookie(currentUser.RefreshToken.ToString(), currentUser.Token);
                return Ok(currentUser);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("achievements")]
        public async Task<ActionResult<AchievementDTO>> GetAllUserAchievementsAsync(CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var achievements = await _achievementService.GetAllUserAchievementsAsync(userId, cancellationToken);

                if (achievements == null)
                {
                    return NoContent();
                }

                return Ok(achievements);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("last")]
        public async Task<ActionResult<IEnumerable<AchievementDTO>>> GetLastUserAchievementsAsync(CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var achievements = await _achievementService.GetLastUserAchievementsAsync(userId, cancellationToken);

                if (achievements == null)
                {
                    return NoContent();
                }

                return Ok(achievements);
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPut]
        [Route("change-password")]
        public async Task<ActionResult<AuthenticationUserDTO>> ChangePasswordAsync(string oldPassword, string newPassword, string confirmPassword, CancellationToken cancellationToken)
        {
            try
            {
                AuthenticationUserDTO newUser = await _userService.ChangePasswordAsync(oldPassword, newPassword, confirmPassword, cancellationToken);

                return Ok(newUser);
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
