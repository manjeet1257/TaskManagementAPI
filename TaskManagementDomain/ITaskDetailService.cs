using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskInfrastructureLayer;

namespace TaskManagementDomain
{
    public interface ITaskDetailService
    {
        Task<List<TaskDetailDTO>> GetTaskDetailAsync();
    }
}
