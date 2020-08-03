using System;
using System.Threading.Tasks;
using Todo.API.Commands;
using Todo.API.Repositories;
using ToDo.Common.Handlers;
using ToDo.Common.Types;

namespace Todo.API.Handlers
{
    public class UpdateTodoHandler : ICommandHandler<UpdateTodo>
    {
        private readonly ITodoRepository _todoRepository;

        public UpdateTodoHandler(ITodoRepository todoRepository) 
            => _todoRepository = todoRepository;

        public async Task HandleAsync(UpdateTodo command)
        {
            var todo = await _todoRepository.GetAsync(command.Id);
            if (todo == null)
            {
                throw new TodoException("todo_not_found",
                    $"Todo with id: '{command.Id}' was not found.");
            }

            await _todoRepository.UpdateAsync(new Models.Entities.Todo { Title = command.Title, IsDone = command.IsDone, DueAt = command.DueAt, Id = command.Id, UserId = todo.UserId , UpdatedDate = DateTime.UtcNow});
        }
    }
}
