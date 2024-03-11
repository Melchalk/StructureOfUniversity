using ConsoleHelper.Interfaces;
using DTOs;

namespace ConsoleHelper;

public class MainMenu : IMainMenu
{
    private readonly string CHOOSE_TEXT = "---- Choose action ----\n" +
        $" {CommandEnum.Create} - {(int)CommandEnum.Create}\n" +
        $" {CommandEnum.Get} - {(int)CommandEnum.Get}\n" +
        $" {CommandEnum.Update} - {(int)CommandEnum.Update}\n" +
        $" {CommandEnum.Delete} - {(int)CommandEnum.Delete}\n" +
        $" {CommandEnum.Exit} - {(int)CommandEnum.Exit}\n\n" +
        "-----------------------\n" +
        "Your choice: ";

    private readonly string ERROR = "\n{0}. Try again";

    public int ChooseMenu()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.Write(CHOOSE_TEXT);

        Console.ForegroundColor = ConsoleColor.White;

        return int.Parse(Console.ReadLine()!);
    }

    public void WrongChoose(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        Console.Write(string.Format(ERROR, error));

        Console.ReadLine();

        Console.Clear();

        Console.ForegroundColor = ConsoleColor.White;
    }
}
