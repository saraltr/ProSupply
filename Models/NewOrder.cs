using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_325_group_project.Models
{
    [Table("order")]
    public class NewOrder
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Required]
        [Column("order_date")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("order_amount")]
        public decimal OrderAmount { get; set; }

        [Required]
        [StringLength(255)]
        [Column("order_address1")]
        public string OrderAddress1 { get; set; }

        [StringLength(255)]
        [Column("order_address2")]
        public string? OrderAddress2 { get; set; }

        [Required]
        [StringLength(100)]
        [Column("order_city")]
        public string OrderCity { get; set; }

        [Required]
        [StringLength(20)]
        [Column("order_zip")]
        public string OrderZip { get; set; }

        [Required]
        [StringLength(50)]
        [Column("order_country")]
        public string OrderCountry { get; set; }

        [Required]
        [Column("order_status")]
        public int OrderStatus { get; set; }

        [Required]
        [Column("supplier_id")]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier? Supplier { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        // constructor
        public NewOrder(
            
           int orderId,
           int supplierId,
           int userId,
           decimal orderAmount,
           string orderAddress1,
           string orderCity,
           string orderZip,
           string orderCountry,
           int orderStatus,
           string? orderAddress2 = null,
           DateTime orderDate = default
       )
        {
            OrderId = orderId;
            SupplierId = supplierId;
            UserId = userId;
            OrderAmount = orderAmount;
            OrderAddress1 = orderAddress1;
            OrderAddress2 = orderAddress2;
            OrderCity = orderCity;
            OrderZip = orderZip;
            OrderCountry = orderCountry;
            OrderDate = orderDate == default ? DateTime.UtcNow : orderDate;
            OrderStatus = orderStatus;
        }

        // parameterless constructor for EF
        public NewOrder() { }
    }

    public enum OrderStatusEnum
    {
        pending = 1,
        approved = 2,
        shipped = 3,
        completed = 4,
        canceled = 5
    }

}