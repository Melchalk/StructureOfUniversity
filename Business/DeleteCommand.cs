using Business.Interfaces;
using Repository.Interfaces;

namespace Business;

public class DeleteCommand : IDeleteCommand
{
    private IStudentsRepository _repository;

    public DeleteCommand(IStudentsRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var student = _repository.Students
            .FirstOrDefault(x => x.Id == id)
            ?? throw new ArgumentException("Student with this id not found");

        _repository.Students.Remove(student);

        await _repository.SaveAsync();
    }
}