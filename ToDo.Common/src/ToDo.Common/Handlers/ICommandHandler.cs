using System.Threading.Tasks;
using ToDo.Common.Types;

namespace ToDo.Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
