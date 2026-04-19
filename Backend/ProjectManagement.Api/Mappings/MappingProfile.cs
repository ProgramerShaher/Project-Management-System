using AutoMapper;
using ProjectManagement.Api.Models.DTOs.Project;
using ProjectManagement.Api.Models.DTOs.ProjectTask;
using ProjectManagement.Api.Models.Entities;

namespace ProjectManagement.Api.Mappings
{
    /// <summary>
    /// إعدادات خريطة التحويل بين كيانات قاعدة البيانات (Entities) و كائنات نقل البيانات (DTOs).
    /// </summary>
    public class MappingProfile : Profile
    {
        #region Constructor
        public MappingProfile()
        {
            // إعدادات المسارات الخاصة بالمشروع
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.TaskCount, opt => opt.MapFrom(src => src.Tasks.Count));
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();

            // إعدادات المسارات الخاصة بالمهام
            CreateMap<Models.Entities.ProjectTask, ProjectTaskDto>();
            CreateMap<ProjectTaskCreateDto, Models.Entities.ProjectTask>();
            CreateMap<ProjectTaskUpdateDto, Models.Entities.ProjectTask>();
        }
        #endregion
    }
}
