using System.ComponentModel.DataAnnotations;

namespace Asset.Models
{
    public class hdAssets
    {
        [Key]
        public int AssetID { get; set; }
        public required string ComputerName { get; set; }
        public required string UserName { get; set; }
        public int? TypeID { get; set; }
        public hdAssetTypes? hdAssetTypes { get; set; }
        public string? SerialNumber { get; set; }
        public string? Model { get; set; }
        public string? Manufacturer { get; set; }
        public string? Supplier { get; set; }
        public string? Processor { get; set; }
        public string? RAM { get; set; }
        public string? Storage { get; set; }
        public string? OS { get; set; }
        public string? Monitor { get; set; }
        public string? Printer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? WarrantyExpirationDate { get; set; }
        public string? Location { get; set; }
        //public string? Status { get; set; } // e.g., Active, Inactive, Under Maintenance
        public int StatusId { get; set; }
        public AssetStatus? Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        // History navigation
        public ICollection<hdAssetHistory> History { get; set; } = new List<hdAssetHistory>();
    }
}
