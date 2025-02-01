using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Dailymonthly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CostPerUnit",
                table: "PriceOfferService",
                newName: "MonthlyCostPerUnit");

            migrationBuilder.AddColumn<long>(
                name: "OfferId",
                table: "PriceRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DailyCostPerUnit",
                table: "PriceOfferService",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DailyCostPerUnit",
                table: "PriceOfferService");

            migrationBuilder.RenameColumn(
                name: "MonthlyCostPerUnit",
                table: "PriceOfferService",
                newName: "CostPerUnit");
        }
    }
}
