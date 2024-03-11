using Business.Interfaces;
using ConsoleHelper.Interfaces;
using DTOs.Requests;

namespace ConsoleHelper;

public class UpdateMenu : IUpdateMenu
{
    private readonly string UPDATE_TITLE = "---- Update action ----\nFill in the fields:\n";
    private readonly string SUCCESS_MESSAGE = "\nStudent with id = {0} was update";

    private IUpdateCommand _command;

    public UpdateMenu(IUpdateCommand command)
    {
        _command = command;
    }

    public async Task ShowMenu()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.Write(UPDATE_TITLE);

        Console.Write("Id of student - ");
        Console.ForegroundColor = ConsoleColor.White;

        var request = new UpdateStudentRequest()
        {
            Id = Guid.Parse(Console.ReadLine()!),
            Name = ReadField("Name"),
            University = ReadField("University")
        };

        var course = ReadField("Course");
        request.Course = course is not null
            ? int.Parse(course)
            : null;

        await _command.ExecuteAsync(request);

        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.WriteLine(string.Format(SUCCESS_MESSAGE, request.Id));
        Console.ReadLine();
    }

    private string? ReadField(string field)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.Write($"{field} - ");

        Console.ForegroundColor = ConsoleColor.White;

        var value = Console.ReadLine();

        return value is null || value.Trim() == string.Empty
            ? null
            : value.Trim();
    }
}
