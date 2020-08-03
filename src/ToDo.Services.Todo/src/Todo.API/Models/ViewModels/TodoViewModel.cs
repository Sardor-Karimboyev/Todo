using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.API.Models.ViewModels
{
    public class TodoViewModel
    {
        public Guid Id { get; set; }
        public bool IsDone { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset? DueAt { get; set; }
        public int Order { get; set; }
    }
}
