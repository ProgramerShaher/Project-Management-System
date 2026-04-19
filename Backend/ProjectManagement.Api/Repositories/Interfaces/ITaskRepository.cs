using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Api.Models.Entities;
using ProjectManagement.Api.Models.Enums;

namespace ProjectManagement.Api.Repositories.Interfaces
{
    /// <summary>
    /// واجهة (Interface) لعمليات مستودع المهام تدعم الفلترة المتقدمة.
    /// </summary>
    public interface ITaskRepository
    {
        #region Read Operations
        Task<ProjectManagement.Api.Wrappers.PagedList<Models.Entities.ProjectTask>> GetTasksAsync(Guid? projectId, ProjectTaskStatus? status, int pageNumber, int pageSize);
        Task<Models.Entities.ProjectTask?> GetByIdAsync(Guid id);
        #endregion

        #region Write Operations
        Task<Models.Entities.ProjectTask> AddAsync(Models.Entities.ProjectTask task);
        Task UpdateAsync(Models.Entities.ProjectTask task);
        Task DeleteAsync(Models.Entities.ProjectTask task);
        #endregion
    }
}
