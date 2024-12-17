using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserNameFirstLastNameType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Guards",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Facilities",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Companies",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Guards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Guards");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Guards",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Facilities",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Companies",
                newName: "Name");
        }
    }
}
