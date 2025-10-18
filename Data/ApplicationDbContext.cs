using Asset.Models;
using Microsoft.EntityFrameworkCore;

namespace Asset.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        // DbSets 
        public DbSet<hdAssets> Assets { get; set; }
        public DbSet<hdAssetHistory> AssetHistories { get; set; }
        public DbSet<hdAssetTypes> AssetTypes { get; set; }
        public DbSet<AssetStatus> AssetStatuses { get; set; }
        public DbSet<AssetActionType> AssetActionTypes { get; set; }
        public DbSet<Software> Software { get; set; }
        public DbSet<Consumable> Consumables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== Relationships =====

            // hdAssets ↔ hdAssetTypes
            modelBuilder.Entity<hdAssets>()
                .HasOne(a => a.hdAssetTypes)
                .WithMany(t => t.Assets)
                .HasForeignKey(a => a.TypeID)
                .OnDelete(DeleteBehavior.Restrict);

            // hdAssets ↔ AssetStatus
            modelBuilder.Entity<hdAssets>()
                .HasOne(a => a.Status)
                .WithMany(s => s.Assets)
                .HasForeignKey(a => a.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // hdAssetHistory ↔ hdAssets
            modelBuilder.Entity<hdAssetHistory>()
                .HasOne(h => h.Asset)
                .WithMany(a => a.History)
                .HasForeignKey(h => h.AssetID)
                .OnDelete(DeleteBehavior.Cascade);

            // hdAssetHistory ↔ AssetActionType
            modelBuilder.Entity<hdAssetHistory>()
                .HasOne(h => h.ActionType)
                .WithMany(a => a.Histories)
                .HasForeignKey(h => h.ActionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== Seeding Lookup Tables =====
            modelBuilder.Entity<AssetStatus>().HasData(
                new AssetStatus { StatusId = 1, Name = "Active", Description = "Asset currently in use" },
                new AssetStatus { StatusId = 2, Name = "Inactive", Description = "Not in use but available" },
                new AssetStatus { StatusId = 3, Name = "Under Maintenance", Description = "Asset under repair or servicing" },
                new AssetStatus { StatusId = 4, Name = "Retired", Description = "No longer in service" }
            );

            modelBuilder.Entity<AssetActionType>().HasData(
                new AssetActionType { ActionTypeId = 1, Name = "Maintenance", Description = "Asset maintenance performed" },
                new AssetActionType { ActionTypeId = 2, Name = "User Change", Description = "Asset assigned to a different user" },
                new AssetActionType { ActionTypeId = 3, Name = "Spare Part Replacement", Description = "Replaced a part of the asset" },
                new AssetActionType { ActionTypeId = 4, Name = "Assignment", Description = "New asset assignment" },
                new AssetActionType { ActionTypeId = 5, Name = "Hardware Addition", Description = "Hardware component added to asset" },
                new AssetActionType { ActionTypeId = 6, Name = "Hardware Removal", Description = "Hardware component removed from asset" },
                new AssetActionType { ActionTypeId = 7, Name = "Return to IT", Description = "Asset returned to IT department" },
                new AssetActionType { ActionTypeId = 8, Name = "Retired", Description = "Asset permanently retired from service" },
                new AssetActionType { ActionTypeId = 9, Name = "Transfer", Description = "Asset transferred to different location" },
                new AssetActionType { ActionTypeId = 10, Name = "Repair", Description = "Asset repair work performed" },
                new AssetActionType { ActionTypeId = 11, Name = "Upgrade", Description = "Asset upgraded with new components" },
                new AssetActionType { ActionTypeId = 12, Name = "Deployment", Description = "Asset deployed to user" }
            );

            modelBuilder.Entity<hdAssetTypes>().HasData(
                new hdAssetTypes { TypeID = 1, Name = "Laptop"},
                new hdAssetTypes { TypeID = 2, Name = "DeskTop" },
                new hdAssetTypes { TypeID = 3, Name = "Server" }
                
            );

            // Seed data for Software (sample records)
            modelBuilder.Entity<Software>().HasData(
                new Software 
                { 
                    SoftwareId = 1, 
                    Name = "Microsoft Office 365",
                    Version = "2024",
                    Vendor = "Microsoft",
                    LicenseType = "Subscription",
                    NumberOfLicenses = 100,
                    LicensesInUse = 75,
                    Category = "Office Suite",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Software 
                { 
                    SoftwareId = 2, 
                    Name = "Windows 11 Pro",
                    Version = "23H2",
                    Vendor = "Microsoft",
                    LicenseType = "Volume",
                    NumberOfLicenses = 200,
                    LicensesInUse = 150,
                    Category = "Operating System",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Software 
                { 
                    SoftwareId = 3, 
                    Name = "Adobe Creative Cloud",
                    Version = "2024",
                    Vendor = "Adobe",
                    LicenseType = "Subscription",
                    NumberOfLicenses = 20,
                    LicensesInUse = 18,
                    Category = "Design Software",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed data for Consumables (sample records)
            modelBuilder.Entity<Consumable>().HasData(
                new Consumable 
                { 
                    ConsumableId = 1, 
                    Name = "HP LaserJet Toner Cartridge",
                    Category = "Printer Supplies",
                    PartNumber = "CF410A",
                    Manufacturer = "HP",
                    QuantityInStock = 15,
                    MinimumStockLevel = 5,
                    ReorderQuantity = 10,
                    UnitOfMeasure = "Each",
                    UnitPrice = 89.99m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Consumable 
                { 
                    ConsumableId = 2, 
                    Name = "USB-C to HDMI Cable",
                    Category = "Cables",
                    PartNumber = "USB-C-HDMI-6FT",
                    QuantityInStock = 25,
                    MinimumStockLevel = 10,
                    ReorderQuantity = 20,
                    UnitOfMeasure = "Each",
                    UnitPrice = 12.99m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Consumable 
                { 
                    ConsumableId = 3, 
                    Name = "Wireless Mouse",
                    Category = "Peripherals",
                    PartNumber = "MX-MASTER-3",
                    Manufacturer = "Logitech",
                    QuantityInStock = 8,
                    MinimumStockLevel = 15,
                    ReorderQuantity = 20,
                    UnitOfMeasure = "Each",
                    UnitPrice = 59.99m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }


    }
}
