using TaskManagementAPI;
using TaskInfrastructureLayer;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class TaskControllerTests
    {

        private TaskContext _context;
        private TaskController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
                .UseInMemoryDatabase(databaseName: "TaskDatabase")
            .Options;

            _context = new TaskContext(options);
            _controller = new TaskController(_context);
        }

        [Test]
        public async Task CanAddTask()
        {
            var task = new TaskDetail { Name = "Test Task", Description = "Test Description" };

            var result = await _controller.AddTask(task);
            Assert.AreEqual(1, _context.Tasks.Count());
        }
    }
}