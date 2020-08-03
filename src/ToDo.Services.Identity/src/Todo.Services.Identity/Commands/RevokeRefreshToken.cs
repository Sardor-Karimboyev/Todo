using System;

namespace Todo.Services.Identity.Commands
{
    public class RevokeRefreshToken
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
