using Business.Interfaces;
using DTOs.Response;
using Repository.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Business;

public class GetCommand : IGetCommand
{
    private IStudentsRepository _repository;

    public GetCommand(IStudentsRepository repository)
    {
        _repository = repository;
    }

    public GetStudentResponse? Execute(Guid id)
    {
        var student = _repository.Students.FirstOrDefault(x => x.Id == id);

        return student is null
            ? null
            : new()
            {
                Name = student.Name,
                Course = student.Course,
                University = student.University,
            };
    }
    /*
    public async Task<IResult> GetFile()
    {
        string path = Directory.GetCurrentDirectory() + "/Students.json";
        byte[] fileContent = await File.ReadAllBytesAsync(path);
        string contentType = ".json";
        string downloadName = "Students.json";

        return Results.File(fileContent, contentType, downloadName);
    }*/
}
