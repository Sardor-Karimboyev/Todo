using System.Threading.Tasks;
using ToDo.Common.Types;

namespace ToDo.Common.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
