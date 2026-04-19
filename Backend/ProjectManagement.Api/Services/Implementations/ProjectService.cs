using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManagement.Api.Models.DTOs.Project;
using ProjectManagement.Api.Models.Entities;
using ProjectManagement.Api.Repositories.Interfaces;
using ProjectManagement.Api.Services.Interfaces;
using ProjectManagement.Api.Wrappers;

namespace ProjectManagement.Api.Services.Implementations
{
    /// <summary>
    /// طبقة البزنس لوجيك (Service Layer) الخاصة بالمشاريع.
    /// </summary>
    public class ProjectService : IProjectService
    {
        #region Fields
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectService> _logger;
        #endregion

        #region Constructor
        public ProjectService(IProjectRepository projectRepository, IMapper mapper, ILogger<ProjectService> logger)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Methods
        public async Task<ApiResponse<IEnumerable<ProjectDto>>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
            
            _logger.LogInformation("تم تجهيز بيانات المشاريع في طبقة الخدمات.");
            return new ApiResponse<IEnumerable<ProjectDto>>(dtos, "تم استرجاع المشاريع بنجاح.");
        }

        public async Task<ApiResponse<ProjectDto>> GetProjectByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                _logger.LogWarning("مشروع غير موجود: {Id}", id);
                return new ApiResponse<ProjectDto>("المشروع المحدد غير موجود.");
            }

            var dto = _mapper.Map<ProjectDto>(project);
            return new ApiResponse<ProjectDto>(dto);
        }

        public async Task<ApiResponse<ProjectDto>> CreateProjectAsync(ProjectCreateDto createDto)
        {
            var projectToCreate = _mapper.Map<Project>(createDto);
            var createdProject = await _projectRepository.AddAsync(projectToCreate);

            var createdDto = _mapper.Map<ProjectDto>(createdProject);
            
            _logger.LogInformation("تم إنشاء مشروع جديد: {Id}", createdDto.Id);
            return new ApiResponse<ProjectDto>(createdDto, "تم إنشاء المشروع بنجاح.");
        }

        public async Task<ApiResponse<ProjectDto>> UpdateProjectAsync(Guid id, ProjectUpdateDto updateDto)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
                return new ApiResponse<ProjectDto>("لا يوجد مشروع بهذا المعرف للتعديل.");

            _mapper.Map(updateDto, existingProject);
            await _projectRepository.UpdateAsync(existingProject);

            var updatedDto = _mapper.Map<ProjectDto>(existingProject);
            _logger.LogInformation("تم تعديل المشروع: {Id}", id);
            return new ApiResponse<ProjectDto>(updatedDto, "تم تعديل المشروع بنجاح.");
        }

        public async Task<ApiResponse<bool>> DeleteProjectAsync(Guid id)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
                return new ApiResponse<bool>("لا يوجد مشروع بهذا المعرف للحذف.");

            await _projectRepository.DeleteAsync(existingProject);
            
            _logger.LogInformation("تم حذف المشروع: {Id}", id);
            return new ApiResponse<bool>(true, "تم حذف المشروع بنجاح.");
        }
        #endregion
    }
}
