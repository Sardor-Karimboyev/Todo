using System;
using System.ComponentModel.DataAnnotations;
using ToDo.Common.Types;

namespace Todo.API.Commands
{
    public class CreateTodo : ICommand
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTimeOffset? DueAt { get; set; }
    }
}
