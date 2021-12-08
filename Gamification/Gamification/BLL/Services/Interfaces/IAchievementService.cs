using Gamification.BLL.DTO;
using Gamification.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.BLL.Services.Interfaces
{
    public interface IAchievementService
    {
        public Task<IEnumerable<AchievementDTO>> GetAllAchievementsAsync(CancellationToken cancellationToken);
        public Task<AchievementDTO> GetAchievementByIdAsync(Guid Id, CancellationToken cancellationToken);
        public Task<Achievement> CreateAchievementAsync(AchievementDTO newAchievement, CancellationToken cancellationToken);
        public Task<Achievement> UpdateAchievementAsync(Guid achievementId, AchievementDTO newAchievement, CancellationToken cancellationToken);
        public Task<Achievement> DeleteAchievementAsync(Guid achievemenId, CancellationToken cancellationToken);
        public Task<IEnumerable<AchievementDTO>> GetAllUserAchievementsAsync(Guid userId, CancellationToken cancellationToken);
        public Task<IEnumerable<AchievementDTO>> UpdateUserAchievementsAsync(Guid userId, Guid achievementId, CancellationToken cancellationToken);
        public Task<IEnumerable<AchievementDTO>> GetLastUserAchievementsAsync(Guid userId, CancellationToken cancellationToken);

    }
}
