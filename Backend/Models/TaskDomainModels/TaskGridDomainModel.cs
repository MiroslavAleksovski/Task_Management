namespace Models.TaskDomainModels
{
    public class TaskGridDomainModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
