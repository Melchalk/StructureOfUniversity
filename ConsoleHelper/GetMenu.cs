using Business.Interfaces;
using ConsoleHelper.Interfaces;

namespace ConsoleHelper;

public class GetMenu : IGetMenu
{
    private readonly string GET_TEXT = "---- Get action ----\nId of student: ";
    private IGetCommand _command;

    public GetMenu(IGetCommand command)
    {
        _command = command;
    }

    public void ShowMenu()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.Write(GET_TEXT);

        Console.ForegroundColor = ConsoleColor.White;

        var id = Guid.Parse(Console.ReadLine()!);

        var student = _command.Execute(id);

        if (student is null)
        {
            throw new ArgumentException("Student with this id not found");
        }

        WriteField("Name", student.Name);
        WriteField("Course", student.Course.ToString());
        WriteField("University", student.University);

        Console.ReadLine();
    }

    private void WriteField(string field, string value)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.Write($"{field} - ");

        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine(value);
    }
}
