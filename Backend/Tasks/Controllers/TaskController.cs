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
        /// Retrieves tasks optionally filtered by the provided filter model.
        /// </summary>
        /// <param name="filter">Optional filter supplied in the request body. If omitted, all tasks are returned.</param>
        /// <response code="200">Returns the list of tasks matching the filter.</response>
        /// <response code="400">Bad request when the provided filter is invalid.</response>
        /// <response code="500">Server error while processing the request.</response>
        [Tags("Task")]
        [HttpPost(Name = "GetTasks")]
        [ProducesResponseType(typeof(IEnumerable<TaskGridDTOModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TaskGridDTOModel>>> GetTasks([FromBody] TaskFilterModel? filter = null)
        {
            try
            {
                var items = await taskService.GetTasks(filter);
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
        /// Retrieves a single task by id.
        /// </summary>
        /// <param name="taskId">Task unique identifier (GUID).</param>
        /// <response code="200">Returns the task details.</response>
        /// <response code="404">Task with the specified id was not found.</response>
        /// <response code="400">Bad request when the provided id is invalid.</response>
        /// <response code="500">Server error while processing the request.</response>
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
        /// <response code="200">Returns the GUID of the created or updated task.</response>
        /// <response code="400">Bad request when the provided model is invalid.</response>
        /// <response code="500">Server error while processing the request.</response>
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
        /// <response code="200">Task deleted successfully.</response>
        /// <response code="400">Bad request when the provided id is invalid.</response>
        /// <response code="500">Server error while processing the request.</response>
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
