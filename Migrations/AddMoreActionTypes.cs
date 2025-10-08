using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddMoreActionTypes : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "AssetActionTypes",
            columns: new[] { "ActionTypeId", "Name", "Description" },
            values: new object[,]
            {
                { 5, "Hardware Addition", "New hardware component added to asset" },
                { 6, "Hardware Removal", "Hardware component removed from asset" },
                { 7, "Return to IT", "Asset returned to IT department" },
                { 8, "Retired", "Asset permanently retired from service" },
                { 9, "Transfer", "Asset transferred between locations" },
                { 10, "Repair", "Asset sent for repair" },
                { 11, "Upgrade", "Asset upgraded with new components" },
                { 12, "Deployment", "Asset deployed to user" }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AssetActionTypes",
            keyColumn: "ActionTypeId",
            keyValues: new object[] { 5, 6, 7, 8, 9, 10, 11, 12 });
    }
}