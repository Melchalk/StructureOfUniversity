using Business.Student;
using Business.Student.Interfaces;
using Data;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using PostgreSql.Ef;
using PostgreSql.Ef.Interfaces;
using Validators;
using Validators.Interfaces;

namespace StudentService;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

                options.AddPolicy("GetPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .WithMethods("GET")
                        .AllowAnyHeader());
            });

        services.AddDbContext<StudentServiceDbContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("SQLConnectionString"));
        });

        services.AddControllers();

        services.AddScoped<IDataProvider, StudentServiceDbContext>();
        services.AddScoped<IStudentsRepository, StudentsRepository>();

        services.AddTransient<IGetStudentCommand, GetStudentCommand>();
        services.AddTransient<ICreateStudentCommand, CreateStudentCommand>();
        services.AddTransient<IDeleteStudentCommand, DeleteStudentCommand>();
        services.AddTransient<IUpdateStudentCommand, UpdateStudentCommand>();

        services.AddTransient<ICreateStudentValidator, CreateStudentValidator>();
        services.AddTransient<IUpdateStudentValidator, UpdateStudentValidator>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        UpdateDatabase(app);

        app.UseRouting();

        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private void UpdateDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider
            .GetService<StudentServiceDbContext>();

        context!.Database.Migrate();
    }
}