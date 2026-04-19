using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManagement.Api.Data;
using ProjectManagement.Api.Extensions;
using ProjectManagement.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// إضافة الاتصال بقاعدة البيانات (Connection String)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// تسجيل الخدمات والـ Repositories والـ AutoMapper الخاص بنا
builder.Services.AddApplicationServices();

builder.Services.AddControllers();

// إعدادات واجهة Swagger القديمة المخصصة لديكم
if (System.Reflection.Assembly.GetExecutingAssembly().GetType("ProjectManagement.Api.Extensions.SwaggerServiceExtensions") != null)
{
    builder.Services.AddSwaggerGen(); // كإجراء احتياطي
}

var app = builder.Build();

// الميدل وير لالتقاط كل الأخطاء برمجياً (يجب أن يكون في البداية)
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
