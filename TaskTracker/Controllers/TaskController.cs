using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
using System.Text.Json;
using TaskTracker.DataAccess;
using TaskTracker.DomainLayer;

namespace TaskTracker.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _taskContext;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger, TaskContext taskContext)
        {
            _logger = logger;
            _taskContext = taskContext;
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public ActionResult GetTasks()
        {
            var result = _taskContext.Tasks;
            return Ok(result);
        }

        /// <summary>
        /// Get task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public ActionResult GetTaskById(int id)
        {
            var result = _taskContext.Tasks.Find(id);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Create task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult CreateTask([FromBody] WorkTask task)
        {
            try
            {
                _taskContext.Tasks.Add(task);
                _taskContext.SaveChanges();
                return Created("api/tasks", task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPut("")]
        public ActionResult UpdateTask([FromBody] WorkTask task)
        {
            try
            {
                _taskContext.Tasks.Update(task);
                _taskContext.SaveChanges();
                return Ok(task);
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete task by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public ActionResult DeleteTask(int id)
        {
            try
            {
                var task = new WorkTask { Id = id };
                _taskContext.Tasks.Attach(task);
                _taskContext.Tasks.Remove(task);
                _taskContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Test endpoint. Does not interact with DB
        /// </summary>
        /// <returns></returns>
        [HttpPut("test")]
        public ActionResult Test()
        {
            string fileData = System.IO.File.ReadAllText("./DataSamples/TasksSample.json");
            var tasks = JsonSerializer.Deserialize<List<WorkTask>>(fileData);

            return Ok(tasks);   
        }
    };
}