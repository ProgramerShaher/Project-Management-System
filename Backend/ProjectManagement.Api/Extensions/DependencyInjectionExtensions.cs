using System;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Api.Repositories.Implementations;
using ProjectManagement.Api.Repositories.Interfaces;

namespace ProjectManagement.Api.Extensions
{
    /// <summary>
    /// كلاس إضافي (Extension Method) لعزل إعدادات تسجيل الخدمات (Dependency Injection) عن ملف Program.cs.
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// تسجيل كافة الخدمات والمستودعات وعمليات الربط (AutoMapper) للتطبيق.
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            #endregion

            #region AutoMapper
            // يتم البحث عن جميع فئات الـ Profile ويربطها تلقائياً
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            return services;
        }
    }
}
