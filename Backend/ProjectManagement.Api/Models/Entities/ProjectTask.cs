using System;
using ProjectManagement.Api.Models.Enums;

namespace ProjectManagement.Api.Models.Entities
{
    /// <summary>
    /// يمثل كيان المهمة المرتبطة بمشروع معين.
    /// </summary>
    public class ProjectTask
    {
        /// <summary>
        /// المعرف الفريد للمهمة.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        
        /// <summary>
        /// العنوان الرئيسي أو اسم المهمة.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// تفاصيل إضافية حول المهمة وما يجب إنجازه.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// حالة المهمة الحالية (على سبيل المثال: قيد الانتظار، قيد التنفيذ، مكتملة).
        /// </summary>
        public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Pending;
        
        /// <summary>
        /// تاريخ بداية العمل على المهمة.
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// تاريخ الاستحقاق المطلوب إنجاز المهمة بحلوله.
        /// </summary>
        public DateTime DueDate { get; set; }
        
        /// <summary>
        /// المعرف الفريد للمشروع الذي تنتمي إليه هذه المهمة.
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// المشروع المرتبط بهذه المهمة.
        /// </summary>
        public Project? Project { get; set; }
    }
}
