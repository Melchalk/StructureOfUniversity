using DbModels;
using Microsoft.EntityFrameworkCore;

namespace PostgreSql.Ef.Interfaces;

public interface IDataProvider : IBaseDataProvider
{
    DbSet<DbStudent> Students { get; set; }
}
