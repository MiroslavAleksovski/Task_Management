using Microsoft.AspNetCore.Mvc;
using Models.TaskDTOModels;
using Services.Interfaces;

namespace Tasks.Controllers
{
    public class TaskController(ITaskService taskService) : BaseController
    {
        [HttpGet(Name = "GetTasks")]
        public async Task<IEnumerable<TaskGridDTOModel>> GetTasks()
        {
            return await taskService.GetTasks();
        }
    }
}
