using Autofac;
using System.Threading.Tasks;
using ToDo.Common.Handlers;
using ToDo.Common.Types;

namespace ToDo.Common.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _context;

        public QueryDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
            => await _context.Resolve<IQueryHandler<TQuery, TResult>>().HandleAsync(query);

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _context.Resolve(handlerType);
            var typd = handler.GetType();
            return await handler.HandleAsync((dynamic)query);
        }
    }
}
