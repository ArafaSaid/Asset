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

        // Type of change (e.g., Spare Part Replacement, User Change, Maintenance)
        //public string ActionType { get; set; }
        public int ActionTypeId { get; set; }
        public AssetActionType? ActionType { get; set; }
        // Description of what happened
        public string? Description { get; set; }

        // Spare part info (optional if ActionType == Spare Part Replacement)
        public string? SparePart { get; set; }

        // Who performed the change (technician or IT staff)
        public string? PerformedBy { get; set; }

        // Who is the assigned user (in case of user change)
        public string? AssignedToUser { get; set; }

        // Date of the action
        public DateTime ActionDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

    }
}
