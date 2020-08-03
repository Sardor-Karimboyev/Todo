using System.Threading.Tasks;

namespace ToDo.Common
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
