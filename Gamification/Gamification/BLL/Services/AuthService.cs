using AutoMapper;
using Gamification.BLL.DTO;
using Gamification.BLL.Services.Interfaces;
using Gamification.DAL.Repository.UnitOfWork;
using Gamification.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Gamification.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }

        private IOptions<AuthOptions> _authOptions;

        public IUserService _userService { get; set; }

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AuthOptions> authOptions, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authOptions = authOptions;
            _userService = userService;
        }

        public async Task<AuthenticationUserDTO> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            User user = await AuthenticateUserAsync(userName, password, cancellationToken);
            if (user == null)
            {
                return null;
            }

            var token = GenerateJWT(user);

            var authenticationUser = _mapper.Map<AuthenticationUserDTO>(user);

            authenticationUser.IsAuthenticated = true;
            authenticationUser.Token = token;

            if (user.JwtRefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = user.JwtRefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                authenticationUser.RefreshToken = activeRefreshToken.RefreshToken;
                authenticationUser.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = CreateRefreshToken();
                authenticationUser.RefreshToken = refreshToken.RefreshToken;
                authenticationUser.RefreshTokenExpiration = refreshToken.Expires;
                user.JwtRefreshTokens.Add(refreshToken);
                await _unitOfWork.userRepository.UpdateUserAsync(user.Id, user, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return authenticationUser;
        }

        public async Task<User> AuthenticateUserAsync(string userName, string password, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.userRepository.AuthenticateUserAsync(userName, password, cancellationToken);

            return user;
        }

        private string GenerateJWT(User user)
        {
            var securityKey = AuthOptions.GetSymmetricSecurityKey();
            var credentialist = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                }
            }

            var token = new JwtSecurityToken(
                AuthOptions.Issuer,
                AuthOptions.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(AuthOptions.TokenLifeTime),
                signingCredentials: credentialist);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtRefreshToken CreateRefreshToken()
        {
            return new JwtRefreshToken
            {
                RefreshToken = Guid.NewGuid(),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
            };
        }

        public async Task<AuthenticationUserDTO> RefreshTokenAsync(Guid tokenId, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.userRepository.GetUserByRefreshTokenAsync(tokenId, cancellationToken);
            var authenticationUser = _mapper.Map<AuthenticationUserDTO>(user);

            if (user == null)
            {
                authenticationUser.IsAuthenticated = false;
                return authenticationUser;
            }

            var refreshToken = user.JwtRefreshTokens.Single(x => x.RefreshToken == tokenId);

            if (!refreshToken.IsActive)
            {
                authenticationUser.IsAuthenticated = false;
                return authenticationUser;
            }

            //Revoke Current Refresh Token
            refreshToken.Revoked = DateTime.UtcNow;

            //Generate new Refresh Token and save to Database
            var newRefreshToken = CreateRefreshToken();
            user.JwtRefreshTokens.Add(newRefreshToken);
            await _unitOfWork.userRepository.UpdateUserAsync(user.Id, user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //Generates new jwt
            var token = GenerateJWT(user);
            authenticationUser.IsAuthenticated = true;
            authenticationUser.Token = token;
            authenticationUser.RefreshToken = newRefreshToken.RefreshToken;
            authenticationUser.RefreshTokenExpiration = newRefreshToken.Expires;

            return authenticationUser;
        }

    }
}
