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
        Task<IEnumerable<Models.Entities.ProjectTask>> GetAllAsync();
        Task<Models.Entities.ProjectTask?> GetByIdAsync(Guid id);
        Task<IEnumerable<Models.Entities.ProjectTask>> GetTasksByProjectIdAsync(Guid projectId);
        Task<IEnumerable<Models.Entities.ProjectTask>> GetTasksByStatusAsync(ProjectTaskStatus status);
        Task<IEnumerable<Models.Entities.ProjectTask>> GetTasksByProjectIdAndStatusAsync(Guid projectId, ProjectTaskStatus status);
        #endregion

        #region Write Operations
        Task<Models.Entities.ProjectTask> AddAsync(Models.Entities.ProjectTask task);
        Task UpdateAsync(Models.Entities.ProjectTask task);
        Task DeleteAsync(Models.Entities.ProjectTask task);
        #endregion
    }
}
