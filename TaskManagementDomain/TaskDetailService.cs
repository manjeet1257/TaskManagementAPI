using Microsoft.EntityFrameworkCore;
using TaskInfrastructureLayer;

namespace TaskManagementDomain
{
    public class TaskDetailService : ITaskDetailService
    {
        private readonly TaskContext _taskContext;

        public TaskDetailService(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }

        public async Task<TaskDetailDTO> AddTaskDetailAsync(TaskDetailDTO taskDetailDTO)
        {
            var taskDetail = new TaskDetail
            {
                Deadline = taskDetailDTO.Deadline,
                Description = taskDetailDTO.Description,
                IsFavourite = taskDetailDTO.IsFavourite,
                Name = taskDetailDTO.Name
            };

            _taskContext.Tasks.Add(taskDetail);
            await _taskContext.SaveChangesAsync();
            taskDetailDTO.Id = taskDetail.Id;
            return taskDetailDTO;
        }

        public Task<TaskDetailDTO> DeleteTaskDetailAsync(int id, TaskDetailDTO taskDetailDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskDetailDTO>> GetTaskDetailAsync()
        {
            var taskDetails = await _taskContext.Tasks.ToListAsync();

            return taskDetails.Select(t => new TaskDetailDTO
            {
                Deadline = t.Deadline,
                Description = t.Description,
                Id = t.Id,
                IsFavourite = t.IsFavourite,
                Name = t.Name
            }).ToList();
        }

        public Task<TaskDetailDTO> GetTaskDetailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskDetailDTO> UpdateTaskDetailAsync(int id, TaskDetailDTO taskDetailDTO)
        {
            var taskDetail = await _taskContext.Tasks.FindAsync(id);

            if (taskDetail == null)
            {
                return null;
            }

            // Update the task details
            taskDetail.Name = taskDetailDTO.Name;
            taskDetail.Description = taskDetailDTO.Description;
            taskDetail.Deadline = taskDetailDTO.Deadline;
            taskDetail.IsFavourite = taskDetailDTO.IsFavourite;

            // Save changes to the database
            await _taskContext.SaveChangesAsync();
            return taskDetailDTO;
        }
    }
}
