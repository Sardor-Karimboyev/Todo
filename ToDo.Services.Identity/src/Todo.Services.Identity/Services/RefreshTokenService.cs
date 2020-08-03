using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Services.Identity.Models.Entities;
using Todo.Services.Identity.Reposotories;
using ToDo.Common.Authentication;
using ToDo.Common.Types;

namespace Todo.Services.Identity.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IClaimsProvider _claimsProvider;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IJwtHandler jwtHandler,
            IPasswordHasher<User> passwordHasher,
            IClaimsProvider claimsProvider)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _passwordHasher = passwordHasher;
            _claimsProvider = claimsProvider;
        }

        public async Task AddAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new TodoException(Codes.UserNotFound,
                    $"User: '{userId}' was not found.");
            }
            await _refreshTokenRepository.AddAsync(new RefreshToken(user, _passwordHasher));
        }

        public async Task<JsonWebToken> CreateAccessTokenAsync(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(token);
            if (refreshToken == null)
            {
                throw new TodoException(Codes.RefreshTokenNotFound,
                    "Refresh token was not found.");
            }
            if (refreshToken.Revoked)
            {
                throw new TodoException(Codes.RefreshTokenAlreadyRevoked,
                    $"Refresh token: '{refreshToken.Id}' was revoked.");
            }
            var user = await _userRepository.GetAsync(refreshToken.UserId);
            if (user == null)
            {
                throw new TodoException(Codes.UserNotFound,
                    $"User: '{refreshToken.UserId}' was not found.");
            }
            var claims = await _claimsProvider.GetAsync(user.Id);
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
            jwt.RefreshToken = refreshToken.Token;

            return jwt;
        }

        public async Task RevokeAsync(string token, Guid userId)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(token);
            if (refreshToken == null || refreshToken.UserId != userId)
            {
                throw new TodoException(Codes.RefreshTokenNotFound,
                    "Refresh token was not found.");
            }
            refreshToken.Revoke();
            await _refreshTokenRepository.UpdateAsync(refreshToken);
        }
    }
}
