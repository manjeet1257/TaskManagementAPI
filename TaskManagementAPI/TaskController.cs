using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskInfrastructureLayer;
using TaskManagementDomain;

namespace TaskManagementAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDetailDTO>>> GetTasks()
        {
            return null;
        }

    }
}
