using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Api.Models.DTOs.ProjectTask;
using ProjectManagement.Api.Models.Enums;
using ProjectManagement.Api.Wrappers;

namespace ProjectManagement.Api.Services.Interfaces
{
    /// <summary>
    /// واجهة طبقة الخدمات للمهام (Business Logic Layer).
    /// </summary>
    public interface ITaskService
    {
        Task<ApiResponse<ProjectManagement.Api.Wrappers.PagedList<ProjectTaskDto>>> GetTasksAsync(Guid? projectId, ProjectTaskStatus? status, int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<ProjectTaskDto>> GetTaskByIdAsync(Guid id);
        Task<ApiResponse<ProjectTaskDto>> CreateTaskAsync(ProjectTaskCreateDto createDto);
        Task<ApiResponse<ProjectTaskDto>> UpdateTaskAsync(Guid id, ProjectTaskUpdateDto updateDto);
        Task<ApiResponse<bool>> DeleteTaskAsync(Guid id);
    }
}
