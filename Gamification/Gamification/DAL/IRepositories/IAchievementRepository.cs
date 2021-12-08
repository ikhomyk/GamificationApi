using Gamification.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.DAL.IRepository
{
    public interface IAchievementRepository
    {
        public Task<IEnumerable<Achievement>> GetAllAchievementsAsync(CancellationToken cancellationToken);
        public Task<Achievement> GetAchievementByIdAsync(Guid AchievementId, CancellationToken cancellationToken);
        public Task<Achievement> CreateAchievementAsync(Achievement achievement, CancellationToken cancellationToken);
        public Task<Achievement> UpdateAchievementAsync(Guid achievementId, Achievement achievement, CancellationToken cancellationToken);
        public Task<Achievement> DeleteAchievementAsync(Guid AchievementId, CancellationToken cancellationToken);
        public Task<IEnumerable<Achievement>> GetAllUserAchievementsAsync(Guid userId, CancellationToken cancellationToken);
        public Task<IEnumerable<Achievement>> UpdateUserAchievementsAsync(Guid userId, Guid achievementId, CancellationToken cancellationToken);
        public Task<IEnumerable<Achievement>> GetLastUserAchievementsAsync(Guid userId, CancellationToken cancellationToken);

    }
}
