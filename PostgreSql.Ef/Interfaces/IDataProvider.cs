using Microsoft.EntityFrameworkCore;
using StructureOfUniversity.DbModels;

namespace StructureOfUniversity.PostgreSql.Ef.Interfaces;

public interface IDataProvider : IBaseDataProvider
{
    DbSet<DbStudent> Students { get; set; }
    DbSet<DbTeacher> Teachers { get; set; }
    DbSet<DbFaculty> Faculties { get; set; }
    DbSet<DbLog> LogRecords { get; set; }
}
