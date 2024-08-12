using StructureOfUniversity.DbModels;
using StructureOfUniversity.DTOs.Teacher.Requests;

namespace StructureOfUniversity.Auth.Services.Interfaces;

public interface IUserService
{
    Task<DbTeacher> RegisterUser(CreateTeacherRequest request);

    Task<DbTeacher> GetUserByPhone(string phone);
}
