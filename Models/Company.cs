using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_325_group_project.Models
{

[Table("company")]
public class Company
{
    [Key]
    [Column("company_id")]
    public int Company_id { get; set; }

    [Required, StringLength(70)]
    [Column("company_name")]
    public  string? Company_name { get; set; }

    [Required, StringLength(45)]
    [Column("company_phone")]
    public  string? Company_phone { get; set; }

    [Required, StringLength(45)]
    [Column("company_email")]
    public  string? Company_email { get; set; }

    [Required]
    [Column("company_address")]
    public  string? Company_address { get; set; }

    [Column ("company_description")]
    public string? Company_description { get; set; } 

    [Required]
    [Column ("industry_id")]
    public int Industry_id { get; set; }

    [Required]
    [Column ("user_id")]
    public int User_id { get; set; }
    
    // constructor
    public Company(
            string companyName, 
            string companyPhone, 
            string companyEmail, 
            string companyAddress, 
            int industryId, 
            int userId, 
            string? companyDescription = null)
    {
        Company_name = companyName;
        Company_phone = companyPhone;
        Company_email = companyEmail;
        Company_address = companyAddress;
        Industry_id = industryId;
        User_id = userId;
        Company_description = companyDescription;
    }

    // parameterless constructor for EF Core

    public Company() {}
    }
}
