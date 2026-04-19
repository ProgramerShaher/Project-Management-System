using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
//using Microsoft.OpenApi.Models;

namespace ProjectManagement.Api.Extensions
{
	public static class SwaggerServiceExtensions
	{
		public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
		{
			// إعداد Swagger/OpenAPI
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Project Management API",
					Version = "v1",
					Description = "API for managing projects and tasks - Recruitment Task"
				});

				// لتفعيل الـ Summary التي ستكتبها فوق الـ Methods
				var filePath = Path.Combine(AppContext.BaseDirectory, "ProjectManagement.Api.xml");
				if (File.Exists(filePath))
				{
					c.IncludeXmlComments(filePath);
				}
			});

			return services;
		}

		public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
		{
			// تفعيل الواجهة الرسومية لسواجير
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Management API v1");
			});

			return app;
		}
	}
}