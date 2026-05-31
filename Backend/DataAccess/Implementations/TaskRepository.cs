using AccessLevel.Interfaces;
using Dapper;
using DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models.TaskDomainModels;
using System.Data;

namespace AccessLevel.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not found in configuration.");
            }
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<TaskDetailsDomainModel>> GetTasksAsync()
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var result = await db.QueryAsync<TaskDetailsDomainModel>(SQLQueriesConstants.GetTasksQuery);

            return result;
        }
    }
}
