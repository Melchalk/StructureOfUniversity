using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StructureOfUniversity.Auth.Helpers;
using StructureOfUniversity.Auth.Services.Interfaces;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.DTOs.Teacher.Requests;
using System.Text;

namespace StructureOfUniversity.Auth.Services;

public class UserService : IUserService
{
    private const string START_PHONE = "+7";

    private readonly ITeachersRepository _repository;
    private readonly IMapper _mapper;

    public UserService(
        ITeachersRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DbTeacher> RegisterUser(CreateTeacherRequest request)
    {
        StringBuilder phone = new();
        phone.Append(START_PHONE);

        request.Phone = request.Phone.StartsWith(START_PHONE)
            ? request.Phone[START_PHONE.Length..]
            : request.Phone[(START_PHONE.Length - 1)..];

        foreach (var symbol in request.Phone)
        {
            if (char.IsDigit(symbol))
            {
                phone.Append(symbol);
            }
        }

        var phoneChecked = phone.ToString();

        if (await _repository.GetTeachers().AnyAsync(x => string.Equals(x.Phone, phoneChecked)))
        {
            throw new ArgumentException("User with this phone already exists.");
        }

        request.Phone = phoneChecked;

        string salt = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        var dbUser = _mapper.Map<DbTeacher>(request);

        dbUser.Salt = salt;
        dbUser.Password = PasswordHelper.GetPasswordHash(dbUser.Phone, dbUser.Password, salt);

        await _repository.CreateAsync(dbUser);

        return dbUser;
    }

    public async Task<DbTeacher> GetUserByPhone(string? phone)
    {
        var user = phone is null
            ? null
            : await _repository.GetTeachers()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Phone == phone);

        return user is null
            ? throw new ArgumentException($"User with phone {phone} is not found.")
            : user;
    }
}
