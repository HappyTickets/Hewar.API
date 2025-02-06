using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntitesTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AudienceType",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IssuerType",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RepresentedEntity",
                table: "TicketMessages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AudienceType",
                table: "Chat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IssuerType",
                table: "Chat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_SenderId",
                table: "TicketMessages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMessages_Users_SenderId",
                table: "TicketMessages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketMessages_Users_SenderId",
                table: "TicketMessages");

            migrationBuilder.DropIndex(
                name: "IX_TicketMessages_SenderId",
                table: "TicketMessages");

            migrationBuilder.DropColumn(
                name: "AudienceType",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IssuerType",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RepresentedEntity",
                table: "TicketMessages");

            migrationBuilder.DropColumn(
                name: "AudienceType",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "IssuerType",
                table: "Chat");
        }
    }
}
