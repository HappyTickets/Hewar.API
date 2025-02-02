using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Removeuselessfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdServicePrice_AdOffers_AdOfferId",
                table: "AdServicePrice");

            migrationBuilder.DropForeignKey(
                name: "FK_AdServicePrice_AdOffers_AdOfferId1",
                table: "AdServicePrice");

            migrationBuilder.DropIndex(
                name: "IX_AdServicePrice_AdOfferId1",
                table: "AdServicePrice");

            migrationBuilder.DropColumn(
                name: "AdOfferId1",
                table: "AdServicePrice");

            migrationBuilder.AddForeignKey(
                name: "FK_AdServicePrice_AdOffers_AdOfferId",
                table: "AdServicePrice",
                column: "AdOfferId",
                principalTable: "AdOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdServicePrice_AdOffers_AdOfferId",
                table: "AdServicePrice");

            migrationBuilder.AddColumn<long>(
                name: "AdOfferId1",
                table: "AdServicePrice",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdServicePrice_AdOfferId1",
                table: "AdServicePrice",
                column: "AdOfferId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AdServicePrice_AdOffers_AdOfferId",
                table: "AdServicePrice",
                column: "AdOfferId",
                principalTable: "AdOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdServicePrice_AdOffers_AdOfferId1",
                table: "AdServicePrice",
                column: "AdOfferId1",
                principalTable: "AdOffers",
                principalColumn: "Id");
        }
    }
}
