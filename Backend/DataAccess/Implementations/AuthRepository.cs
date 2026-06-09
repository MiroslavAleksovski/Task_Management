using Dapper;
using DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not found in configuration.");
            }
            _connectionString = connectionString;
        }

        public async Task<bool> UserExists(string email)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("Email", email, DbType.String);

            var count = await db.ExecuteScalarAsync<int>(SQLQueriesConstants.CheckUserExistsQuery, parameters);
            return count > 0;
        }

        public async Task<bool> CreateUser(Guid userId, string name, string surname, string email, string passwordHash)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("UserId", userId, DbType.Guid);
            parameters.Add("Name", name, DbType.String);
            parameters.Add("Surname", surname, DbType.String);
            parameters.Add("Email", email, DbType.String);
            parameters.Add("PasswordHash", passwordHash, DbType.String);

            var rows = await db.ExecuteAsync(SQLQueriesConstants.InsertNewUserQuery, parameters);
            return rows > 0;
        }

        public async Task<(string Email, string PasswordHash)?> GetUserByEmail(string email)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("Email", email, DbType.String);

            var user = await db.QueryFirstOrDefaultAsync<(string Email, string PasswordHash)>(
                @"SELECT Email, PasswordHash FROM Users WHERE Email = @Email",
                parameters);

            return user;
        }
    }
}
