using Models.TaskDomainModels;
using Models.TaskDTOModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskGridDTOModel>> GetTasks();
    }
}
