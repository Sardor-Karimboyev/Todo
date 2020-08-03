using System;
using System.ComponentModel.DataAnnotations;
using ToDo.Common.Types;

namespace Todo.API.Commands
{
    public class UpdateTodo : ICommand
    {
        [Required]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset? DueAt { get; set; }
        public bool IsDone { get; set; }
    }
}
