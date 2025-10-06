using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asset.Models
{
    public class AssetStatus
    {
        [Key]
        public int StatusId { get; set; }
        public required string Name { get; set; }  // e.g., "Active"
        public string? Description { get; set; }
        public ICollection<hdAssets> Assets { get; set; } = new List<hdAssets>();
    }
}
