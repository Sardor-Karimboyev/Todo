using System;

namespace ToDo.Common.Authentication
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(Guid userId, string role);
        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}