using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.API.Commands;
using Todo.API.Repositories;
using ToDo.Common.Kafka;
using ToDo.Common.Types;

namespace Todo.Services.Todo.Services
{
    public class ProcessTodosService : BackgroundService
    {
        private readonly ConsumerConfig consumerConfig;
        private readonly ProducerConfig producerConfig;
        private readonly ITodoRepository _todoRepository;
        public ProcessTodosService(ConsumerConfig consumerConfig, 
            ProducerConfig producerConfig, 
            ITodoRepository todoRepository)
        {
            this.producerConfig = producerConfig;
            this.consumerConfig = consumerConfig;
            _todoRepository = todoRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumerHelper = new ConsumerWrapper(consumerConfig, "jsontest");
                string todoRequest = consumerHelper.ReadMessage();

                //Deserilaize 
                CreateTodo todo = JsonConvert.DeserializeObject<CreateTodo>(todoRequest);

                //TODO:: 
                //E imzo DSV server ga yuborib ma'lumotlarni processing qilib, 
                //keyin MongoDB ga yozib qo'yadi.

                if (await _todoRepository.ExistsAsync(todo.Title))
                {
                    throw new TodoException("todo_already_exists",
                        $"Todo: '{todo.Title}' already exists.");
                }

                var lastTodo = await _todoRepository.GetMaxOrderedTodoAsync();
                int order = 1;
                if (lastTodo != null)
                    order = ++lastTodo.Order;

                await _todoRepository.AddAsync(new API.Models.Entities.Todo { Title = todo.Title, UserId = todo.UserId, DueAt = todo.DueAt, CreatedDate = DateTime.UtcNow, Order = order });

                var producerWrapper = new ProducerWrapper(producerConfig, "todoTopic");
                await producerWrapper.WriteMessage(JsonConvert.SerializeObject(todo));
            }
        }
    }
}
