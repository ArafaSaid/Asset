using System.ComponentModel.DataAnnotations;

namespace Asset.Models
{
    public class Software
    {
        [Key]
        public int SoftwareId { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Software Name")]
        public required string Name { get; set; }

        [MaxLength(100)]
        public string? Version { get; set; }

        [MaxLength(200)]
        public string? Vendor { get; set; }

        [MaxLength(100)]
        [Display(Name = "License Key")]
        public string? LicenseKey { get; set; }

        [Display(Name = "License Type")]
        [MaxLength(100)]
        public string? LicenseType { get; set; } // e.g., Perpetual, Subscription, Volume

        [Display(Name = "Number of Licenses")]
        public int NumberOfLicenses { get; set; } = 1;

        [Display(Name = "Licenses In Use")]
        public int LicensesInUse { get; set; } = 0;

        [Display(Name = "Purchase Date")]
        [DataType(DataType.Date)]
        public DateTime? PurchaseDate { get; set; }

        [Display(Name = "Purchase Price")]
        [DataType(DataType.Currency)]
        public decimal? PurchasePrice { get; set; }

        [Display(Name = "Expiration Date")]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Renewal Date")]
        [DataType(DataType.Date)]
        public DateTime? RenewalDate { get; set; }

        [MaxLength(200)]
        public string? Supplier { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(200)]
        public string? Category { get; set; } // e.g., Operating System, Office Suite, Antivirus

        [Display(Name = "Support Contact")]
        [MaxLength(200)]
        public string? SupportContact { get; set; }

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
