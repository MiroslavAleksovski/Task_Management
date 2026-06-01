namespace DataAccess
{
    internal static class SQLQueriesConstants
    {
        public static readonly string GetTasksQuery = @"SELECT Id AS TaskId, [Name] AS TaskName, IsCompleted FROM Tasks";
        public static readonly string GetTaskQuery = @"SELECT TOP 1 Id AS TaskId, 
                                                              [Name] AS TaskName,
                                                              Description AS TaskDescription,
                                                              IsCompleted
                                                              FROM Tasks
                                                              WHERE Id = @Id";

        public static readonly string InsertTaskQuery = @"INSERT INTO Tasks (Id, [Name], Description, IsCompleted)
                                                          VALUES (@Id, @Name, @Description, @IsCompleted)";

        public static readonly string UpdateTaskQuery = @"UPDATE Tasks
                                                          SET [Name] = @Name,
                                                              Description = @Description,
                                                              IsCompleted = @IsCompleted
                                                          WHERE Id = @Id";
        public static readonly string DeleteTaskQuery = @"DELETE FROM Tasks WHERE Id = @Id";
    }
}
