using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Asset.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingActionTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AssetActionTypes",
                columns: new[] { "ActionTypeId", "Description", "Name" },
                values: new object[,]
                {
                    { 5, "Hardware component added to asset", "Hardware Addition" },
                    { 6, "Hardware component removed from asset", "Hardware Removal" },
                    { 7, "Asset returned to IT department", "Return to IT" },
                    { 8, "Asset permanently retired from service", "Retired" },
                    { 9, "Asset transferred to different location", "Transfer" },
                    { 10, "Asset repair work performed", "Repair" },
                    { 11, "Asset upgraded with new components", "Upgrade" },
                    { 12, "Asset deployed to user", "Deployment" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AssetActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AssetActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AssetActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AssetActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AssetActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AssetActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AssetActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AssetActionTypes",
                keyColumn: "ActionTypeId",
                keyValue: 12);
        }
    }
}
