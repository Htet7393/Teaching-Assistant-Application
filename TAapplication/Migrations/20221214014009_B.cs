using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAapplication.Migrations
{
    public partial class B : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Availability",
                newName: "Wednesday");

            migrationBuilder.RenameColumn(
                name: "Available",
                table: "Availability",
                newName: "Tuesday");

            migrationBuilder.AddColumn<string>(
                name: "Friday",
                table: "Availability",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Monday",
                table: "Availability",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Thursday",
                table: "Availability",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friday",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "Monday",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "Thursday",
                table: "Availability");

            migrationBuilder.RenameColumn(
                name: "Wednesday",
                table: "Availability",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Tuesday",
                table: "Availability",
                newName: "Available");
        }
    }
}
