using MongoDB.Driver;
using User.Domain.Interfaces.Sync;
using User.Domain.Models.Base;
using User.Repository.Contexts;

namespace User.Repository.SyncComponents
{
    public class DatabasesSyncRepo<T> : IDatabasesSyncRepo<T> where T : BaseModel
    {
        private readonly MongoDbContext _mongoDbContext;
        private readonly string _collection;

        public DatabasesSyncRepo(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _collection = Utils.GetModelShortName(typeof(T).Name);
        }

        public async Task Add(T item, CancellationToken cancelToken)
        {
            var collection = _mongoDbContext.Database.GetCollection<T>(_collection);

            await collection.InsertOneAsync(item);
        }

        public async Task Delete(Guid id, CancellationToken cancelToken)
        {
            var collection = _mongoDbContext.Database.GetCollection<T>(_collection);

            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq(x => x.Id, id);

            await collection.DeleteOneAsync(filter);
        }

        public async Task Edit(T item, CancellationToken cancelToken)
        {
            var collection = _mongoDbContext.Database.GetCollection<T>(_collection);
            await collection.ReplaceOneAsync(x => x.Id == item.Id, item);
        }
    }
}