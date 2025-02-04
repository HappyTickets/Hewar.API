using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class pricerequestOfferslist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceRequests_PriceRequestOffers_OfferId",
                table: "PriceRequests");

            migrationBuilder.DropIndex(
                name: "IX_PriceRequests_OfferId",
                table: "PriceRequests");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "PriceRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "PriceRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "PriceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OfferId",
                table: "PriceRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceRequests_OfferId",
                table: "PriceRequests",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceRequests_PriceRequestOffers_OfferId",
                table: "PriceRequests",
                column: "OfferId",
                principalTable: "PriceRequestOffers",
                principalColumn: "Id");
        }
    }
}
