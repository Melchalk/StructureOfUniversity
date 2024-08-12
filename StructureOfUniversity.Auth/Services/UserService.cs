using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StructureOfUniversity.Auth.Helpers;
using StructureOfUniversity.Auth.Services.Interfaces;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.DTOs.Teacher.Requests;
using StructureOfUniversity.Logging;
using StructureOfUniversity.Models.Exceptions;
using System.Text;

namespace StructureOfUniversity.Auth.Services;

public class UserService : IUserService
{
    private const string START_PHONE = "+7";

    private readonly ITeachersRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IMemoryCache _cache;

    public UserService(
        ITeachersRepository repository,
        IMapper mapper,
        ILogger<UserService> logger,
        IMemoryCache memoryCache)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _cache = memoryCache;
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
            throw new BadRequestException("User with this phone already exists.");
        }

        request.Phone = phoneChecked;

        string salt = $"{Guid.NewGuid()}{Guid.NewGuid()}";

        var dbUser = _mapper.Map<DbTeacher>(request);

        dbUser.Salt = salt;
        dbUser.Password = PasswordHelper.GetPasswordHash(dbUser.Phone, dbUser.Password, salt);

        await _repository.CreateAsync(dbUser);

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_TEACHERS);

        return dbUser;
    }

    public async Task<DbTeacher> GetUserByPhone(string phone)
    {
        _cache.TryGetValue(phone, out DbTeacher? user);

        if (user is null)
        {
            user = await _repository.GetTeachers()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Phone == phone);

            if (user is not null)
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                };

                var callbackRegistration = new PostEvictionCallbackRegistration
                {
                    EvictionCallback = (object key, object? value, EvictionReason reason, object? state)
                    => Console.WriteLine($"Cache for user with phone = '{phone}' was old")
                };
                cacheOptions.PostEvictionCallbacks.Add(callbackRegistration);

                _cache.Set(phone, user, cacheOptions);
            }
        }

        return user?? throw new BadRequestException($"User with phone {phone} is not found.");
    }
}
