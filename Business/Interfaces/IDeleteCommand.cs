namespace Business.Interfaces;

public interface IDeleteCommand
{
    Task ExecuteAsync(Guid id);
}
