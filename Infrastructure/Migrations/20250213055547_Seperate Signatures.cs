using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeperateSignatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "PartyOneSignature",
                table: "ContractTemplate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PartyTwoSignature",
                table: "ContractTemplate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractTemplate_PriceRequestOffers_OfferId",
                table: "ContractTemplate");

            migrationBuilder.DropColumn(
                name: "PartyOneSignature",
                table: "ContractTemplate");

            migrationBuilder.DropColumn(
                name: "PartyTwoSignature",
                table: "ContractTemplate");
        }
    }
}
