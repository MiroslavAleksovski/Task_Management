using Models.TaskDTOModels;

namespace Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskGridDTOModel>> GetTasks(Models.TaskDTOModels.TaskFilterModel? filter = null);
        Task<TaskDetailsDTOModel> GetTask(Guid taskId);

        Task<Guid> AddUpdateTask(TaskAddUpdateDTOModel taskDto);
        Task DeleteTask(Guid taskId);
    }
}
