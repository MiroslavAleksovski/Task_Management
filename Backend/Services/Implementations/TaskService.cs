using Models.TaskDTOModels;
using Services.Interfaces;
using AccessLevel.Interfaces;

namespace Services.Implementations
{
    public class TaskService(ITaskRepository taskRepository) : ITaskService
    {
        public async Task<IEnumerable<TaskGridDTOModel>> GetTasks()
        {
            var domainItems = await taskRepository.GetTasksAsync();

            // Map domain models to DTO grid models (simple mapping here)
            var dtoItems = new List<TaskGridDTOModel>();

            foreach (var d in domainItems)
            {
                dtoItems.Add(new TaskGridDTOModel { Id = d.Id, Name = d.Name });
            }

            return dtoItems;
        }
    }
}
