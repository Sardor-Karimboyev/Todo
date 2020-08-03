using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Common.Types;

namespace Todo.Services.Identity.Models.Entities
{
    public class RefreshToken : IIdentifiable
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public bool Revoked => RevokedAt.HasValue;

        protected RefreshToken()
        {
        }

        public RefreshToken(User user, IPasswordHasher<User> passwordHasher)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
            CreatedAt = DateTime.UtcNow;
            Token = CreateToken(user, passwordHasher);
        }

        public void Revoke()
        {
            if (Revoked)
            {
                throw new TodoException(Codes.RefreshTokenAlreadyRevoked,
                    $"Refresh token: '{Id}' was already revoked at '{RevokedAt}'.");
            }
            RevokedAt = DateTime.UtcNow;
        }

        private static string CreateToken(User user, IPasswordHasher<User> passwordHasher)
            => passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"))
                .Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);
    }
}
