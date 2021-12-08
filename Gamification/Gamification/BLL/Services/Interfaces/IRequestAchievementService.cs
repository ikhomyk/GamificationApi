using Gamification.BLL.DTO;
using Gamification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.BLL.Services.Interfaces
{
    public interface IRequestAchievementService
    {
        public Task<RequestAchievementDTO> AddRequest(RequestAchievement requestAchievement, CancellationToken cancellationToken);

    }
}
