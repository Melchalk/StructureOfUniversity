using DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PostgreSql.Ef.Migrations;

[DbContext(typeof(StudentServiceDbContext))]
[Migration("20240310002811_InitialTables")]
class InitialTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
        name: DbStudent.TableName,
        columns: table => new
        {
            Id = table.Column<Guid>(nullable: false),
            Name = table.Column<string>(nullable: false),
            Course = table.Column<int>(nullable: false),
            University = table.Column<string>(nullable: false)
        },
        constraints: table => { table.PrimaryKey("PK_Students", x => x.Id); }
        );
    }
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(DbStudent.TableName);
    }
}
