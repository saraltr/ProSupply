using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("supplier")]
public class Supplier
{
    [Key]
    [Column("supplier_id")]
    public int SupplierId { get; set; }

    [Required]
    [Column("category_id")]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(45)]
    [Column("supplier_name")]
    public required string SupplierName { get; set; }

    [Required]
    [StringLength(45)]
    [Column("supplier_phone")]
    public required string SupplierPhone { get; set; }

    [Required]
    [StringLength(45)]
    [Column("supplier_email")]
    public required string SupplierEmail { get; set; }

    [Required]
    [StringLength(45)]
    [Column("supplier_address")]
    public required string SupplierAddress { get; set; }

    [Required]
    [StringLength(45)]
    [Column("supplier_description")]
    public required string SupplierDescription { get; set; }

    [Required]
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("company_id")]
    public int? CompanyId { get; set; }

    // Foreign key relationships
    [ForeignKey("CompanyId")]
    public virtual required Company Company { get; set; }

    // [ForeignKey("UserId")]
    // public virtual User User { get; set; }

    [ForeignKey("CategoryId")]
    public virtual required Category Category { get; set; }
}
