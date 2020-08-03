using System;
using System.ComponentModel.DataAnnotations;
using ToDo.Common.Types;

namespace Todo.API.Commands
{
    public class ChangeIsDoneTodo : ICommand
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public bool IsDone { get; set; }
    }
}
