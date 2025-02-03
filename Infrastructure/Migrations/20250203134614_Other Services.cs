using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OtherServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceOfferService");

            migrationBuilder.DropTable(
                name: "PriceRequestService");

            migrationBuilder.CreateTable(
                name: "OtherRequestedService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    PriceRequestId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherRequestedService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherRequestedService_PriceRequests_PriceRequestId",
                        column: x => x.PriceRequestId,
                        principalTable: "PriceRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OtherServiceOffer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    DailyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceOfferId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherServiceOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherServiceOffer_PriceRequestOffers_PriceOfferId",
                        column: x => x.PriceOfferId,
                        principalTable: "PriceRequestOffers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceOffer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceOfferId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DailyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceOffer_CompanyService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "CompanyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceOffer_PriceRequestOffers_PriceOfferId",
                        column: x => x.PriceOfferId,
                        principalTable: "PriceRequestOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequest",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_CompanyService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "CompanyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_PriceRequests_PriceRequestId",
                        column: x => x.PriceRequestId,
                        principalTable: "PriceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtherRequestedService_PriceRequestId",
                table: "OtherRequestedService",
                column: "PriceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherServiceOffer_PriceOfferId",
                table: "OtherServiceOffer",
                column: "PriceOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOffer_PriceOfferId",
                table: "ServiceOffer",
                column: "PriceOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOffer_ServiceId",
                table: "ServiceOffer",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_PriceRequestId",
                table: "ServiceRequest",
                column: "PriceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ServiceId",
                table: "ServiceRequest",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherRequestedService");

            migrationBuilder.DropTable(
                name: "OtherServiceOffer");

            migrationBuilder.DropTable(
                name: "ServiceOffer");

            migrationBuilder.DropTable(
                name: "ServiceRequest");

            migrationBuilder.CreateTable(
                name: "PriceOfferService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceOfferId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    DailyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceOfferService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceOfferService_CompanyService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "CompanyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceOfferService_PriceRequestOffers_PriceOfferId",
                        column: x => x.PriceOfferId,
                        principalTable: "PriceRequestOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceRequestService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRequestService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceRequestService_CompanyService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "CompanyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceRequestService_PriceRequests_PriceRequestId",
                        column: x => x.PriceRequestId,
                        principalTable: "PriceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceOfferService_PriceOfferId",
                table: "PriceOfferService",
                column: "PriceOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceOfferService_ServiceId",
                table: "PriceOfferService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceRequestService_PriceRequestId",
                table: "PriceRequestService",
                column: "PriceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceRequestService_ServiceId",
                table: "PriceRequestService",
                column: "ServiceId");
        }
    }
}
