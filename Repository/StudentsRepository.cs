using DTOs;
using Repository.Interfaces;
using System.Text.Json;

namespace Repository;

public class StudentsRepository : IStudentsRepository
{
    private readonly string JSON_FILE = Directory.GetCurrentDirectory() + "/Students.json";

    private List<StudentDto>? _students;
    public List<StudentDto> Students
    {
        get
        {
            if (_students is null)
                _students = DeserializeJson();

            return _students;
        }
        set
        {
            _students = value;
        }
    }

    public async Task SaveAsync()
    {
        await File.WriteAllTextAsync(JSON_FILE, SerializeJson());
    }

    private List<StudentDto> DeserializeJson()
    {
        if (!File.Exists(JSON_FILE))
        {
            return new();
        }

        var jsonString = File.ReadAllText(JSON_FILE);

        var students = JsonSerializer.Deserialize<List<StudentDto>>(jsonString);

        return students is null
            ? new()
            : students;
    }

    private string SerializeJson()
    {
        return JsonSerializer.Serialize(Students);
    }
}
