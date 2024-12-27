using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTicketAudience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_PriceRequests_PriceRequestId",
                table: "Tickets");

            migrationBuilder.AlterColumn<long>(
                name: "PriceRequestId",
                table: "Tickets",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "AudienceId",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "AudienceType",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "IssuerId",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "IssuerType",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_PriceRequests_PriceRequestId",
                table: "Tickets",
                column: "PriceRequestId",
                principalTable: "PriceRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_PriceRequests_PriceRequestId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AudienceId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AudienceType",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IssuerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IssuerType",
                table: "Tickets");

            migrationBuilder.AlterColumn<long>(
                name: "PriceRequestId",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_PriceRequests_PriceRequestId",
                table: "Tickets",
                column: "PriceRequestId",
                principalTable: "PriceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
