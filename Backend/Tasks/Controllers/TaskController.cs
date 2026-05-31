using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models.TaskDTOModels;
using Services.Interfaces;

namespace Tasks.Controllers
{
    public class TaskController(ITaskService taskService) : BaseController
    {
        [Tags("Task")]
        [HttpGet(Name = "GetTasks")]
        public async Task<ActionResult<IEnumerable<TaskGridDTOModel>>> GetTasks()
        {
            try
            {
                var items = await taskService.GetTasks();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Tags("Task")]
        [HttpGet("{taskId:Guid}", Name = "GetTask")]
        public async Task<ActionResult<TaskDetailsDTOModel>> GetTask(Guid taskId)
        {
            try
            {
                var item = await taskService.GetTask(taskId);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Tags("Task")]
        [HttpPost(Name = "AddUpdateTask")]
        public async Task<ActionResult<Guid>> AddUpdateTask(TaskAddUpdateDTOModel taskDto)
        {
            try
            {
                var itemId = await taskService.AddUpdateTask(taskDto);
                return Ok(itemId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [Tags("Task")]
        [HttpDelete("{taskId:Guid}", Name = "DeleteTask")]
        public async Task<ActionResult> DeleteTask(Guid taskId)
        {
            try
            {
                await taskService.DeleteTask(taskId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }
    }
}
