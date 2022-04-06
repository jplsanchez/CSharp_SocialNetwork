using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using User.Domain;
using User.Domain.Interfaces.Repositories;
using User.Domain.Models;

namespace User.Repository.Repositories
{
    public class WriteRepository : IWriteRepository<UserModel>
    {
        private readonly string? _connectionString;

        public WriteRepository(Settings settings)
        {
            _connectionString = settings.MySql.ConnectionString;
        }

        public async Task Add(UserModel item)
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(_connectionString))
                {
                    var query = "INSERT INTO  User" +
                        "(Id, Name, Age, Gender, isEnabled, Created)" +
                        "VALUES (@Id,@Name,@Age,@Gender,@isEnabled,@Created);";

                    await connection.ExecuteAsync(query, new
                    {
                        item.Id,
                        item.Name,
                        item.Age,
                        item.Gender,
                        item.IsEnabled,
                        Created = DateTime.Now
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao se conectar ao banco SQL", ex);
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(_connectionString))
                {
                    var query = "DELETE FROM User WHERE Id = @Id";

                    await connection.ExecuteAsync(query, new { id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao se conectar ao banco SQL", ex);
            }
        }

        public async Task Edit(UserModel item)
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(_connectionString))
                {
                    var query = "UPDATE User " +
                        "SET Name=@Name, Age=@Age, Gender=@Gender , isEnabled=@isEnabled " +
                        "WHERE Id = @Id";

                    await connection.ExecuteAsync(query, new
                    {
                        item.Id,
                        item.Name,
                        item.Age,
                        item.Gender,
                        item.IsEnabled,
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao se conectar ao banco SQL", ex);
            }
        }
    }
}