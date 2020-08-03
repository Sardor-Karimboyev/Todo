using System.Linq;
using System.Threading.Tasks;
using Todo.API.Commands;
using Todo.API.Repositories;
using ToDo.Common.Handlers;
using ToDo.Common.Types;

namespace Todo.API.Handlers
{
    public class ChangeOrderTodoHandler : ICommandHandler<ChangeOrderTodo>
    {
        private readonly ITodoRepository _todoRepository;

        public ChangeOrderTodoHandler(ITodoRepository todoRepository)
            => _todoRepository = todoRepository;

        public async Task HandleAsync(ChangeOrderTodo command)
        {
            var todo = await _todoRepository.GetAsync(command.Id);
            if (todo == null)
            {
                throw new TodoException("todo_not_found",
                    $"Todo with id: '{command.Id}' was not found.");
            }
            int oldOrder = todo.Order;
            if(oldOrder > command.NewOrder)
            {
                var todos = await _todoRepository.FindAsync(p => p.Order >= command.NewOrder && p.Order < oldOrder);
                foreach(var td in todos)
                {
                    td.Order++;
                    await _todoRepository.UpdateAsync(td);
                }
                todo.Order = command.NewOrder;
                await _todoRepository.UpdateAsync(todo);
            }
            else
            {
                var todos = await _todoRepository.FindAsync(p => p.Order <= command.NewOrder && p.Order > oldOrder);
                foreach (var td in todos.OrderBy(t => t.Order))
                {
                    td.Order--;
                    await _todoRepository.UpdateAsync(td);
                }
                todo.Order = command.NewOrder;
                await _todoRepository.UpdateAsync(todo);
            }
        }
    }
}
