using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAapplication.Migrations
{
    public partial class T : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "course",
                table: "Enrollment");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Enrollment",
                newName: "course_date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "course_date",
                table: "Enrollment",
                newName: "date");

            migrationBuilder.AddColumn<string>(
                name: "course",
                table: "Enrollment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
