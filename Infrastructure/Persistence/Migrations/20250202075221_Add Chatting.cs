using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChatting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdOffers_Chat_ChatId",
                table: "AdOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyService_Companies_CompanyId",
                table: "CompanyService");

            migrationBuilder.DropColumn(
                name: "Offer",
                table: "AdOffers");

            migrationBuilder.AlterColumn<long>(
                name: "ChatId",
                table: "AdOffers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "AdService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    AdId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdService_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdService_CompanyService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "CompanyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdServicePrice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DailyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    AdOfferId = table.Column<long>(type: "bigint", nullable: false),
                    AdOfferId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdServicePrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdServicePrice_AdOffers_AdOfferId",
                        column: x => x.AdOfferId,
                        principalTable: "AdOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdServicePrice_AdOffers_AdOfferId1",
                        column: x => x.AdOfferId1,
                        principalTable: "AdOffers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdServicePrice_CompanyService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "CompanyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdService_AdId",
                table: "AdService",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_AdService_ServiceId",
                table: "AdService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdServicePrice_AdOfferId",
                table: "AdServicePrice",
                column: "AdOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_AdServicePrice_AdOfferId1",
                table: "AdServicePrice",
                column: "AdOfferId1");

            migrationBuilder.CreateIndex(
                name: "IX_AdServicePrice_ServiceId",
                table: "AdServicePrice",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdOffers_Chat_ChatId",
                table: "AdOffers",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyService_Companies_CompanyId",
                table: "CompanyService",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdOffers_Chat_ChatId",
                table: "AdOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyService_Companies_CompanyId",
                table: "CompanyService");

            migrationBuilder.DropTable(
                name: "AdService");

            migrationBuilder.DropTable(
                name: "AdServicePrice");

            migrationBuilder.AlterColumn<long>(
                name: "ChatId",
                table: "AdOffers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Offer",
                table: "AdOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AdOffers_Chat_ChatId",
                table: "AdOffers",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyService_Companies_CompanyId",
                table: "CompanyService",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
