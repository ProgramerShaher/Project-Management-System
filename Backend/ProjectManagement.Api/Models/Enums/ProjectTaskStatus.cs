namespace ProjectManagement.Api.Models.Enums
{
    /// <summary>
    /// يمثل الحالات المختلفة التي يمكن أن تمر بها المهمة.
    /// </summary>
    public enum ProjectTaskStatus
    {
        /// <summary>
        /// المهمة قيد الانتظار ولم يتم البدء بها بعد.
        /// </summary>
        Pending,
        
        /// <summary>
        /// المهمة قيد التنفيذ حالياً.
        /// </summary>
        InProgress,
        
        /// <summary>
        /// تم إنجاز المهمة بالكامل.
        /// </summary>
        Completed
    }
}
