using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class foriegnkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractKey_Contracts_ContractKey",
                table: "ContractKey");

            migrationBuilder.DropIndex(
                name: "IX_ContractKey_ContractKey",
                table: "ContractKey");

            migrationBuilder.DropColumn(
                name: "ContractKey",
                table: "ContractKey");

            migrationBuilder.CreateIndex(
                name: "IX_ContractKey_ContractId",
                table: "ContractKey",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractKey_Contracts_ContractId",
                table: "ContractKey",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractKey_Contracts_ContractId",
                table: "ContractKey");

            migrationBuilder.DropIndex(
                name: "IX_ContractKey_ContractId",
                table: "ContractKey");

            migrationBuilder.AddColumn<long>(
                name: "ContractKey",
                table: "ContractKey",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ContractKey_ContractKey",
                table: "ContractKey",
                column: "ContractKey");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractKey_Contracts_ContractKey",
                table: "ContractKey",
                column: "ContractKey",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
