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
        private readonly ITaskDetailService _taskDetailService;

        public TaskController(TaskContext context, ITaskDetailService taskDetailService)
        {
            _context = context;
            _taskDetailService = taskDetailService;
        }

        // GET: api/Task
        [HttpGet]
        public ActionResult<IEnumerable<TaskDetailDTO>> Tasks()
        {
            return _taskDetailService.GetTaskDetail();
        }

        [HttpPost]
        public async Task<ActionResult<TaskDetailDTO>> Task(TaskDetailDTO taskDetailDTO)
        {
            await _taskDetailService.AddTaskDetailAsync(taskDetailDTO);
            return Ok(taskDetailDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDetailDTO>> Task(int id, [FromBody] TaskDetailDTO taskDetailDTO)
        {
            if (id != taskDetailDTO.Id)
                return BadRequest("Task ID mismatch.");

            var taskDetail = await _taskDetailService.UpdateTaskDetailAsync(id, taskDetailDTO);

            if (taskDetail != null)
                return Ok(taskDetailDTO);
            else
                return BadRequest();

        }

    }
}
