using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Todo.API.Commands;
using Todo.API.Repositories;
using ToDo.Common.Handlers;
using ToDo.Common.Kafka;
using ToDo.Common.Types;

namespace Todo.API.Handlers
{
    public class CreateTodoHandler : ICommandHandler<CreateTodo>
    {
        private readonly ITodoRepository _todoRepository;

        public CreateTodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task HandleAsync(CreateTodo command)
        {
            string serializedTodo = JsonConvert.SerializeObject(command);

            var producer = new ProducerWrapper(command.Config, "jsontest");
            await producer.WriteMessage(serializedTodo);

            //if (await _todoRepository.ExistsAsync(command.Title))
            //{
            //    throw new TodoException("todo_already_exists",
            //        $"Todo: '{command.Title}' already exists.");
            //}

            //var todo = await _todoRepository.GetMaxOrderedTodoAsync();
            //int order = 1;
            //if (todo != null)
            //    order = ++todo.Order;

            //await _todoRepository.AddAsync(new Models.Entities.Todo { Title = command.Title, UserId = command.UserId, DueAt = command.DueAt, CreatedDate = DateTime.UtcNow, Order = order });
        }
    }
}
