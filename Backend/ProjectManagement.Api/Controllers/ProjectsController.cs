using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Models.DTOs.Project;
using ProjectManagement.Api.Services.Interfaces;
using ProjectManagement.Api.Wrappers;

namespace ProjectManagement.Api.Controllers
{
    /// <summary>
    /// المتحكم الخاص بإدارة بيانات المشاريع (Thin Controller).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        #region Endpoint - GET ALL
        [HttpGet]
        public async Task<ActionResult<ApiResponse<ProjectManagement.Api.Wrappers.PagedList<ProjectDto>>>> GetAllProjects([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _projectService.GetAllProjectsAsync(pageNumber, pageSize);
            return Ok(response);
        }
        #endregion

        #region Endpoint - GET BY ID
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> GetProjectById(Guid id)
        {
            var response = await _projectService.GetProjectByIdAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
        #endregion

        #region Endpoint - POST
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> CreateProject([FromBody] ProjectCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<ProjectDto>("بيانات المشروع غير صحيحة."));
            var response = await _projectService.CreateProjectAsync(createDto);
            return CreatedAtAction(nameof(GetProjectById), new { id = response.Data?.Id }, response);
        }
        #endregion

        #region Endpoint - PUT
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> UpdateProject(Guid id, [FromBody] ProjectUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<ProjectDto>("البيانات المدخلة غير صحيحة."));
            var response = await _projectService.UpdateProjectAsync(id, updateDto);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
        #endregion

        #region Endpoint - DELETE
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProject(Guid id)
        {
            var response = await _projectService.DeleteProjectAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
        #endregion
    }
}
