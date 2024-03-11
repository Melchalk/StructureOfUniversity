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
            .FirstOrDefault(x => x.Id == id);

        if (student is null)
        {
            return;
        }

        _repository.Students.Remove(student);

        await _repository.SaveAsync();
    }
}