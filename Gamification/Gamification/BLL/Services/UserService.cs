using AutoMapper;
using Gamification.BLL.DTO;
using Gamification.BLL.Services.Interfaces;
using Gamification.DAL.Repository.UnitOfWork;
using Gamification.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.userRepository.GetAllUsersAsync(cancellationToken);

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.userRepository.GetUserByIdAsync(Id, cancellationToken);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<User> CreateUserAsync(CreateUserDTO newUser, CancellationToken cancellationToken)
        {
            User mapData = _mapper.Map<User>(newUser);
            User user = await _unitOfWork.userRepository.CreateUserAsync(mapData, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<UpdateUserDTO> UpdateUserAsync(Guid userId, UpdateUserDTO newUser, CancellationToken cancellationToken)
        {
            User mapData = _mapper.Map<User>(newUser);
            User user = await _unitOfWork.userRepository.UpdateUserAsync(userId, mapData, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            UpdateUserDTO updatedUser = _mapper.Map<UpdateUserDTO>(user);
            return updatedUser;
        }

        public async Task<User> DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            User deletedUser = await _unitOfWork.userRepository.DeleteUserAsync(userId, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return deletedUser;
        }

        public async Task<AuthenticationUserDTO> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.userRepository.GetCurrentUserAsync(userId, cancellationToken);

            return _mapper.Map<AuthenticationUserDTO>(user);
        }

        public async Task<AuthenticationUserDTO> GetUserByRefreshTokenAsync(Guid refreshTokenId, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.userRepository.GetUserByRefreshTokenAsync(refreshTokenId, cancellationToken);

            return _mapper.Map<AuthenticationUserDTO>(user);
        }

        public async Task<AuthenticationUserDTO> ChangePasswordAsync(string oldPassword, string newPassword, string confirmRassword, CancellationToken cancellationToken)
        {
            User newUser = await _unitOfWork.userRepository.ChangePasswordAsync(oldPassword, newPassword, confirmRassword, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (newUser != null)
            {
                AuthenticationUserDTO updatedUser = _mapper.Map<AuthenticationUserDTO>(newUser);

                return updatedUser;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserShortInfoDTO>> GetAllUsersWithLastAchievementAsync(CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.userRepository.GetAllUsersWithLastAchievementAsync(cancellationToken);

            return _mapper.Map<IEnumerable<UserShortInfoDTO>>(users);
        }
    }
}
