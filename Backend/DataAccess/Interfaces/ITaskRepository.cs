using Models.TaskDomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessLevel.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskDetailsDomainModel>> GetTasksAsync();
    }
}
