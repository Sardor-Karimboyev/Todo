using System.Threading.Tasks;
using Todo.API.Commands;
using Todo.API.Models.ViewModels;
using Todo.API.Queries;
using Todo.API.Repositories;
using ToDo.Common.Handlers;

namespace Todo.API.Handlers
{
    public class GetTodoHandler : IQueryHandler<GetTodo, TodoViewModel>
    {
        private readonly ITodoRepository _todoRepository;

        public GetTodoHandler(ITodoRepository todoRepository)
            => _todoRepository = todoRepository;

        public async Task<TodoViewModel> HandleAsync(GetTodo query)
        {
            var todo = await _todoRepository.GetAsync(query.Id);

            return todo == null ? null : new TodoViewModel
            {
                Id = todo.Id,
                DueAt = todo.DueAt,
                Title = todo.Title,
                IsDone = todo.IsDone,
            };
        }
    }
}
