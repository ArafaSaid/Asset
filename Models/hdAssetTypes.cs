using System.ComponentModel.DataAnnotations;

namespace Asset.Models
{
    public class hdAssetTypes
    {
        [Key]
        public int TypeID { get; set; }
        public required string Name { get; set; }

        // Add a collection navigation property for hdAssets
        public ICollection<hdAssets> Assets { get; set; } = new List<hdAssets>();
    }
}
