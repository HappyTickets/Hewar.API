using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Key",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DataType", "DeletedBy", "DeletedOn", "IsDeleted", "ModifiedBy", "ModifiedOn", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1L, null, null, 1, null, null, false, null, null, "ContractSignDate", null },
                    { 2L, null, null, 1, null, null, false, null, null, "ContractStartDate", null },
                    { 3L, null, null, 2, null, null, false, null, null, "CompanyNameAr", null },
                    { 4L, null, null, 2, null, null, false, null, null, "CompanyNameEn", null },
                    { 5L, null, null, 2, null, null, false, null, null, "CompanyMainOfficeCityAr", null },
                    { 6L, null, null, 2, null, null, false, null, null, "CompanyMainOfficeCityEn", null },
                    { 7L, null, null, 2, null, null, false, null, null, "CompanyCommercialRegistration", null },
                    { 8L, null, null, 2, null, null, false, null, null, "CompanyPublicSecurityLicense", null },
                    { 9L, null, null, 2, null, null, false, null, null, "CompanyTelephone", null },
                    { 10L, null, null, 2, null, null, false, null, null, "CompanyMobile", null },
                    { 11L, null, null, 2, null, null, false, null, null, "CompanyAddressCityAr", null },
                    { 12L, null, null, 2, null, null, false, null, null, "CompanyAddressCityEn", null },
                    { 13L, null, null, 2, null, null, false, null, null, "CompanyAddressPostalCode", null },
                    { 14L, null, null, 2, null, null, false, null, null, "CompanyAddressUnitNumber", null },
                    { 15L, null, null, 2, null, null, false, null, null, "CompanyAddressBuildingNumber", null },
                    { 16L, null, null, 2, null, null, false, null, null, "CompanyRegistrationInSabl", null },
                    { 17L, null, null, 2, null, null, false, null, null, "CompanyEmail", null },
                    { 18L, null, null, 2, null, null, false, null, null, "CompanyRepresentativeNameAr", null },
                    { 19L, null, null, 2, null, null, false, null, null, "CompanyRepresentativeNameEn", null },
                    { 20L, null, null, 2, null, null, false, null, null, "CompanyRepresentativeTitleAr", null },
                    { 21L, null, null, 2, null, null, false, null, null, "CompanyRepresentativeTitleEn", null },
                    { 22L, null, null, 0, null, null, false, null, null, "CompanyGuardsCount", null },
                    { 23L, null, null, 2, null, null, false, null, null, "FacilityNameAr", null },
                    { 24L, null, null, 2, null, null, false, null, null, "FacilityNameEn", null },
                    { 25L, null, null, 2, null, null, false, null, null, "FacilityMainOfficeCityAr", null },
                    { 26L, null, null, 2, null, null, false, null, null, "FacilityMainOfficeCityEn", null },
                    { 27L, null, null, 2, null, null, false, null, null, "FacilityCommercialRegistrationCityAr", null },
                    { 28L, null, null, 2, null, null, false, null, null, "FacilityCommercialRegistrationCityEn", null },
                    { 29L, null, null, 2, null, null, false, null, null, "FacilityMobile", null },
                    { 30L, null, null, 2, null, null, false, null, null, "FacilityAddressCityAr", null },
                    { 31L, null, null, 2, null, null, false, null, null, "FacilityAddressCityEn", null },
                    { 32L, null, null, 2, null, null, false, null, null, "FacilityAddressPostalCode", null },
                    { 33L, null, null, 2, null, null, false, null, null, "FacilityAddressUnitNumber", null },
                    { 34L, null, null, 2, null, null, false, null, null, "FacilityAddressBuildingNumber", null },
                    { 35L, null, null, 2, null, null, false, null, null, "FacilityEmail", null },
                    { 36L, null, null, 2, null, null, false, null, null, "FacilityRepresentativeNameAr", null },
                    { 37L, null, null, 2, null, null, false, null, null, "FacilityRepresentativeNameEn", null },
                    { 38L, null, null, 2, null, null, false, null, null, "FacilityRepresentativeTitleAr", null },
                    { 39L, null, null, 2, null, null, false, null, null, "FacilityRepresentativeTitleEn", null },
                    { 40L, null, null, 2, null, null, false, null, null, "FacilityLocationToBeSecuredAr", null },
                    { 41L, null, null, 2, null, null, false, null, null, "FacilityLocationToBeSecuredEn", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "Key",
                keyColumn: "Id",
                keyValue: 41L);
        }
    }
}
