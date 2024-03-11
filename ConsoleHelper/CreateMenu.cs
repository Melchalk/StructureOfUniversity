using Business.Interfaces;
using ConsoleHelper.Interfaces;
using DTOs.Requests;

namespace ConsoleHelper;

public class CreateMenu : ICreateMenu
{
    private readonly string CREATE_TITLE = "---- Create action ----\nFill in the fields:\n";
    private readonly string SUCCESS_MESSAGE = "\nID of Student = {0}";

    private ICreateCommand _command;

    public CreateMenu(ICreateCommand command)
    {
        _command = command;
    }

    public async Task ShowMenu()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.Write(CREATE_TITLE);

        var request = new CreateStudentRequest()
        {
            Name = ReadField("Name"),
            Course = int.Parse(ReadField("Course")),
            University = ReadField("University")
        };

        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.WriteLine(string.Format(SUCCESS_MESSAGE, await _command.ExecuteAsync(request)));
        Console.ReadLine();
    }

    private string ReadField(string field)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.Write($"{field} - ");

        Console.ForegroundColor = ConsoleColor.White;

        return Console.ReadLine()!.Trim();
    }
}