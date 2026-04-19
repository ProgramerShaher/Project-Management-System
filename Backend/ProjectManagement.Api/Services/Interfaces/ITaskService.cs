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
        Task<ApiResponse<IEnumerable<ProjectTaskDto>>> GetTasksAsync(Guid? projectId, ProjectTaskStatus? status);
        Task<ApiResponse<ProjectTaskDto>> GetTaskByIdAsync(Guid id);
        Task<ApiResponse<ProjectTaskDto>> CreateTaskAsync(ProjectTaskCreateDto createDto);
        Task<ApiResponse<string>> UpdateTaskAsync(Guid id, ProjectTaskUpdateDto updateDto);
        Task<ApiResponse<string>> DeleteTaskAsync(Guid id);
    }
}
