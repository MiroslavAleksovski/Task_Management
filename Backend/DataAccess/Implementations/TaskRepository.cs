using AccessLevel.Interfaces;
using Dapper;
using DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models.TaskDomainModels;
using Models.TaskDTOModels;
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
        public async Task<IEnumerable<TaskGridDomainModel>> GetTasks(TaskFilterModel? filter = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            if (filter == null)
            {
                var result = await db.QueryAsync<TaskGridDomainModel>(SQLQueriesConstants.GetTasksQuery);
                return result;
            }

            // build dynamic query based on filter
            var sql = SQLQueriesConstants.GetTasksQuery + " WHERE 1=1";
            var parameters = new DynamicParameters();

            var isCompletedProp = filter.GetType().GetProperty("IsCompleted");
            if (isCompletedProp != null)
            {
                var val = isCompletedProp.GetValue(filter);
                if (val != null)
                {
                    sql += " AND IsCompleted = @IsCompleted";
                    parameters.Add("IsCompleted", val, DbType.Boolean);
                }
            }

            var filtered = await db.QueryAsync<TaskGridDomainModel>(sql, parameters);
            return filtered;
        }

        public async Task<TaskDetailsDomainModel?> GetTask(Guid taskId)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("Id", taskId, DbType.Guid, ParameterDirection.Input);

            TaskDetailsDomainModel? result = await db.QueryFirstOrDefaultAsync<TaskDetailsDomainModel>(
                SQLQueriesConstants.GetTaskQuery,
                parameters);

            return result;
        }

        public async Task<Guid> AddTask(TaskDetailsDomainModel task)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("Id", task.Id, DbType.Guid, ParameterDirection.Input);
            parameters.Add("Name", task.Name, DbType.String, ParameterDirection.Input, size: 50);
            parameters.Add("Description", task.Description, DbType.String, ParameterDirection.Input, size: -1);
            parameters.Add("IsCompleted", task.IsCompleted, DbType.Boolean, ParameterDirection.Input);

            await db.ExecuteAsync(SQLQueriesConstants.InsertTaskQuery, parameters);

            return task.Id;
        }

        public async Task<Guid> UpdateTask(TaskDetailsDomainModel task)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("Id", task.Id, DbType.Guid, ParameterDirection.Input);
            parameters.Add("Name", task.Name, DbType.String, ParameterDirection.Input, size: 50);
            parameters.Add("Description", task.Description, DbType.String, ParameterDirection.Input, size: -1);
            parameters.Add("IsCompleted", task.IsCompleted, DbType.Boolean, ParameterDirection.Input);

            var rows = await db.ExecuteAsync(SQLQueriesConstants.UpdateTaskQuery, parameters);
            return task.Id;
        }

        public async Task<bool> DeleteTask(Guid taskId)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("Id", taskId, DbType.Guid, ParameterDirection.Input);

            var rows = await db.ExecuteAsync(SQLQueriesConstants.DeleteTaskQuery, parameters);
            return rows > 0;
        }
    }
}
