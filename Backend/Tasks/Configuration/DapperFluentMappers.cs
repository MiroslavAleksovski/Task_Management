using Dapper.FluentMap.Mapping;
using Models.TaskDomainModels;

namespace Tasks.Configuration
{
    public class TaskDetailsDomainModelMap : EntityMap<TaskDetailsDomainModel>
    {
        public TaskDetailsDomainModelMap()
        {
            Map(i => i.Id).ToColumn("TaskId");
            Map(i => i.Name).ToColumn("TaskName");
        }
    }
}
