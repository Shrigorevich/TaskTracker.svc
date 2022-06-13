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

        [HttpGet("")]
        public ActionResult GetTasks()
        {
            var result = _taskContext.Tasks;
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult GetTaskById(int id)
        {
            var result = _taskContext.Tasks.Find(id);
            return result != null ? Ok(result) : NotFound();
        }

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

        private void LoadSampleData()
        {
            string fileData = System.IO.File.ReadAllText("./DataSamples/TasksSample.json");
            var tasks = JsonSerializer.Deserialize<List<WorkTask>>(fileData);
            if (!_taskContext.Tasks.Any())
            {
                _taskContext.AddRangeAsync(tasks);
                _taskContext.SaveChanges();
            }
        }
    };
}