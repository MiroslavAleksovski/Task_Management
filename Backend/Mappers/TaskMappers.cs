using Models.TaskDomainModels;
using Models.TaskDTOModels;

namespace Mappers
{
    public static class TaskMappers
    {
        public static TaskGridDTOModel ToTaskGridDTOModel(this TaskGridDomainModel domainModel)
        {
            return new TaskGridDTOModel
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                IsCompleted = domainModel.IsCompleted
            };
        }

        public static TaskDetailsDTOModel ToTaskDetailsDTOModel(this TaskDetailsDomainModel domainModel)
        {
            return new TaskDetailsDTOModel
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                Description = domainModel.Description,
                IsCompleted = domainModel.IsCompleted
            };
        }
    }
}
