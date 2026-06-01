namespace Models.TaskDTOModels
{
    public class TaskFilterModel
    {
        public TaskFilterModel()
        {
            IsCompleted = false;
        }   

        public bool IsCompleted { get; set; }
    }
}
