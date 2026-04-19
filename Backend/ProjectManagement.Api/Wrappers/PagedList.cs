using System;
using System.Collections.Generic;

namespace ProjectManagement.Api.Wrappers
{
    /// <summary>
    /// فئة عامة مسؤولة عن إدارة تقسيم البيانات (Pagination) لتسهيل العرض المبوب.
    /// </summary>
    /// <typeparam name="T">نوع العناصر الموجودة داخل القائمة.</typeparam>
    public class PagedList<T>
    {
        /// <summary>
        /// قائمة العناصر في الصفحة الحالية.
        /// </summary>
        public List<T> Items { get; set; } = new List<T>();
        
        /// <summary>
        /// رقم الصفحة الحالية.
        /// </summary>
        public int PageNumber { get; set; }
        
        /// <summary>
        /// عدد العناصر المسموح بها في الصفحة الواحدة.
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// العدد الإجمالي لجميع العناصر في قاعدة البيانات المطابقة للشروط.
        /// </summary>
        public int TotalCount { get; set; }
        
        /// <summary>
        /// إجمالي عدد الصفحات بعد تقسيم البيانات.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        
        /// <summary>
        /// يشير إلى ما إذا كان هناك صفحة سابقة.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;
        
        /// <summary>
        /// يشير إلى ما إذا كان هناك صفحة تالية.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;

        public PagedList() { }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Items = items;
        }
    }
}
