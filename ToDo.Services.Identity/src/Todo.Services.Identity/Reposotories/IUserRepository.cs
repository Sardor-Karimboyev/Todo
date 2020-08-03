using System;
using System.Threading.Tasks;
using Todo.Services.Identity.Models.Entities;

namespace Todo.Services.Identity.Reposotories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}