using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PriceRequest_Tickets_Notifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ClosedDate",
                table: "Tickets",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "OpenedDate",
                table: "Tickets",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "PriceRequestId",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceId = table.Column<long>(type: "bigint", nullable: false),
                    ReferenceType = table.Column<int>(type: "int", nullable: false),
                    Event = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RecipientId = table.Column<long>(type: "bigint", nullable: false),
                    RecipientType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecurityRole = table.Column<int>(type: "int", nullable: false),
                    GuardsCount = table.Column<int>(type: "int", nullable: false),
                    WorkShift = table.Column<int>(type: "int", nullable: false),
                    ContractType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FacilityId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceRequests_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceRequests_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    SenderType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketMessages_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceRequestFacilityDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilitySize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityActivityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityCommercialRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityCommercialRegistrationExpiryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FacilityLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityLicenseExpiryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FacilityNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentativeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentativePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentativeNationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentativeNationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentativeNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommissionerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommissionerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommissionerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommissionerNationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommissionerNationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommissionerNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceRequestId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRequestFacilityDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceRequestFacilityDetails_PriceRequests_PriceRequestId",
                        column: x => x.PriceRequestId,
                        principalTable: "PriceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceRequestResponses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RespondedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PriceRequestId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRequestResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceRequestResponses_PriceRequests_PriceRequestId",
                        column: x => x.PriceRequestId,
                        principalTable: "PriceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    TicketMessageId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => new { x.TicketMessageId, x.Id });
                    table.ForeignKey(
                        name: "FK_Media_TicketMessages_TicketMessageId",
                        column: x => x.TicketMessageId,
                        principalTable: "TicketMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PriceRequestId",
                table: "Tickets",
                column: "PriceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceRequestFacilityDetails_PriceRequestId",
                table: "PriceRequestFacilityDetails",
                column: "PriceRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceRequestResponses_PriceRequestId",
                table: "PriceRequestResponses",
                column: "PriceRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceRequests_CompanyId",
                table: "PriceRequests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceRequests_FacilityId",
                table: "PriceRequests",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_TicketId",
                table: "TicketMessages",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_PriceRequests_PriceRequestId",
                table: "Tickets",
                column: "PriceRequestId",
                principalTable: "PriceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_PriceRequests_PriceRequestId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PriceRequestFacilityDetails");

            migrationBuilder.DropTable(
                name: "PriceRequestResponses");

            migrationBuilder.DropTable(
                name: "TicketMessages");

            migrationBuilder.DropTable(
                name: "PriceRequests");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PriceRequestId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "OpenedDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "PriceRequestId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tickets");
        }
    }
}
