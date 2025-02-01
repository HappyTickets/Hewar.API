using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CompanyServicesProvided : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyService_Companies_CompanyId",
                table: "CompanyService");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "CompanyService",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CompanyService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "CompanyService",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "CompanyService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "CompanyService",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CompanyService",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "CompanyService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedOn",
                table: "CompanyService",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "CompanyService",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyService_Companies_CompanyId",
                table: "CompanyService",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyService_Companies_CompanyId",
                table: "CompanyService");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CompanyService");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CompanyService");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "CompanyService");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CompanyService");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CompanyService");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "CompanyService");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CompanyService");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "CompanyService");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "CompanyService",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyService_Companies_CompanyId",
                table: "CompanyService",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
