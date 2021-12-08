using Gamification.BLL.DTO;
using Gamification.BLL.Services.Interfaces;
using Gamification.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.Controllers
{
    [Route("api/request-achievement")]
    [ApiController]
    [Authorize]
    public class RequestAchievementController : ControllerBase
    {
        private IRequestAchievementService _requestAchievementService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public RequestAchievementController(IRequestAchievementService requestAchievementService, IHttpContextAccessor httpContextAccessor)
        {
            _requestAchievementService = requestAchievementService;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpPost]
        public async Task<ActionResult<RequestAchievementDTO>> AddRequest(Guid achievementId, string message, CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                RequestAchievement newRequest = new RequestAchievement
                {
                    AchievementId = achievementId,
                    UserId = userId,
                    Message = message
                };

                RequestAchievementDTO createdRequest = await _requestAchievementService.AddRequest(newRequest, cancellationToken);

                return Ok(createdRequest);

            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
