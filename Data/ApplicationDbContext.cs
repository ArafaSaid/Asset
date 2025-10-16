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
        }


    }
}
