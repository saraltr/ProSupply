using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("company")]
public class Company
{
    [Key]
    public int company_id { get; set; }

    [Required, StringLength(70)]
    public required string company_name { get; set; }

    [Required, StringLength(45)]
    public required string company_phone { get; set; }

    [Required, StringLength(45)]
    public required string company_email { get; set; }

    [Required]
    public required string company_address { get; set; }

    public string? company_description { get; set; } 

    [Required]
    public int industry_id { get; set; }

    [Required]
    public int user_id { get; set; }
}
