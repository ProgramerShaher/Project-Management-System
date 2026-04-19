using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Api.Models.DTOs.Project
{
    /// <summary>
    /// كائن نقل البيانات (DTO) لتحديث وتعديل بيانات المشروع.
    /// </summary>
    public class ProjectUpdateDto
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
