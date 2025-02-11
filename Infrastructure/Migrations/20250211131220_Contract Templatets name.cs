using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContractTemplatetsname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferContracts_PriceRequestOffers_OfferId",
                table: "OfferContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferContracts",
                table: "OfferContracts");

            migrationBuilder.RenameTable(
                name: "OfferContracts",
                newName: "ContractTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_OfferContracts_OfferId",
                table: "ContractTemplate",
                newName: "IX_ContractTemplate_OfferId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractTemplate",
                table: "ContractTemplate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTemplate_PriceRequestOffers_OfferId",
                table: "ContractTemplate",
                column: "OfferId",
                principalTable: "PriceRequestOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractTemplate_PriceRequestOffers_OfferId",
                table: "ContractTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractTemplate",
                table: "ContractTemplate");

            migrationBuilder.RenameTable(
                name: "ContractTemplate",
                newName: "OfferContracts");

            migrationBuilder.RenameIndex(
                name: "IX_ContractTemplate_OfferId",
                table: "OfferContracts",
                newName: "IX_OfferContracts_OfferId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferContracts",
                table: "OfferContracts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferContracts_PriceRequestOffers_OfferId",
                table: "OfferContracts",
                column: "OfferId",
                principalTable: "PriceRequestOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
