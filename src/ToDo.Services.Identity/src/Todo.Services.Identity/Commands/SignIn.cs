using ToDo.Common.Types;

namespace Todo.Services.Identity.Commands
{
    public class SignIn : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
