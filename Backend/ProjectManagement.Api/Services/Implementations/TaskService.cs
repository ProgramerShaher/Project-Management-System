using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManagement.Api.Models.DTOs.ProjectTask;
using ProjectManagement.Api.Models.Enums;
using ProjectManagement.Api.Repositories.Interfaces;
using ProjectManagement.Api.Services.Interfaces;
using ProjectManagement.Api.Wrappers;

namespace ProjectManagement.Api.Services.Implementations
{
    /// <summary>
    /// طبقة البزنس لوجيك الخاصة بالمهام.
    /// </summary>
    public class TaskService : ITaskService
    {
        #region Fields
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;
        #endregion

        #region Constructor
        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository, IMapper mapper, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Methods
        public async Task<ApiResponse<ProjectManagement.Api.Wrappers.PagedList<ProjectTaskDto>>> GetTasksAsync(Guid? projectId, ProjectTaskStatus? status, int pageNumber = 1, int pageSize = 10)
        {
            var pagedTasks = await _taskRepository.GetTasksAsync(projectId, status, pageNumber, pageSize);

            var dtos = _mapper.Map<List<ProjectTaskDto>>(pagedTasks.Items);
            var pagedDtos = new ProjectManagement.Api.Wrappers.PagedList<ProjectTaskDto>(dtos, pagedTasks.TotalCount, pagedTasks.PageNumber, pagedTasks.PageSize);

            return new ApiResponse<ProjectManagement.Api.Wrappers.PagedList<ProjectTaskDto>>(pagedDtos, "تم الجلب الفلترة بنجاح.");
        }

        public async Task<ApiResponse<ProjectTaskDto>> GetTaskByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) 
            {
                _logger.LogWarning("المهمة غير متوفرة: {Id}", id);
                return new ApiResponse<ProjectTaskDto>("المهمة غير متوفرة في النظام.");
            }

            var dto = _mapper.Map<ProjectTaskDto>(task);
            return new ApiResponse<ProjectTaskDto>(dto, "تم استرجاع المهمة.");
        }

        public async Task<ApiResponse<ProjectTaskDto>> CreateTaskAsync(ProjectTaskCreateDto createDto)
        {
            if (createDto.DueDate <= createDto.StartDate)
                return new ApiResponse<ProjectTaskDto>("تاريخ استحقاق المهمة يجب أن يكون بعد تاريخ بدايتها.");

            var projectExists = await _projectRepository.GetByIdAsync(createDto.ProjectId);
            if (projectExists == null)
            {
                _logger.LogWarning("المشروع غير موجود. المشروع: {ProjectId}", createDto.ProjectId);
                return new ApiResponse<ProjectTaskDto>("لا يمكن إضافة المهمة لأن المشروع المحدد غير موجود.");
            }

            if (createDto.StartDate < projectExists.StartDate)
                return new ApiResponse<ProjectTaskDto>("لا يمكن أن تبدأ المهمة قبل تاريخ بداية المشروع.");
                
            if (projectExists.EndDate.HasValue && createDto.DueDate > projectExists.EndDate.Value)
                return new ApiResponse<ProjectTaskDto>("لا يمكن أن تنتهي المهمة بعد تاريخ نهاية المشروع.");

            var pagedTasks = await _taskRepository.GetTasksAsync(createDto.ProjectId, null, 1, int.MaxValue);
            bool isOverlapping = pagedTasks.Items.Any(t => 
                (createDto.StartDate < t.DueDate) && (createDto.DueDate > t.StartDate));
            
            if (isOverlapping)
                return new ApiResponse<ProjectTaskDto>("توجد مهمة أخرى تتعارض زمنياً مع هذه المهمة في نفس المشروع.");

            var taskToCreate = _mapper.Map<Models.Entities.ProjectTask>(createDto);
            var createdTask = await _taskRepository.AddAsync(taskToCreate);

            var createdDto = _mapper.Map<ProjectTaskDto>(createdTask);
            
            _logger.LogInformation("تمت إضافة المهمة: {Id}", createdDto.Id);
            return new ApiResponse<ProjectTaskDto>(createdDto, "تم إنشاء المهمة بنجاح.");
        }

        public async Task<ApiResponse<ProjectTaskDto>> UpdateTaskAsync(Guid id, ProjectTaskUpdateDto updateDto)
        {
            if (updateDto.DueDate <= updateDto.StartDate)
                return new ApiResponse<ProjectTaskDto>("تاريخ استحقاق المهمة يجب أن يكون بعد تاريخ بدايتها.");

            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null)
                return new ApiResponse<ProjectTaskDto>("المهمة غير موجودة.");

            var projectExists = await _projectRepository.GetByIdAsync(existingTask.ProjectId);
            if (projectExists != null)
            {
                if (updateDto.StartDate < projectExists.StartDate)
                    return new ApiResponse<ProjectTaskDto>("لا يمكن أن تبدأ المهمة قبل تاريخ بداية المشروع.");
                    
                if (projectExists.EndDate.HasValue && updateDto.DueDate > projectExists.EndDate.Value)
                    return new ApiResponse<ProjectTaskDto>("لا يمكن أن تنتهي المهمة بعد تاريخ نهاية المشروع.");

                var pagedTasks = await _taskRepository.GetTasksAsync(existingTask.ProjectId, null, 1, int.MaxValue);
                bool isOverlapping = pagedTasks.Items.Any(t => 
                    t.Id != id && (updateDto.StartDate < t.DueDate) && (updateDto.DueDate > t.StartDate));
                
                if (isOverlapping)
                    return new ApiResponse<ProjectTaskDto>("توجد مهمة أخرى تتعارض زمنياً مع هذه المهمة في نفس المشروع.");
            }

            _mapper.Map(updateDto, existingTask);
            await _taskRepository.UpdateAsync(existingTask);

            var updatedDto = _mapper.Map<ProjectTaskDto>(existingTask);
            _logger.LogInformation("تم تحديث المهمة: {Id}", id);
            return new ApiResponse<ProjectTaskDto>(updatedDto, "تم التعديل وحفظ التغييرات.");
        }

        public async Task<ApiResponse<bool>> DeleteTaskAsync(Guid id)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null)
                return new ApiResponse<bool>("المهمة غير موجودة لتتم إزالتها.");

            await _taskRepository.DeleteAsync(existingTask);
            
            _logger.LogInformation("تم حذف المهمة بنجاح: {Id}", id);
            return new ApiResponse<bool>(true, "تم حذف المهمة.");
        }
        #endregion
    }
}
