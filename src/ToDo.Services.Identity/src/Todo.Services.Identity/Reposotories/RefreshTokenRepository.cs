using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Services.Identity.Models.Entities;
using ToDo.Common.Mongo;

namespace Todo.Services.Identity.Reposotories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshToken> _repository;

        public RefreshTokenRepository(IMongoRepository<RefreshToken> repository)
        {
            _repository = repository;
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await _repository.GetAsync(x => x.Token == token);

        public async Task AddAsync(RefreshToken token)
            => await _repository.CreateAsync(token);

        public async Task UpdateAsync(RefreshToken token)
            => await _repository.UpdateAsync(token);
    }
}
