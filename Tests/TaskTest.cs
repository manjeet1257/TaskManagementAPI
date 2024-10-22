using TaskManagementAPI;
using TaskInfrastructureLayer;
using Microsoft.EntityFrameworkCore;
using TaskManagementDomain;

namespace Tests
{
    public class TaskControllerTests
    {
        private TaskContext _context;
        private TaskController _controller;
        private ITaskDetailService _taskDetailService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: "TaskManagementAPI")
            .Options;

            _context = new TaskContext(options);
            _controller = new TaskController(_context, _taskDetailService);
        }

        [Test]
        public async Task CanAddTask()
        {
            var task = new TaskDetailDTO { Name = "Test Task", Description = "Test Description", Deadline = DateTime.Now.AddYears(1)};

            var result = await _controller.Task(task);
            Assert.AreEqual(1, _context.Tasks.Count());
        }

        [Test]
        public async Task CanUpdateTask()
        {
            var task = new TaskDetail { Name = "Test Task" };
            _context.Tasks.Add(task);
            _context.SaveChanges();

            task.Name = "Updated Task";
            await _controller.Task(task.Id, new TaskDetailDTO
            {
                Deadline = task.Deadline,
                Description = task.Description,
                Id = task.Id,
                IsFavourite = task.IsFavourite,
                Name = task.Name
            });

            Assert.AreEqual("Updated Task", _context.Tasks.First().Name);
        }
    }
}