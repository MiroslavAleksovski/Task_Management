namespace DataAccess
{
    internal static class SQLQueriesConstants
    {
        public static readonly string GetTasksQuery = @"SELECT Id AS TaskId, [Name] AS TaskName FROM Tasks";
        public static readonly string GetTaskQuery = @"SELECT TOP 1 Id AS TaskId, 
                                                              [Name] AS TaskName,
                                                              Description AS TaskDescription
                                                              FROM Tasks
                                                              WHERE Id = @Id";

        public static readonly string InsertTaskQuery = @"INSERT INTO Tasks (Id, [Name], Description)
                                                          VALUES (@Id, @Name, @Description)";

        public static readonly string UpdateTaskQuery = @"UPDATE Tasks
                                                          SET [Name] = @Name,
                                                              Description = @Description
                                                          WHERE Id = @Id";
        public static readonly string DeleteTaskQuery = @"DELETE FROM Tasks WHERE Id = @Id";
    }
}
