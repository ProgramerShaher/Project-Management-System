using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Api.Models.Entities;

namespace ProjectManagement.Api.Repositories.Interfaces
{
    /// <summary>
    /// واجهة (Interface) لعمليات مستودع المشاريع تطبيقاً لمبدأ Interface Segregation.
    /// </summary>
    public interface IProjectRepository
    {
        #region Read Operations
        Task<ProjectManagement.Api.Wrappers.PagedList<Project>> GetAllAsync(int pageNumber, int pageSize);
        Task<Project?> GetByIdAsync(Guid id);
        #endregion

        #region Write Operations
        Task<Project> AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Project project);
        #endregion
    }
}
