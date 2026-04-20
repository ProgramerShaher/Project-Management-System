using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Api.Models.Entities;
using ProjectManagement.Api.Models.Enums;
using ProjectManagement.Api.Wrappers;

namespace ProjectManagement.Api.Repositories.Interfaces
{
    /// <summary>
    /// واجهة (Interface) لعمليات مستودع المهام تدعم الفلترة المتقدمة.
    /// </summary>
    public interface ITaskRepository
    {
        #region Read Operations
        Task<PagedList<ProjectTask>> GetTasksAsync(Guid? projectId, ProjectTaskStatus? status, int pageNumber, int pageSize);
        Task<ProjectTask?> GetByIdAsync(Guid id);
        #endregion

        #region Write Operations
        Task<ProjectTask> AddAsync(ProjectTask task);
        Task UpdateAsync(ProjectTask task);
        Task DeleteAsync(ProjectTask task);
        #endregion
    }
}
