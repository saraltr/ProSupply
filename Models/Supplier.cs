using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_325_group_project.Models
{
    [Table("supplier")]
    public class Supplier
    {
        [Key]
        [Column("supplier_id")]
        public int SupplierId { get; set; }

        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Supplier Name Required")]
        [StringLength(45)]
        [Column("supplier_name")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 15 characters.")]
        [Column("supplier_phone")]
        public string SupplierPhone { get; set; }

        [Required(ErrorMessage = "Phone email required")]
        [StringLength(45, ErrorMessage = "Email must be at most 45 characters long")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Column("supplier_email")]
        public string SupplierEmail { get; set; }

        [Required(ErrorMessage = "Address required")]
        [StringLength(45)]
        [Column("supplier_address")]
        public string SupplierAddress { get; set; }

        [Required(ErrorMessage = "Description required")]
        [StringLength(230)]
        [Column("supplier_description")]
        public string SupplierDescription { get; set; }

        [Column("supplier_logo")]
        public string SupplierLogo { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("company_id")]
        public int? CompanyId { get; set; }

        // Foreign key relationships
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [InverseProperty("Supplier")]
        public virtual List<Service> Services { get; set; } = new();

        public Supplier() { }

        public Supplier(
            int supplierId, 
            int categoryId, 
            string supplierName, 
            string supplierPhone, 
            string supplierEmail,
            string supplierAddress, 
            string supplierDescription, 
            string supplierLogo,
            int userId, 
            int? companyId = null)
        {
            SupplierId = supplierId;
            CategoryId = categoryId;
            SupplierName = supplierName;
            SupplierPhone = supplierPhone;
            SupplierEmail = supplierEmail;
            SupplierAddress = supplierAddress;
            SupplierDescription = supplierDescription;
            SupplierLogo = supplierLogo;
            UserId = userId;
            CompanyId = companyId;
            Services = [];
        }
    }
}
