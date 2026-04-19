using System;

namespace ProjectManagement.Api.Models.DTOs.Project
{
    /// <summary>
    /// كائن نقل البيانات (DTO) المخصص لعمليات القراءة وعرض بيانات المشاريع.
    /// </summary>
    public class ProjectDto
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// عدد المهام المرتبطة بهذا المشروع (يتم حسابه تلقائياً عبر AutoMapper).
        /// </summary>
        public int TaskCount { get; set; }
        #endregion
    }
}
