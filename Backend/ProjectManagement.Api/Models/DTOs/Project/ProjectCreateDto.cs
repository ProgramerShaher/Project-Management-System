using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Api.Models.DTOs.Project
{
    /// <summary>
    /// كائن نقل البيانات (DTO) المخصص لإنشاء مشروع جديد متضمناً القيود (Validation).
    /// </summary>
    public class ProjectCreateDto
    {
        #region Properties
        [Required(ErrorMessage = "اسم المشروع مطلوب.")]
        [MaxLength(200, ErrorMessage = "يجب ألا يتجاوز طول الاسم 200 حرف.")]
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "تاريخ البدء مطلوب.")]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        #endregion
    }
}
