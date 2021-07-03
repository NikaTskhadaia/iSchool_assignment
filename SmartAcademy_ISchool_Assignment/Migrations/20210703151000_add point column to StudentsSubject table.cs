using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartAcademy_ISchool_Assignment.Migrations
{
    public partial class addpointcolumntoStudentsSubjecttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "StudentsToSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "StudentsToSubjects");
        }
    }
}
