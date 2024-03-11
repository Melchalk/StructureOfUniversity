using Business.Student.Interfaces;
using Data.Interfaces;

namespace Business.Student;

public class DeleteStudentCommand : IDeleteStudentCommand
{
    private IStudentsRepository _repository;

    public DeleteStudentCommand(IStudentsRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var student = await _repository.GetAsync(id)
            ?? throw new ArgumentException("Student with this id not found");

        await _repository.DeleteAsync(student);
    }
}