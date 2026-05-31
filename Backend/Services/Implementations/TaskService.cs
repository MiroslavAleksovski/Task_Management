using Models.TaskDTOModels;
using Services.Interfaces;
using AccessLevel.Interfaces;
using Models.TaskDomainModels;
using Mappers;

namespace Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskGridDTOModel>> GetTasks()
        {
            var domainTasks = await _taskRepository.GetTasks();
            var dtoItems = domainTasks.Select(x => x.ToTaskGridDTOModel());
            return dtoItems;
        }

        public async Task<TaskDetailsDTOModel> GetTask(Guid taskId)
        {
            if (taskId == Guid.Empty)
            {
                throw new ArgumentException("taskId must be a valid non-empty GUID.", nameof(taskId));
            }

            var domainTask = await _taskRepository.GetTask(taskId);
            if (domainTask == null) throw new NullReferenceException("Task not found.");
            return domainTask.ToTaskDetailsDTOModel();
        }

        public async Task<Guid> AddUpdateTask(TaskAddUpdateDTOModel taskDto)
        {
            if (taskDto == null) throw new ArgumentNullException(nameof(taskDto));

            var domain = new TaskDetailsDomainModel
            {
                Id = taskDto.Id ?? Guid.NewGuid(),
                Name = taskDto.Name,
                Description = taskDto.Description
            };

            if (taskDto.Id.HasValue)
            {
                var taskId = await _taskRepository.UpdateTask(domain);
                return taskId;
            }
            else
            {
                var taskId = await _taskRepository.AddTask(domain);
                return taskId;
            }
        }

        public async Task DeleteTask(Guid taskId)
        {
            if (taskId == Guid.Empty)
            {
                throw new ArgumentException("taskId must be a valid non-empty GUID.", nameof(taskId));
            }

            await _taskRepository.DeleteTask(taskId);
        }
    }
}