using Models.TaskDomainModels;
using Models.TaskDTOModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessLevel.Interfaces
{
    public interface ITaskRepository    
    {
        Task<IEnumerable<TaskGridDomainModel>> GetTasks(TaskFilterModel? filter = null);
        Task<TaskDetailsDomainModel?> GetTask(Guid taskId);
        Task<Guid> AddTask(TaskDetailsDomainModel task);
        Task<Guid> UpdateTask(TaskDetailsDomainModel task);
        Task<bool> DeleteTask(Guid taskId);   
    }
}
