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
        private readonly ILogger<TaskController> _logger;
        private readonly TaskContext _taskContext;

        public TaskController(ILogger<TaskController> logger, TaskContext taskContext)
        {
            _logger = logger;
            _taskContext = taskContext;
        }

        [HttpGet("")]
        public ActionResult Get()
        {
            var result = _taskContext.Tasks;
            return Ok(result);
        }

        [HttpPost("loadSample")]
        public ActionResult LoadSampleData()
        {
            string fileData = System.IO.File.ReadAllText("./DataSamples/TasksSample.json");
            //var tasks = JsonConvert.DeserializeObject<List<WorkTask>>(fileData);
            var tasks = JsonSerializer.Deserialize<List<WorkTask>>(fileData);
            if (!_taskContext.Tasks.Any())
            {
                _taskContext.AddRange(tasks);
                _taskContext.SaveChanges();
                return Ok();
            }
            
            return Ok();
        }
    };
}