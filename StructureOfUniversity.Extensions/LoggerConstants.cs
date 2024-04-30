using StructureOfUniversity.DbModels;

namespace StructureOfUniversity.Logging;

public static class LoggerConstants
{
    public const string SUCCESSFUL_ACCESS = "There was a successful access to the table {0}.";

    public static readonly string SUCCESSFUL_ACCESS_STUDENTS = string.Format(SUCCESSFUL_ACCESS, DbStudent.TableName);
    public static readonly string SUCCESSFUL_ACCESS_TEACHERS = string.Format(SUCCESSFUL_ACCESS, DbTeacher.TableName);
    public static readonly string SUCCESSFUL_ACCESS_FACULTIES = string.Format(SUCCESSFUL_ACCESS, DbFaculty.TableName);
}