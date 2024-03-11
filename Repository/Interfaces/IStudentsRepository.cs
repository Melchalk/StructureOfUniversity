using DTOs;

namespace Repository.Interfaces;

public interface IStudentsRepository
{
    List<StudentDto> Students { get; set; }
    Task SaveAsync();
}