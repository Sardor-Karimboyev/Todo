using System.Threading.Tasks;
using Todo.Services.Identity.Models.Entities;

namespace Todo.Services.Identity.Reposotories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}