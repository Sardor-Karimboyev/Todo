using System;
using Todo.API.Models.ViewModels;
using ToDo.Common.Types;

namespace Todo.API.Queries
{
    public class GetTodo : IQuery<TodoViewModel>
    {
        public Guid Id { get; set; }
    }
}
