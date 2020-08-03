using System.Threading.Tasks;
using ToDo.Common.Types;

namespace ToDo.Common.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}
