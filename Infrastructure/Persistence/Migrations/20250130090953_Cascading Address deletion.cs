using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CascadingAddressdeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Facilities_AddressId",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AddressId",
                table: "Companies");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_AddressId",
                table: "Facilities",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddressId",
                table: "Companies",
                column: "AddressId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Facilities_AddressId",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AddressId",
                table: "Companies");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_AddressId",
                table: "Facilities",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddressId",
                table: "Companies",
                column: "AddressId");
        }
    }
}
