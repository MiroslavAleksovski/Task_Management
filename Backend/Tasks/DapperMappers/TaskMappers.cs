using Dapper.FluentMap.Mapping;
using Models.TaskDomainModels;

namespace Tasks.DapperMappers
{
    public class TaskGridDomainModelMap : EntityMap<TaskGridDomainModel>
    {
        public TaskGridDomainModelMap()
        {
            Map(i => i.Id).ToColumn("TaskId");
            Map(i => i.Name).ToColumn("TaskName");
            Map(i => i.IsCompleted).ToColumn("IsCompleted");
        }
    }

    public class TaskDetailsDomainModelMap : EntityMap<TaskDetailsDomainModel>
    {
        public TaskDetailsDomainModelMap()
        {
            Map(i => i.Id).ToColumn("TaskId");
            Map(i => i.Name).ToColumn("TaskName");
            Map(i => i.Description).ToColumn("TaskDescription");
            Map(i => i.IsCompleted).ToColumn("IsCompleted");
        }
    }
}
