using Gamification.DAL.IRepository;
using Gamification.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.DAL.Repository
{
    public class AchievementRepository : IAchievementRepository
    {
        private MyContext _context;

        public AchievementRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Achievement>> GetAllAchievementsAsync(CancellationToken cancellationToken)
        {
            var achievements = _context.Achievements;

            return await achievements.ToListAsync(cancellationToken);
        }

        public async Task<Achievement> GetAchievementByIdAsync(Guid achievementId, CancellationToken cancellationToken)
        {
            Achievement achievement = await _context.Achievements.FirstOrDefaultAsync(x => x.Id == achievementId, cancellationToken);

            return achievement;
        }

        public async Task<Achievement> CreateAchievementAsync(Achievement achievement, CancellationToken cancellationToken)
        {
            Guid guid = Guid.NewGuid();
            achievement.Id = guid;
            achievement.AddedTime = DateTime.Now;
            await _context.Achievements.AddAsync(achievement, cancellationToken);

            return achievement;
        }

        public async Task<Achievement> UpdateAchievementAsync(Guid achievementId, Achievement newAchievement, CancellationToken cancellationToken)
        {
            Achievement achievement = await _context.Achievements.FirstOrDefaultAsync(x => x.Id == achievementId, cancellationToken);
            if (achievement != null)
            {
                achievement.Id = achievementId;
                achievement.Name = newAchievement.Name;
                achievement.Xp = newAchievement.Xp;
                achievement.Description = newAchievement.Description;
                achievement.IconId = newAchievement.IconId;

                _context.Achievements.Update(achievement);
                await _context.SaveChangesAsync();
            }

            return achievement;
        }

        public async Task<Achievement> DeleteAchievementAsync(Guid AchievementId, CancellationToken cancellationToken)
        {
            Achievement achievement = await _context.Achievements.FirstOrDefaultAsync(x => x.Id == AchievementId, cancellationToken);
            _context.Achievements.Attach(achievement);
            _context.Achievements.Remove(achievement);

            return achievement;
        }

        public async Task<IEnumerable<Achievement>> GetAllUserAchievementsAsync(Guid userId, CancellationToken cancellationToken)
        {
            User user = await _context.Users.Include(a => a.Achievements).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user == null)
            {
                return null;
            }

            return user?.Achievements != null ? user.Achievements : null;
        }

        public async Task<IEnumerable<Achievement>> UpdateUserAchievementsAsync(Guid userId, Guid achievementId, CancellationToken cancellationToken)
        {
            User user = await _context.Users.Include(a => a.Achievements).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            Achievement achievement = await GetAchievementByIdAsync(achievementId, cancellationToken);

            achievement.AddedTime = DateTime.Now;

            user.Achievements.Add(achievement);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var achievements = user.Achievements;

            return achievements;
        }

        public async Task<IEnumerable<Achievement>> GetLastUserAchievementsAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(a => a.Achievements.OrderByDescending(d => d.AddedTime).Take(5)).FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return null;
            }

            return user?.Achievements != null ? user.Achievements : null;
        }
    }
}
