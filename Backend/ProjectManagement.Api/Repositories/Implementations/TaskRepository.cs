using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Data;
using ProjectManagement.Api.Models.Entities;
using ProjectManagement.Api.Models.Enums;
using ProjectManagement.Api.Repositories.Interfaces;

namespace ProjectManagement.Api.Repositories.Implementations
{
    /// <summary>
    /// تطبيق لعمليات سجل المهام في قاعدة البيانات.
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Read Operations
        public async Task<ProjectManagement.Api.Wrappers.PagedList<Models.Entities.ProjectTask>> GetTasksAsync(Guid? projectId, ProjectTaskStatus? status, int pageNumber, int pageSize)
        {
            var query = _context.Tasks.AsNoTracking().AsQueryable();

            if (projectId.HasValue)
                query = query.Where(t => t.ProjectId == projectId.Value);

            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new ProjectManagement.Api.Wrappers.PagedList<Models.Entities.ProjectTask>(items, count, pageNumber, pageSize);
        }

        public async Task<Models.Entities.ProjectTask?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }
        #endregion

        #region Write Operations
        public async Task<Models.Entities.ProjectTask> AddAsync(Models.Entities.ProjectTask task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task UpdateAsync(Models.Entities.ProjectTask task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Models.Entities.ProjectTask task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
