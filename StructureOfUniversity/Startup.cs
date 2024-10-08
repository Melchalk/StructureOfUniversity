﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StructureOfUniversity.Auth.Helpers;
using StructureOfUniversity.Auth.Services;
using StructureOfUniversity.Auth.Services.Interfaces;
using StructureOfUniversity.Data;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.Domain;
using StructureOfUniversity.Domain.Interfaces;
using StructureOfUniversity.DTOs;
using StructureOfUniversity.DTOs.Enums;
using StructureOfUniversity.Infrastructure;
using StructureOfUniversity.Infrastructure.HealthCheck;
using StructureOfUniversity.Infrastructure.Logging;
using StructureOfUniversity.Infrastructure.Mapping;
using StructureOfUniversity.Infrastructure.Middlewares;
using StructureOfUniversity.Infrastructure.Swagger;
using StructureOfUniversity.PostgreSql.Ef;
using StructureOfUniversity.PostgreSql.Ef.Interfaces;
using StructureOfUniversity.Validators;
using StructureOfUniversity.Validators.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StructureOfUniversity;

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

        services.AddDbContext<StructureOfUniversityDbContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("SQLConnectionString"));
        });

        services.AddSingleton(new MapperConfiguration(mc =>
        {
            mc.AddProfile<MappingProfile>();
        }).CreateMapper());

        services.AddControllers();

        ConfigureDI(services);

        services.AddHttpContextAccessor();
        services.AddHttpClient();

        services
            .AddHealthChecks()
            .AddCheck<RequestTimeHealthCheck>("RequestTimeCheck");

        services.AddLogging(opt =>
        {
            opt.AddConsole();
            opt.AddFile(Directory.GetCurrentDirectory() + "/FileLogger.txt");
            opt.AddDatabase(services
                .BuildServiceProvider()!
                .GetRequiredService<IDataProvider>());
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddMemoryCache();
        services.AddStackExchangeRedisCache(options => {
            options.Configuration = "localhost";
            options.InstanceName = "local";
        });

        ConfigureJwt(services);
        ConfigureEnv(services);
    }

    private void ConfigureDI(IServiceCollection services)
    {
        services.AddScoped<IDataProvider, StructureOfUniversityDbContext>();
        services.AddScoped<IStudentsRepository, StudentsRepository>();
        services.AddScoped<IFacultiesRepository, FacultiesRepository>();
        services.AddScoped<ITeachersRepository, TeachersRepository>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IFacultyService, FacultyService>();

        services.AddTransient<ICreateStudentValidator, CreateStudentValidator>();
        services.AddTransient<IUpdateStudentValidator, UpdateStudentValidator>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseMiddleware<GlobalExceptionMiddleware>();

        UpdateDatabase(app);

        app.UseRouting();

        app.UseRewriter(
            new RewriteOptions()
                .Add(RewriterOptions.RewriteRequests)
        );
        app.UseStaticFiles();

        app.UseMiddleware<TokenMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors("CorsPolicy");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
    }

    private void ConfigureEnv(IServiceCollection services)
    {
        services.Configure<UniversityInfo>(opt =>
        {
            opt.Name = Environment.GetEnvironmentVariable("UNIVERSITY_NAME")!;
        });
    }

    private void ConfigureJwt(IServiceCollection services)
    {
        services.Configure<TokenSettings>(Configuration.GetSection("TokenSettings"));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetSection("TokenSettings:TokenIssuer").Value,
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetSection("TokenSettings:TokenAudience").Value,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SigningSymmetricKey.GetKey()
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("More important than the assistant", policy =>
            {
                policy.RequireRole(
                    TeachingPositions.Teacher.ToString(),
                    TeachingPositions.SeniorLecturer.ToString());
            });
    }

    private void UpdateDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider
            .GetService<StructureOfUniversityDbContext>();

        context!.Database.Migrate();
    }
}