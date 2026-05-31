using Dapper.FluentMap.Mapping;
using Models.TaskDomainModels;

namespace Tasks.Configuration
{
    public class TaskGridDomainModelMap : EntityMap<TaskGridDomainModel>
    {
        public TaskGridDomainModelMap()
        {
            Map(i => i.Id).ToColumn("TaskId");
            Map(i => i.Name).ToColumn("TaskName");
        }
    }

    public class TaskDetailsDomainModelMap : EntityMap<TaskDetailsDomainModel>
    {
        public TaskDetailsDomainModelMap()
        {
            Map(i => i.Id).ToColumn("TaskId");
            Map(i => i.Name).ToColumn("TaskName");
            Map(i => i.Description).ToColumn("TaskDescription");
        }
    }
}
