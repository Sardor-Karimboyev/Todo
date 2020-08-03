using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Todo.API.Queries;
using ToDo.Common.Types;

namespace Todo.API.Repositories
{
    public interface ITodoRepository
    {
        Task<Models.Entities.Todo> GetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<IEnumerable<Models.Entities.Todo>> FindAsync(Expression<Func<Models.Entities.Todo, bool>> predicate);
        Task<Models.Entities.Todo> GetMaxOrderedTodoAsync();
        Task<PagedResult<Models.Entities.Todo>> BrowseAsync(BrowseTodos query);
        Task AddAsync(Models.Entities.Todo todo);
        Task UpdateAsync(Models.Entities.Todo todo);
        Task DeleteAsync(Guid id);
    }
}
