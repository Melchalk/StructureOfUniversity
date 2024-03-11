using Business;
using Business.Interfaces;
using Repository;
using Repository.Interfaces;
using Validators;
using Validators.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();

builder.Services.AddTransient<IGetCommand, GetCommand>();
builder.Services.AddTransient<ICreateCommand, CreateCommand>();
builder.Services.AddTransient<IDeleteCommand, DeleteCommand>();
builder.Services.AddTransient<IUpdateCommand, UpdateCommand>();

builder.Services.AddTransient<ICreateStudentValidator, CreateStudentValidator>();
builder.Services.AddTransient<IUpdateStudentValidator, UpdateStudentValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
