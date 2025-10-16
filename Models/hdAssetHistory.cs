using System.ComponentModel.DataAnnotations;

namespace Asset.Models
{
    public class hdAssetHistory
    {
        [Key]
        public int HistoryID { get; set; }

        // Link to the asset
        public int AssetID { get; set; }
        public hdAssets? Asset { get; set; }

        // Type of change
        public int ActionTypeId { get; set; }
        public AssetActionType? ActionType { get; set; }

        // Description of what happened
        [MaxLength(1000)]
        public string? Description { get; set; }

        // OLD VALUE and NEW VALUE (to see what changed)
        [MaxLength(200)]
        public string? FromUser { get; set; }
        
        [MaxLength(200)]
        public string? ToUser { get; set; }

        [MaxLength(200)]
        public string? FromLocation { get; set; }
        
        [MaxLength(200)]
        public string? ToLocation { get; set; }

        public int? FromStatusId { get; set; }
        public AssetStatus? FromStatus { get; set; }  // NEW: Navigation property
        
        public int? ToStatusId { get; set; }
        public AssetStatus? ToStatus { get; set; }    // NEW: Navigation property

        // Spare part info
        [MaxLength(200)]
        public string? SparePart { get; set; }
        
        [MaxLength(100)]
        public string? SparePartSerialNumber { get; set; }

        // Who performed the change
        [MaxLength(100)]
        public string? PerformedBy { get; set; }

        // Who is assigned now
        [MaxLength(200)]
        public string? AssignedToUser { get; set; }

        // Cost of the action
        public decimal? Cost { get; set; }

        // Reason for return
        [MaxLength(500)]
        public string? ReturnReason { get; set; }

        // Optional: Attachment for receipts/photos
        [MaxLength(500)]
        public string? AttachmentPath { get; set; }  // NEW: Store file path

        // Dates
        public DateTime ActionDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [MaxLength(100)]
        public string? CreatedBy { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        [MaxLength(100)]
        public string? UpdatedBy { get; set; }
    }
}
