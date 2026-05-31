namespace DataAccess
{
    internal static class SQLQueriesConstants
    {
        public static readonly string GetTasksQuery = @"SELECT Id AS TaskId, [Name] AS TaskName FROM Tasks";
    }
}
