using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GuardAdditionalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Skills",
                table: "Guards",
                newName: "NationalId");

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "Guards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "City",
                table: "Guards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Guards",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Qualification",
                table: "Guards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Guards",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrevCompany",
                columns: table => new
                {
                    GuardId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    To = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrevCompany", x => new { x.GuardId, x.Id });
                    table.ForeignKey(
                        name: "FK_PrevCompany_Guards_GuardId",
                        column: x => x.GuardId,
                        principalTable: "Guards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    GuardId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => new { x.GuardId, x.Id });
                    table.ForeignKey(
                        name: "FK_Skill_Guards_GuardId",
                        column: x => x.GuardId,
                        principalTable: "Guards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrevCompany");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Guards");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Guards");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Guards");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "Guards");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Guards");

            migrationBuilder.RenameColumn(
                name: "NationalId",
                table: "Guards",
                newName: "Skills");
        }
    }
}
