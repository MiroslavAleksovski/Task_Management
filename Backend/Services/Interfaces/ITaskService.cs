using Models.TaskDomainModels;
using Models.TaskDTOModels;

namespace Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskGridDTOModel>> GetTasks();
        Task<TaskDetailsDTOModel> GetTask(Guid taskId);

        Task<Guid> AddUpdateTask(TaskAddUpdateDTOModel taskDto);
        Task DeleteTask(Guid taskId);
    }
}
