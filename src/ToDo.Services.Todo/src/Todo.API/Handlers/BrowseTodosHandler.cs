using System.Linq;
using System.Threading.Tasks;
using Todo.API.Models.ViewModels;
using Todo.API.Queries;
using Todo.API.Repositories;
using ToDo.Common.Handlers;
using ToDo.Common.Types;

namespace Todo.API.Handlers
{
    public class BrowseTodosHandler : IQueryHandler<BrowseTodos, PagedResult<TodoViewModel>>
    {
        private readonly ITodoRepository _todoRepository;

        public BrowseTodosHandler(ITodoRepository todoRepository)
            => _todoRepository = todoRepository;

        public async Task<PagedResult<TodoViewModel>> HandleAsync(BrowseTodos query)
        {
            var pagedResult = await _todoRepository.BrowseAsync(query);
            var todos = pagedResult.Items.Select(p => new TodoViewModel
            {
                Id = p.Id,
                Title = p.Title,
                IsDone = p.IsDone,
                DueAt = p.DueAt,
                Order = p.Order
            }).ToList().OrderBy(t => t.Order);

            return PagedResult<TodoViewModel>.From(pagedResult, todos);
        }
    }
}
