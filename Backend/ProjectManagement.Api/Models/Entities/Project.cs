using System;
using System.Collections.Generic;

namespace ProjectManagement.Api.Models.Entities
{
    /// <summary>
    /// يمثل كيان المشروع في النظام.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// المعرف الفريد للمشروع.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        
        /// <summary>
        /// اسم المشروع.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// وصف تفصيلي لأهداف أو تفاصيل المشروع.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// تاريخ بداية العمل على المشروع.
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ الانتهاء الفعلي المتوقع للمشروع (قد يكون فارغاً إذا لم يحدد).
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// تاريخ وتوقيت إنشاء سجل المشروع في النظام.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// قائمة المهام المرتبطة بهذا المشروع.
        /// </summary>
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
