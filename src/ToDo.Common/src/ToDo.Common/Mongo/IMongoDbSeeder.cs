using System.Threading.Tasks;

namespace ToDo.Common.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}
