using System;
using ProjectManagement.Api.Models.Enums;

namespace ProjectManagement.Api.Models.DTOs.ProjectTask
{
    /// <summary>
    /// كائن نقل البيانات (DTO) لعرض تفاصيل المهمة.
    /// </summary>
    public class ProjectTaskDto
    {
        #region Properties
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectTaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ProjectId { get; set; }
        #endregion
    }
}
