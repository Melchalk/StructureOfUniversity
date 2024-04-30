using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using StructureOfUniversity.DbModels;

namespace StructureOfUniversity.PostgreSql.Ef.Migrations;

[DbContext(typeof(StructureOfUniversityDbContext))]
[Migration("202404181210_AddLogs")]
public class AddLogs : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        var sequenceLogs = "generate_log_number_seq";
        migrationBuilder.Sql($"CREATE SEQUENCE {sequenceLogs} START WITH 1 INCREMENT BY 1;");


        migrationBuilder.CreateTable(
            name: DbLog.TableName,
            columns: table => new
            {
                Number = table.Column<int>(nullable: false, defaultValueSql: $"nextval('{sequenceLogs}')"),
                Level = table.Column<LogLevel>(nullable: false),
                Message = table.Column<string>(nullable: false),
                Time = table.Column<DateTime>(nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Logs", x => x.Number);
            }
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(DbLog.TableName);
    }
}
