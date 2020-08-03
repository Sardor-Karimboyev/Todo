using System;
using System.ComponentModel.DataAnnotations;
using ToDo.Common.Types;

namespace Todo.API.Commands
{
    public class ChangeOrderTodo : ICommand
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int NewOrder { get; set; }
    }
}
