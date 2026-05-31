using Models.TaskDomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessLevel.Interfaces
{
    public interface ITaskRepository    
    {
        Task<IEnumerable<TaskGridDomainModel>> GetTasks();
        Task<TaskDetailsDomainModel?> GetTask(Guid taskId);
        Task<Guid> AddTask(TaskDetailsDomainModel task);
        Task<Guid> UpdateTask(TaskDetailsDomainModel task);
        Task<bool> DeleteTask(Guid taskId);   
    }
}
