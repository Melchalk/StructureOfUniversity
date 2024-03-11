using Business.Interfaces;
using ConsoleHelper.Interfaces;

namespace ConsoleHelper;

public class DeleteMenu : IDeleteMenu
{
    private readonly string DELETE_TEXT = "---- Delete action ----\nId of student: ";
    private readonly string SUCCESS_MESSAGE = "\nStudent with id = {0} was deleted";

    private IDeleteCommand _command;

    public DeleteMenu(IDeleteCommand command)
    {
        _command = command;
    }

    public async Task ShowMenu()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.Write(DELETE_TEXT);

        Console.ForegroundColor = ConsoleColor.White;

        var id = Guid.Parse(Console.ReadLine()!);

        await _command.ExecuteAsync(id);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(string.Format(SUCCESS_MESSAGE, id));

        Console.ReadLine();
    }
}