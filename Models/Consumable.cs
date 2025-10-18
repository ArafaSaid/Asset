using System.ComponentModel.DataAnnotations;

namespace Asset.Models
{
    public class Consumable
    {
        [Key]
        public int ConsumableId { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Item Name")]
        public required string Name { get; set; }

        [MaxLength(100)]
        public string? Category { get; set; } // e.g., Printer Supplies, Cables, Peripherals

        [MaxLength(100)]
        [Display(Name = "Part Number")]
        public string? PartNumber { get; set; }

        [MaxLength(200)]
        public string? Manufacturer { get; set; }

        [MaxLength(200)]
        public string? Supplier { get; set; }

        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        public decimal? UnitPrice { get; set; }

        [Required]
        [Display(Name = "Quantity in Stock")]
        public int QuantityInStock { get; set; } = 0;

        [Required]
        [Display(Name = "Minimum Stock Level")]
        public int MinimumStockLevel { get; set; } = 0;

        [Display(Name = "Reorder Quantity")]
        public int? ReorderQuantity { get; set; }

        [MaxLength(50)]
        [Display(Name = "Unit of Measure")]
        public string? UnitOfMeasure { get; set; } // e.g., Each, Box, Pack

        [MaxLength(200)]
        public string? Location { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Last Ordered Date")]
        [DataType(DataType.Date)]
        public DateTime? LastOrderedDate { get; set; }

        [Display(Name = "Last Received Date")]
        [DataType(DataType.Date)]
        public DateTime? LastReceivedDate { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [MaxLength(500)]
        public string? Notes { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Created By")]
        [MaxLength(100)]
        public string? CreatedBy { get; set; }

        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "Updated By")]
        [MaxLength(100)]
        public string? UpdatedBy { get; set; }
    }
}
