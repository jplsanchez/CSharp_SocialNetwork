using MongoDB.Driver;
using User.Domain.Interfaces.Repositories;
using User.Domain.Models.Base;
using User.Repository.Contexts;

namespace User.Repository.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseModel
    {
        private readonly MongoDbContext _mongoDbContext;
        private readonly string _collection;

        public ReadRepository(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _collection = Utils.GetModelShortName(typeof(T).Name);
        }

        public async Task<T> Get(Guid id)
        {
            var collection = _mongoDbContext.Database.GetCollection<T>(_collection);

            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq(x => x.Id, id);
            filter &= filterBuilder.Eq(x => x.IsEnabled, true);

            var result = await collection.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var collection = _mongoDbContext.Database.GetCollection<T>(_collection);

            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq(x => x.IsEnabled, true);

            var result = await collection.FindAsync(filter);
            return result.ToList();
        }
    }
}