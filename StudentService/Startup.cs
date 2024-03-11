using Business.Interfaces;
using Business;
using Repository.Interfaces;
using Repository;
using Validators.Interfaces;
using Validators;

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

        services.AddControllers();

        services.AddScoped<IStudentsRepository, StudentsRepository>();

        services.AddTransient<IGetCommand, GetCommand>();
        services.AddTransient<ICreateCommand, CreateCommand>();
        services.AddTransient<IDeleteCommand, DeleteCommand>();
        services.AddTransient<IUpdateCommand, UpdateCommand>();

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

        app.UseRouting();

        app.UseCors("GetPolicy");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}