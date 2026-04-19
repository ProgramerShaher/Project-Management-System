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
        public async Task<ProjectManagement.Api.Wrappers.PagedList<Project>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Projects.Include(p => p.Tasks).AsNoTracking();
            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            
            return new ProjectManagement.Api.Wrappers.PagedList<Project>(items, count, pageNumber, pageSize);
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
