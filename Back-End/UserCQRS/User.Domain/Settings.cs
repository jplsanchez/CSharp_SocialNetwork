using Microsoft.Extensions.Configuration;

namespace User.Domain
{
    public record Settings
    {
        public MongoDbSettings MongoDb { get; init; }
        public MySqlSettings MySql { get; init; }

        public Settings(IConfiguration configuration)
        {
            MongoDb = new()
            {
                ConnectionString = configuration.GetSection("MongoConnection:ConnectionString").Value,
                DatabaseName = configuration.GetSection("MongoConnection:Database").Value,
                IsSSL = Convert.ToBoolean(configuration.GetSection("MongoConnection:IsSSL").Value),
            };

            MySql = new()
            {
                ConnectionString = configuration.GetSection("MySqlConnection:ConnectionString").Value,
            };
        }
    }

    public record MongoDbSettings
    {
        public string? ConnectionString { get; init; }
        public string? DatabaseName { get; init; }
        public bool IsSSL { get; init; }
    }

    public record MySqlSettings
    {
        public string? ConnectionString { get; init; }
    }
}