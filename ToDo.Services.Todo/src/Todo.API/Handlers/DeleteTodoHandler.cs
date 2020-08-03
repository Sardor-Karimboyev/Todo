using System.Threading.Tasks;
using Todo.API.Commands;
using Todo.API.Repositories;
using ToDo.Common.Handlers;
using ToDo.Common.Types;

namespace Todo.API.Handlers
{
    public class DeleteTodoHandler : ICommandHandler<DeleteTodo>
    {
        private readonly ITodoRepository _todosRepository;

        public DeleteTodoHandler(ITodoRepository todosRepository) => _todosRepository = todosRepository;

        public async Task HandleAsync(DeleteTodo command)
        {
            if (!await _todosRepository.ExistsAsync(command.Id))
            {
                throw new TodoException("todo_not_found",
                    $"Todo with id: '{command.Id}' was not found.");
            }

            await _todosRepository.DeleteAsync(command.Id);
        }
    }
}
