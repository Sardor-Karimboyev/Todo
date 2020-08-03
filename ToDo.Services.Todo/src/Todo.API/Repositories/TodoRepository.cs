using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Todo.API.Queries;
using ToDo.Common.Mongo;
using ToDo.Common.Types;

namespace Todo.API.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IMongoRepository<Models.Entities.Todo> _repository;

        public TodoRepository(IMongoRepository<Models.Entities.Todo> repository)
        {
            _repository = repository;
        }

        public async Task<Models.Entities.Todo> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<bool> ExistsAsync(Guid id)
            => await _repository.ExistsAsync(p => p.Id == id);

        public async Task<bool> ExistsAsync(string name)
            => await _repository.ExistsAsync(p => p.Title == name.ToLowerInvariant());

        public async Task<PagedResult<Models.Entities.Todo>> BrowseAsync(BrowseTodos query)
            => await _repository.BrowseAsync(p =>
                p.DueAt >= query.DueAtFrom && p.DueAt <= query.DueAtTo && p.UserId == query.UserId, query);
        
        public async Task<IEnumerable<Models.Entities.Todo>> FindAsync(Expression<Func<Models.Entities.Todo, bool>> predicate)
            => await _repository.FindAsync(predicate);
        
        public async Task<Models.Entities.Todo> GetMaxOrderedTodoAsync()
            => await _repository.GetMaxByFieldAsync<Models.Entities.Todo>(t => t.Order);

        public async Task AddAsync(Models.Entities.Todo todo)
            => await _repository.CreateAsync(todo);

        public async Task UpdateAsync(Models.Entities.Todo todo)
            => await _repository.UpdateAsync(todo);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
