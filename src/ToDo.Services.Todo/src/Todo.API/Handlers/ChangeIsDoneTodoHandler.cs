using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.API.Commands;
using Todo.API.Repositories;
using ToDo.Common.Handlers;
using ToDo.Common.Types;

namespace Todo.API.Handlers
{
    public class ChangeIsDoneTodoHandler : ICommandHandler<ChangeIsDoneTodo>
    {
        private readonly ITodoRepository _todoRepository;

        public ChangeIsDoneTodoHandler(ITodoRepository todoRepository)
            => _todoRepository = todoRepository;

        public async Task HandleAsync(ChangeIsDoneTodo command)
        {
            var todo = await _todoRepository.GetAsync(command.Id);
            if (todo == null)
            {
                throw new TodoException("todo_not_found",
                    $"Todo with id: '{command.Id}' was not found.");
            }
            todo.IsDone = command.IsDone;
            await _todoRepository.UpdateAsync(todo);
        }
    }
}
