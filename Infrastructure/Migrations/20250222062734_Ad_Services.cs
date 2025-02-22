using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ad_Services : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdService");

            migrationBuilder.DropTable(
                name: "AdServicePrice");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Ads");

            migrationBuilder.CreateTable(
                name: "AdCompanyServiceCost",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DailyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    AdOfferId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdCompanyServiceCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdCompanyServiceCost_AdOffers_AdOfferId",
                        column: x => x.AdOfferId,
                        principalTable: "AdOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdCompanyServiceCost_CompanyService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "CompanyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdHewarService",
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
                    table.PrimaryKey("PK_AdHewarService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdHewarService_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdHewarService_HewarService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "HewarService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdHewarServiceCost",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DailyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    AdOfferId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdHewarServiceCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdHewarServiceCost_AdOffers_AdOfferId",
                        column: x => x.AdOfferId,
                        principalTable: "AdOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdHewarServiceCost_HewarService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "HewarService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OtherAdService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    AdId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherAdService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherAdService_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherAdServiceCost",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DailyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    AdOfferId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherAdServiceCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherAdServiceCost_AdOffers_AdOfferId",
                        column: x => x.AdOfferId,
                        principalTable: "AdOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdCompanyServiceCost_AdOfferId",
                table: "AdCompanyServiceCost",
                column: "AdOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_AdCompanyServiceCost_ServiceId",
                table: "AdCompanyServiceCost",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdHewarService_AdId",
                table: "AdHewarService",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_AdHewarService_ServiceId",
                table: "AdHewarService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdHewarServiceCost_AdOfferId",
                table: "AdHewarServiceCost",
                column: "AdOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_AdHewarServiceCost_ServiceId",
                table: "AdHewarServiceCost",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherAdService_AdId",
                table: "OtherAdService",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherAdServiceCost_AdOfferId",
                table: "OtherAdServiceCost",
                column: "AdOfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdCompanyServiceCost");

            migrationBuilder.DropTable(
                name: "AdHewarService");

            migrationBuilder.DropTable(
                name: "AdHewarServiceCost");

            migrationBuilder.DropTable(
                name: "OtherAdService");

            migrationBuilder.DropTable(
                name: "OtherAdServiceCost");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Ads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AdService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_AdService_HewarService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "HewarService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdServicePrice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdOfferId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    DailyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdServicePrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdServicePrice_AdOffers_AdOfferId",
                        column: x => x.AdOfferId,
                        principalTable: "AdOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdServicePrice_HewarService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "HewarService",
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
                name: "IX_AdServicePrice_ServiceId",
                table: "AdServicePrice",
                column: "ServiceId");
        }
    }
}
