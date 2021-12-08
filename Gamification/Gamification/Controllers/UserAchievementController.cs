using Gamification.BLL.DTO;
using Gamification.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAchievementController : ControllerBase
    {
        private IAchievementService _achievementService { get; set; }
        public UserAchievementController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<AchievementDTO>>> UpdateUserAchievementsAsync(Guid userId, Guid achievementId, CancellationToken cancellationToken)
        {
            try
            {
                var achievements = await _achievementService
                    .UpdateUserAchievementsAsync(userId, achievementId, cancellationToken);

                return Ok(achievements);
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
