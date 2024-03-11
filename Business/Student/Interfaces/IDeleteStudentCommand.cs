namespace Business.Student.Interfaces;

public interface IDeleteStudentCommand
{
    Task ExecuteAsync(Guid id);
}
