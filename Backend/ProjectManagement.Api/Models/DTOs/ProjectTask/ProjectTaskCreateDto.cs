using System;
using System.ComponentModel.DataAnnotations;
using ProjectManagement.Api.Models.Enums;

namespace ProjectManagement.Api.Models.DTOs.ProjectTask
{
    /// <summary>
    /// كائن نقل البيانات (DTO) لإنشاء مهمة جديدة في مشروع معين.
    /// </summary>
    public class ProjectTaskCreateDto
    {
        #region Properties
        [Required(ErrorMessage = "عنوان المهمة مطلوب.")]
        [MaxLength(200, ErrorMessage = "يجب ألا يتجاوز طول العنوان 200 حرف.")]
        public string Title { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "تاريخ البداية مطلوب.")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage = "تاريخ الاستحقاق مطلوب.")]
        public DateTime DueDate { get; set; }
        
        [Required(ErrorMessage = "حالة المهمة مطلوبة.")]
        public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Pending;

        [Required(ErrorMessage = "معرف المشروع مطلوب.")]
        public Guid ProjectId { get; set; }
        #endregion
    }
}
