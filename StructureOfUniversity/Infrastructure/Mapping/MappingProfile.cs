using AutoMapper;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.DTOs.Faculty.Requests;
using StructureOfUniversity.DTOs.Faculty.Response;
using StructureOfUniversity.DTOs.Student.Requests;
using StructureOfUniversity.DTOs.Student.Response;
using StructureOfUniversity.DTOs.Teacher.Requests;
using StructureOfUniversity.DTOs.Teacher.Response;

namespace StructureOfUniversity.Infrastructure.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DbTeacher, GetTeacherResponse>();
        CreateMap<DbStudent, GetStudentResponse>();
        CreateMap<CreateStudentRequest, DbStudent>()
            .ForMember(db => db.Id, _ => Guid.NewGuid());
        CreateMap<CreateFacultyRequest, DbFaculty>();
        CreateMap<DbFaculty, GetFacultyResponse>();
        CreateMap<DbTeacher, CreateTeacherRequest>().ReverseMap();
    }
}
