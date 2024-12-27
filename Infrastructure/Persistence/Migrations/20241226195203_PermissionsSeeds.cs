using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PermissionsSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permission",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permission",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Permission",
                value: 3);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Permission",
                value: 4);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Permission",
                value: 5);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Permission",
                value: 6);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Permission",
                value: 50);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Permission",
                value: 51);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Permission",
                value: 52);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Permission",
                value: 53);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 100, 1L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 101, 1L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 102, 1L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 103, 1L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 150, 1L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 151, 1L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 152, 1L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 153, 1L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permission",
                value: 50);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permission",
                value: 51);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Permission",
                value: 52);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Permission",
                value: 53);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Permission",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Permission",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Permission",
                value: 3);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Permission",
                value: 4);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Permission",
                value: 5);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Permission",
                value: 6);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 104, 2L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 102, 2L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 103, 2L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 151, 2L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 152, 2L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 153, 2L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 154, 2L });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "Permission", "RoleId" },
                values: new object[] { 100, 3L });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "Permission", "RoleId" },
                values: new object[,]
                {
                    { 19L, 101, 3L },
                    { 20L, 104, 3L },
                    { 21L, 150, 3L },
                    { 22L, 151, 3L },
                    { 23L, 153, 3L },
                    { 24L, 154, 3L }
                });
        }
    }
}
