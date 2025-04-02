using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_325_group_project.Models
{

    [Table("service")]
    public class Service
    {
        [Key]
        [Required]
        [Column("service_id")]
        public int ServiceId { get; set; }

        [Required]
        [StringLength(45)]
        [Column("service_name")]
        public required string ServiceName { get; set; } 

        [Required]
        [StringLength(250)]
        [Column("service_description")]
        public required string ServiceDescription { get; set; }

        [Required]
        [Column("service_price")]
        public decimal ServicePrice { get; set; }

        [Required]
        [Column("supplier_id")]
        public int SupplierId { get; set; }

        // Foreign key relationship
        [ForeignKey("SupplierId")]
        public virtual Supplier? Supplier { get; set; }

        // constructor
        public Service(int serviceId, string serviceName, string serviceDescription, decimal servicePrice, int supplierId)
        {
            ServiceId = serviceId;
            ServiceName = serviceName;
            ServiceDescription = serviceDescription;
            ServicePrice = servicePrice;
            SupplierId = supplierId;
        }

        // parameterless constructor for EF Core
        public Service() { }
    }
}