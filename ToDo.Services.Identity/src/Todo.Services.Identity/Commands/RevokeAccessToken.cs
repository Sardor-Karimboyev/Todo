using System;
using ToDo.Common.Types;

namespace Todo.Services.Identity.Commands
{
    public class RevokeAccessToken : ICommand
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}