namespace Models.TaskDTOModels
{
    public class TaskAddUpdateDTOModel
    {
        public Guid? Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
