using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Models.DTOs.ProjectTask;
using ProjectManagement.Api.Models.Enums;
using ProjectManagement.Api.Services.Interfaces;
using ProjectManagement.Api.Wrappers;

namespace ProjectManagement.Api.Controllers
{
    /// <summary>
    /// المتحكم الخاص بإدارة المهام (Thin Controller).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        #region Endpoint - GET ALL
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProjectTaskDto>>>> GetTasks([FromQuery] Guid? projectId, [FromQuery] ProjectTaskStatus? status)
        {
            var response = await _taskService.GetTasksAsync(projectId, status);
            return Ok(response);
        }
        #endregion

        #region Endpoint - GET BY ID
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ApiResponse<ProjectTaskDto>>> GetTaskById(Guid id)
        {
            var response = await _taskService.GetTaskByIdAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
        #endregion

        #region Endpoint - POST
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProjectTaskDto>>> CreateTask([FromBody] ProjectTaskCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<ProjectTaskDto>("البيانات غير كافية لإنشاء المهمة."));
            var response = await _taskService.CreateTaskAsync(createDto);
            if (!response.Success) return NotFound(response); // المشروع غير موجود
            return CreatedAtAction(nameof(GetTaskById), new { id = response.Data?.Id }, response);
        }
        #endregion

        #region Endpoint - PUT
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateTask(Guid id, [FromBody] ProjectTaskUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<string>("بيانات المهمة غير صحيحة."));
            var response = await _taskService.UpdateTaskAsync(id, updateDto);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
        #endregion

        #region Endpoint - DELETE
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteTask(Guid id)
        {
            var response = await _taskService.DeleteTaskAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
        #endregion
    }
}
