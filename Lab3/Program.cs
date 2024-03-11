using ConsoleHelper.Interfaces;
using DTOs;
using Lab3;
using Microsoft.Extensions.DependencyInjection;

var builder = new ServiceCollection();

Startup.ConfigureServices(builder);

var provider = builder.BuildServiceProvider();
var mainMenu = provider.GetService<IMainMenu>();

while (true)
{
    try
    {
        var selectedMenu = (CommandEnum)mainMenu!.ChooseMenu();

        switch (selectedMenu)
        {
            case CommandEnum.Create:
                var createMenu = provider.GetService<ICreateMenu>();
                await createMenu!.ShowMenu();
                break;

            case CommandEnum.Get:
                var getMenu = provider.GetService<IGetMenu>();
                getMenu!.ShowMenu();
                break;

            case CommandEnum.Update:
                var updateMenu = provider.GetService<IUpdateMenu>();
                await updateMenu!.ShowMenu();
                break;

            case CommandEnum.Delete:
                var deleteMenu = provider.GetService<IDeleteMenu>();
                await deleteMenu!.ShowMenu();
                break;

            case CommandEnum.Exit:
                return;
        }
    }
    catch(Exception ex)
    {
        mainMenu.WrongChoose(ex.Message);
    }
}