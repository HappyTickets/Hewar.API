using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ar_En_Fields_Entries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShiftTime",
                table: "ScheduleEntry",
                newName: "ShiftTimeEn");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "ScheduleEntry",
                newName: "NotesEn");

            migrationBuilder.AddColumn<string>(
                name: "NotesAr",
                table: "ScheduleEntry",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShiftTimeAr",
                table: "ScheduleEntry",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotesAr",
                table: "ScheduleEntry");

            migrationBuilder.DropColumn(
                name: "ShiftTimeAr",
                table: "ScheduleEntry");

            migrationBuilder.RenameColumn(
                name: "ShiftTimeEn",
                table: "ScheduleEntry",
                newName: "ShiftTime");

            migrationBuilder.RenameColumn(
                name: "NotesEn",
                table: "ScheduleEntry",
                newName: "Notes");
        }
    }
}
