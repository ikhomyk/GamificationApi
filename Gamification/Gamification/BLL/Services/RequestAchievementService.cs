using AutoMapper;
using Gamification.BLL.DTO;
using Gamification.BLL.Services.Interfaces;
using Gamification.DAL.Repository.UnitOfWork;
using Gamification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.BLL.Services
{
    public class RequestAchievementService : IRequestAchievementService
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }

        public RequestAchievementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<RequestAchievementDTO> AddRequest(RequestAchievement requestAchievement, CancellationToken cancellationToken)
        {
            RequestAchievement newRequestAchievement = await _unitOfWork.requestAchievementRepository.AddRequest(requestAchievement, cancellationToken);

            RequestAchievementDTO mapRequest = _mapper.Map<RequestAchievementDTO>(newRequestAchievement);

            return mapRequest;
        }
    }
}
