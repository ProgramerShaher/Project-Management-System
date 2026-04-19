using System;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Api.Repositories.Implementations;
using ProjectManagement.Api.Repositories.Interfaces;
using ProjectManagement.Api.Services.Implementations;
using ProjectManagement.Api.Services.Interfaces;

namespace ProjectManagement.Api.Extensions
{
    /// <summary>
    /// كلاس إضافي لعزل إعدادات تسجيل الخدمات عن ملف Program.cs.
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            #endregion

            #region Services
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();
            #endregion

            #region AutoMapper
            services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            #endregion

            return services;
        }
    }
}
