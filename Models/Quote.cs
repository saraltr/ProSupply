using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_325_group_project.Models
{
    [Table("quote")]
    public class Quote
    {
        [Key]
        [Column("quote_id")]
        public int QuoteId { get; set; }

        [Required]
        [Column("quote_date")]
        public DateTime QuoteDate { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(300)]
        [Column("quote_details")]
        public string QuoteDetails { get; set; }
        
        [Column("quote_price")]
        public decimal? QuotePrice { get; set;}

        [Required]
        [Column("quote_status")]
        public int QuoteStatus { get; set; }

        [StringLength(250)]
        [Column("quote_notes")]
        public string? QuoteNotes { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [Required]
        [Column("supplier_id")]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier? Supplier { get; set; }

        public Quote() {}

        public Quote(
            int quoteId,
            int userId,
            int supplierId,
            string quoteDetails,
            int quoteStatus,
            decimal? quotePrice = null,
            string? quoteNotes = null,
            DateTime quoteDate = default
        )
        {
            QuoteId = quoteId;
            UserId = userId;
            SupplierId = supplierId;
            QuoteDetails = quoteDetails;
            QuoteStatus = quoteStatus;
            QuoteNotes = quoteNotes;
            QuotePrice = quotePrice;
            QuoteDate = quoteDate == default ? DateTime.UtcNow : quoteDate;

        }
    }
}