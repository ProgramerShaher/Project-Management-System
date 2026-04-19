namespace ProjectManagement.Api.Wrappers
{
    /// <summary>
    /// فئة عامة لتغليف وتوحيد جميع الاستجابات العائدة من واجهة برمجة التطبيقات (API).
    /// </summary>
    /// <typeparam name="T">نوع البيانات المخرجة.</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// يشير إلى ما إذا كانت العملية قد تمت بنجاح.
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// رسالة نصية توضح تفاصيل الاستجابة (مثل رسالة نجاح أو خطأ).
        /// </summary>
        public string? Message { get; set; }
        
        /// <summary>
        /// البيانات الفعلية المرجعة من العملية (قد تكون مجموعة أو عنصر واحد).
        /// </summary>
        public T? Data { get; set; }
        
        /// <summary>
        /// مصفوفة تحتوي على تفاصيل الأخطاء في حال فشل العملية.
        /// </summary>
        public string[]? Errors { get; set; }

        public ApiResponse() { }

        public ApiResponse(T data, string? message = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
        }

        public ApiResponse(string[] errors, string? message = null)
        {
            Success = false;
            Message = message;
            Errors = errors;
        }
    }
}
