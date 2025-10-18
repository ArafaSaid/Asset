using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Asset.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftwareAndConsumables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consumables",
                columns: table => new
                {
                    ConsumableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    MinimumStockLevel = table.Column<int>(type: "int", nullable: false),
                    ReorderQuantity = table.Column<int>(type: "int", nullable: true),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastOrderedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumables", x => x.ConsumableId);
                });

            migrationBuilder.CreateTable(
                name: "Software",
                columns: table => new
                {
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LicenseKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LicenseType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumberOfLicenses = table.Column<int>(type: "int", nullable: false),
                    LicensesInUse = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SupportContact = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.SoftwareId);
                });

            migrationBuilder.InsertData(
                table: "Consumables",
                columns: new[] { "ConsumableId", "Category", "CreatedAt", "CreatedBy", "Description", "IsActive", "LastOrderedDate", "LastReceivedDate", "Location", "Manufacturer", "MinimumStockLevel", "Name", "Notes", "PartNumber", "QuantityInStock", "ReorderQuantity", "Supplier", "UnitOfMeasure", "UnitPrice", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "Printer Supplies", new DateTime(2025, 10, 18, 18, 40, 40, 92, DateTimeKind.Utc).AddTicks(9113), null, null, true, null, null, null, "HP", 5, "HP LaserJet Toner Cartridge", null, "CF410A", 15, 10, null, "Each", 89.99m, null, null },
                    { 2, "Cables", new DateTime(2025, 10, 18, 18, 40, 40, 92, DateTimeKind.Utc).AddTicks(9421), null, null, true, null, null, null, null, 10, "USB-C to HDMI Cable", null, "USB-C-HDMI-6FT", 25, 20, null, "Each", 12.99m, null, null },
                    { 3, "Peripherals", new DateTime(2025, 10, 18, 18, 40, 40, 92, DateTimeKind.Utc).AddTicks(9424), null, null, true, null, null, null, "Logitech", 15, "Wireless Mouse", null, "MX-MASTER-3", 8, 20, null, "Each", 59.99m, null, null }
                });

            migrationBuilder.InsertData(
                table: "Software",
                columns: new[] { "SoftwareId", "Category", "CreatedAt", "CreatedBy", "Description", "ExpirationDate", "IsActive", "LicenseKey", "LicenseType", "LicensesInUse", "Name", "Notes", "NumberOfLicenses", "PurchaseDate", "PurchasePrice", "RenewalDate", "Supplier", "SupportContact", "UpdatedAt", "UpdatedBy", "Vendor", "Version" },
                values: new object[,]
                {
                    { 1, "Office Suite", new DateTime(2025, 10, 18, 18, 40, 40, 92, DateTimeKind.Utc).AddTicks(6151), null, null, null, true, null, "Subscription", 75, "Microsoft Office 365", null, 100, null, null, null, null, null, null, null, "Microsoft", "2024" },
                    { 2, "Operating System", new DateTime(2025, 10, 18, 18, 40, 40, 92, DateTimeKind.Utc).AddTicks(6334), null, null, null, true, null, "Volume", 150, "Windows 11 Pro", null, 200, null, null, null, null, null, null, null, "Microsoft", "23H2" },
                    { 3, "Design Software", new DateTime(2025, 10, 18, 18, 40, 40, 92, DateTimeKind.Utc).AddTicks(6337), null, null, null, true, null, "Subscription", 18, "Adobe Creative Cloud", null, 20, null, null, null, null, null, null, null, "Adobe", "2024" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consumables");

            migrationBuilder.DropTable(
                name: "Software");
        }
    }
}
