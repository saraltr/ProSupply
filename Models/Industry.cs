using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_325_group_project.Models
{


[Table("industry")]
public class Industry
{
    [Key]
    [Column("industry_id")]
    public int Industry_id { get; set; }
    
    [Column("industry_name")] 
    public  string? Industry_Name { get; set; }
}
}