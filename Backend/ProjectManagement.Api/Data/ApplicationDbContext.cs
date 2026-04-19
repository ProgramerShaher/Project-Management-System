using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Models.Entities;

namespace ProjectManagement.Api.Data
{
    /// <summary>
    /// يمثل سياق قاعدة البيانات والتواصل بين التطبيق و Entity Framework Core.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// جدول المشاريع في قاعدة البيانات.
        /// </summary>
        public DbSet<Project> Projects { get; set; }
        
        /// <summary>
        /// جدول المهام في قاعدة البيانات.
        /// </summary>
        public DbSet<ProjectTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // إعداد العلاقة بين المشروعات والمهام (رأس بأطراف - One to Many)
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade); // حذف المهام التابعة إذا تم حذف المشروع

            // إعدادات وشروط إضافية للحقول
            modelBuilder.Entity<Project>()
                .Property(p => p.Name).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<ProjectTask>()
                .Property(t => t.Title).IsRequired().HasMaxLength(200);
        }
    }
}
