using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Permissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Permission = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1L, null, null, "Admin", "ADMIN" },
                    { 2L, null, null, "Company", "COMPANY" },
                    { 3L, null, null, "Facility", "FACILITY" },
                    { 4L, null, null, "Guard", "GUARD" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "Permission", "RoleId" },
                values: new object[,]
                {
                    { 1L, 50, 1L },
                    { 2L, 51, 1L },
                    { 3L, 52, 1L },
                    { 4L, 53, 1L },
                    { 5L, 1, 1L },
                    { 6L, 2, 1L },
                    { 7L, 3, 1L },
                    { 8L, 4, 1L },
                    { 9L, 5, 1L },
                    { 10L, 6, 1L },
                    { 11L, 104, 2L },
                    { 12L, 102, 2L },
                    { 13L, 103, 2L },
                    { 14L, 151, 2L },
                    { 15L, 152, 2L },
                    { 16L, 153, 2L },
                    { 17L, 154, 2L },
                    { 18L, 100, 3L },
                    { 19L, 101, 3L },
                    { 20L, 104, 3L },
                    { 21L, 150, 3L },
                    { 22L, 151, 3L },
                    { 23L, 153, 3L },
                    { 24L, 154, 3L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4L);
        }
    }
}
