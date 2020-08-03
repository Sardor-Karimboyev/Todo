using System;
using Todo.API.Models.ViewModels;
using ToDo.Common.Types;

namespace Todo.API.Queries
{
    public class BrowseTodos : PagedQueryBase, IQuery<PagedResult<TodoViewModel>>
    {
        public Guid UserId { get; set; }
        public DateTimeOffset? DueAtFrom { get; set; } = DateTimeOffset.MinValue;
        public DateTimeOffset? DueAtTo { get; set; } = DateTimeOffset.UtcNow.AddDays(7);
    }
}
