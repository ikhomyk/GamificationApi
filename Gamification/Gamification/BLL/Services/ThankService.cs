using AutoMapper;
using Gamification.BLL.DTO;
using Gamification.DAL.Repositories;
using Gamification.DAL.Repository.UnitOfWork;
using Gamification.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.BLL.Services.Interfaces
{
    public class ThankService : IThankService
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }

        public ThankService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<CreateThankDTO> SayThankAsync(Guid currentUserId, CreateThankDTO newThank, CancellationToken cancellationToken)
        {
            Thank mapData = _mapper.Map<Thank>(newThank);
            User currentUser = await _unitOfWork.userRepository.GetCurrentUserAsync(currentUserId, cancellationToken);

            Thank thank = await _unitOfWork.thankRepository.SayThankAsync(currentUser, mapData, cancellationToken);

            return _mapper.Map<CreateThankDTO>(thank);
        }

        public async Task<GetThankDTO> GetLastThankAsync(Guid userId, CancellationToken cancellationToken)
        {

            Thank thank = await _unitOfWork.thankRepository.GetLastThankAsync(userId, cancellationToken);

            return _mapper.Map<GetThankDTO>(thank);
        }
    }
}
