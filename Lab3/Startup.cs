using Business;
using Business.Interfaces;
using ConsoleHelper;
using ConsoleHelper.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Interfaces;
using Validators;
using Validators.Interfaces;

namespace Lab3;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IStudentsRepository, StudentsRepository>();

        services.AddTransient<IGetCommand, GetCommand>();
        services.AddTransient<ICreateCommand, CreateCommand>();
        services.AddTransient<IDeleteCommand, DeleteCommand>();
        services.AddTransient<IUpdateCommand, UpdateCommand>();

        services.AddTransient<ICreateStudentValidator, CreateStudentValidator>();
        services.AddTransient<IUpdateStudentValidator, UpdateStudentValidator>();

        services.AddTransient<IGetMenu, GetMenu>();
        services.AddTransient<ICreateMenu, CreateMenu>();
        services.AddTransient<IDeleteMenu, DeleteMenu>();
        services.AddTransient<IUpdateMenu, UpdateMenu>();
        services.AddTransient<IMainMenu, MainMenu>();
    }
}
