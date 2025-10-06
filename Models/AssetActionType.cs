using System.ComponentModel.DataAnnotations;

namespace Asset.Models
{
    public class AssetActionType
    {
        [Key]
        public int ActionTypeId { get; set; }
        public required string Name { get; set; }  // e.g., "Maintenance"
        public string? Description { get; set; }
        public ICollection<hdAssetHistory> Histories { get; set; } = new List<hdAssetHistory>();
    }
}
