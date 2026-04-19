using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Api.Models.DTOs.Project;
using ProjectManagement.Api.Wrappers;

namespace ProjectManagement.Api.Services.Interfaces
{
    /// <summary>
    /// واجهة طبقة الخدمات للمشاريع (Business Logic Layer).
    /// </summary>
    public interface IProjectService
    {
        Task<ApiResponse<IEnumerable<ProjectDto>>> GetAllProjectsAsync();
        Task<ApiResponse<ProjectDto>> GetProjectByIdAsync(Guid id);
        Task<ApiResponse<ProjectDto>> CreateProjectAsync(ProjectCreateDto createDto);
        Task<ApiResponse<string>> UpdateProjectAsync(Guid id, ProjectUpdateDto updateDto);
        Task<ApiResponse<string>> DeleteProjectAsync(Guid id);
    }
}
