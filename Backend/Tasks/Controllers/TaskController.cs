using Infrastructure.ExceptionExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Models.TaskDTOModels;
using Services.Interfaces;

namespace Tasks.Controllers
{
    public class TaskController(ITaskService taskService) : BaseController
    {
        /// <summary>
        /// Retrieves all tasks.
        /// </summary>
        /// <returns>List of task grid DTOs.</returns>
        [Tags("Task")]
        [HttpGet(Name = "GetTasks")]
        [ProducesResponseType(typeof(IEnumerable<TaskGridDTOModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TaskGridDTOModel>>> GetTasks()
        {
            try
            {
                var items = await taskService.GetTasks();
                return Ok(items);
            }
            catch (CustomException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves a task by id.
        /// </summary>
        /// <param name="taskId">Task unique identifier (GUID).</param>
        /// <returns>Task details DTO.</returns>
        [Tags("Task")]
        [HttpGet("{taskId:Guid}", Name = "GetTask")]
        [ProducesResponseType(typeof(TaskDetailsDTOModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskDetailsDTOModel>> GetTask(Guid taskId)
        {
            try
            {
                var item = await taskService.GetTask(taskId);
                return Ok(item);
            }
            catch (CustomException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Adds a new task or updates an existing one.
        /// </summary>
        /// <param name="taskDto">Task DTO with data to insert or update.</param>
        /// <returns>GUID of the created or updated task.</returns>
        [Tags("Task")]
        [HttpPost(Name = "AddUpdateTask")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> AddUpdateTask(TaskAddUpdateDTOModel taskDto)
        {
            try
            {
                var itemId = await taskService.AddUpdateTask(taskDto);
                return Ok(itemId);
            }
            //handle specific custom exception types here if needed:
            catch (CustomException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes a task by id.
        /// </summary>
        /// <param name="taskId">Task unique identifier (GUID).</param>
        /// <returns>HTTP 200 on success.</returns>
        [Tags("Task")]
        [HttpDelete("{taskId:Guid}", Name = "DeleteTask")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTask(Guid taskId)
        {
            try
            {
                await taskService.DeleteTask(taskId);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
