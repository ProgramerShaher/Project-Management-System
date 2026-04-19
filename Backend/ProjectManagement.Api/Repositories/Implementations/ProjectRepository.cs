using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Data;
using ProjectManagement.Api.Models.Entities;
using ProjectManagement.Api.Repositories.Interfaces;

namespace ProjectManagement.Api.Repositories.Implementations
{
    /// <summary>
    /// تطبيق (Implementation) لمستودع المشاريع والتفاعل مع قاعدة البيانات.
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Read Operations
        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            // نستخدم Include لنجلب المهام من أجل حساب عددها في الواجهة
            return await _context.Projects
                .Include(p => p.Tasks)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        #endregion

        #region Write Operations
        public async Task<Project> AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
