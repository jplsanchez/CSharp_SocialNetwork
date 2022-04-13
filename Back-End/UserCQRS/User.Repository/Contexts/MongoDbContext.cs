using MongoDB.Driver;
using User.Domain;
using User.Domain.Models;

namespace User.Repository.Contexts
{
    public class MongoDbContext : IDisposable
    {
        public IMongoDatabase Database { get; set; }
        private IMongoClient? _client { get; set; }

        public MongoDbContext(Settings settings)
        {
            try
            {
                MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(settings.MongoDb.ConnectionString));
                if (settings.MongoDb.IsSSL)
                {
                    mongoSettings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                _client = new MongoClient(mongoSettings);
                Database = _client.GetDatabase(settings.MongoDb.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to connect to MongoDB server", ex);
            }
        }

        public IMongoCollection<UserModel> Users
        {
            get
            {
                return Database.GetCollection<UserModel>("Users");
            }
        }

        public void Dispose()
        {
            //Database = null;
            _client = null;
            GC.SuppressFinalize(this);
        }
    }
}