using System;
using ToDo.Common.Types;

namespace Todo.API.Models.Entities
{
    public class Todo : BaseEntity
    {
        public bool IsDone { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset? DueAt { get; set; }
        public int Order { get; set; }
    }
}
