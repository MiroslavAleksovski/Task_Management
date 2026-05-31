namespace Models.TaskDTOModels
{
    public class TaskDetailsDTOModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
