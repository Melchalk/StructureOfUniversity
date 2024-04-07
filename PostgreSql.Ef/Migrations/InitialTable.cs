using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.DTOs.Enums;

namespace StructureOfUniversity.PostgreSql.Ef.Migrations;

[DbContext(typeof(StructureOfUniversityDbContext))]
[Migration("20240317191711_InitialTables")]
class InitialTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        #region Sequence

        var sequenceFaculty = "generate_faculty_number_seq";
        migrationBuilder.Sql($"CREATE SEQUENCE {sequenceFaculty} START WITH 1 INCREMENT BY 1;");

        #endregion

        #region Faculty

        migrationBuilder.CreateTable(
            name: DbFaculty.TableName,
            columns: table => new
            {
                Number = table.Column<int>(nullable: false, defaultValueSql: $"nextval('{sequenceFaculty}')"),
                Name = table.Column<string>(nullable: false),
                DeanName = table.Column<string>(nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Faculties", x => x.Number);
            }
        );

        #endregion

        #region Student

        migrationBuilder.CreateTable(
            name: DbStudent.TableName,
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                Name = table.Column<string>(nullable: false),
                Course = table.Column<int>(nullable: false),
                FacultyNumber = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Students", x => x.Id);

                table.ForeignKey(
                    "FK_Students_Faculty",
                    x => x.FacultyNumber,
                    DbFaculty.TableName);
            }
        );

        #endregion

        #region Teacher

        migrationBuilder.CreateTable(
            name: DbTeacher.TableName,
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                Name = table.Column<string>(nullable: false),
                Position = table.Column<TeachingPositions>(nullable: false),
                Phone = table.Column<string>(nullable: false),
                Password = table.Column<string>(nullable: false),
                Salt = table.Column<string>(nullable: false),
                FacultyNumber = table.Column<int>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Teachers", x => x.Id);

                table.ForeignKey(
                    "FK_Teachers_Faculty",
                    x => x.FacultyNumber,
                    DbFaculty.TableName);
            }
        );

        #endregion
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(DbStudent.TableName);
        migrationBuilder.DropTable(DbFaculty.TableName);
        migrationBuilder.DropTable(DbTeacher.TableName);
    }
}
