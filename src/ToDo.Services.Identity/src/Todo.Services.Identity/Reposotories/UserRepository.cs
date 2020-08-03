using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Services.Identity.Models.Entities;
using ToDo.Common.Mongo;

namespace Todo.Services.Identity.Reposotories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _repository;

        public UserRepository(IMongoRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<User> GetAsync(string email)
            => await _repository.GetAsync(x => x.Email == email.ToLowerInvariant());

        public async Task AddAsync(User user)
            => await _repository.CreateAsync(user);

        public async Task UpdateAsync(User user)
            => await _repository.UpdateAsync(user);
    }
}
