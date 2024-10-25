using TaskManagementAPI;
using TaskInfrastructureLayer;
using Microsoft.EntityFrameworkCore;
using TaskManagementDomain;
using Moq;

namespace Tests
{
    [TestFixture]
    public class TaskServiceTests
    {
        private Mock<DbSet<TaskDetail>> _mockTaskSet;
        private Mock<TaskContext> _mockContext;
        private TaskDetailService _taskService;

        [SetUp]
        public void Setup()
        {
            // Sample data to be used in the mock DbSet
            var tasks = new List<TaskDetail>
        {
            new TaskDetail { Id = 1, Name = "Task 1" },
            new TaskDetail { Id = 2, Name = "Task 2" }
        }.AsQueryable();

            // Mock the DbSet
            _mockTaskSet = new Mock<DbSet<TaskDetail>>();
            _mockTaskSet.As<IQueryable<TaskDetail>>().Setup(m => m.Provider).Returns(tasks.Provider);
            _mockTaskSet.As<IQueryable<TaskDetail>>().Setup(m => m.Expression).Returns(tasks.Expression);
            _mockTaskSet.As<IQueryable<TaskDetail>>().Setup(m => m.ElementType).Returns(tasks.ElementType);
            _mockTaskSet.As<IQueryable<TaskDetail>>().Setup(m => m.GetEnumerator()).Returns(tasks.GetEnumerator());

            // Mock the DbContext
            _mockContext = new Mock<TaskContext>();
            _mockContext.Setup(c => c.Tasks).Returns(_mockTaskSet.Object);

            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Initialize the service with the mocked context
            _taskService = new TaskDetailService(_mockContext.Object);
        }

        [Test]
        public void GetTasks_ShouldReturnAllTasks()
        {
            // Act
            var result = _taskService.GetTaskDetail();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Task 1", result.First().Name);
        }

        [Test]
        public void GetTasks_WhenNoTasks_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyTasks = new List<TaskDetail>().AsQueryable();

            // Re-mock the DbSet to return no tasks
            _mockTaskSet.As<IQueryable<TaskDetail>>().Setup(m => m.Provider).Returns(emptyTasks.Provider);
            _mockTaskSet.As<IQueryable<TaskDetail>>().Setup(m => m.Expression).Returns(emptyTasks.Expression);
            _mockTaskSet.As<IQueryable<TaskDetail>>().Setup(m => m.ElementType).Returns(emptyTasks.ElementType);
            _mockTaskSet.As<IQueryable<TaskDetail>>().Setup(m => m.GetEnumerator()).Returns(emptyTasks.GetEnumerator());

            // Act
            var result = _taskService.GetTaskDetail();

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}