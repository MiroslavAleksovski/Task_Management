namespace Models.TaskDTOModels
{
    public class TaskGridDTOModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
