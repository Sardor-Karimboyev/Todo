using System;
using ToDo.Common.Types;

namespace Todo.API.Commands
{
    public class DeleteTodo : ICommand
    {
        public Guid Id { get; }
        public DeleteTodo(Guid id)
        {
            Id = id;
        }
    }
}
