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

        public async Task Add(UserModel user, CancellationToken cancelToken)
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(_connectionString))
                {
                    var query = "INSERT INTO  User" +
                        "(Id, Name, Email, Birth, Gender, isEnabled, Created)" +
                        "VALUES (@Id,@Name, @Email,@Birth,@Gender,@isEnabled,@Created);";
                    await connection.ExecuteAsync(query, new
                    {
                        user.Id,
                        user.Name,
                        user.Email,
                        user.Birth,
                        user.Gender,
                        user.IsEnabled,
                        Created = DateTime.Now
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to run query or to connect to SQL Database", ex);
            }
        }

        public async Task Delete(Guid id, CancellationToken cancelToken)
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
                throw new Exception("Failed to run query or to connect to SQL Database", ex);
            }
        }

        public async Task Edit(UserModel user, CancellationToken cancelToken)
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(_connectionString))
                {
                    var query = "UPDATE User " +
                        "SET Name=@Name, Email=@Email, Birth=@Birth, Gender=@Gender , isEnabled=@isEnabled " +
                        "WHERE Id = @Id";

                    await connection.ExecuteAsync(query, new
                    {
                        user.Id,
                        user.Name,
                        user.Email,
                        user.Birth,
                        user.Gender,
                        user.IsEnabled,
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to run query or to connect to SQL Database", ex);
            }
        }
    }
}