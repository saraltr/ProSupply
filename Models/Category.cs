using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_325_group_project.Models
{

[Table("category")]

public class Category
{
    [Key]
    public int category_id { get; set; }

    [Required, StringLength(45)]
    public required string category_name {get; set;}

    [Required, StringLength(45)]
    public required string category_description {get; set;}
}
}