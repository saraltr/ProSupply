using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_325_group_project.Models
{

[Table("category")]

public class Category
{
    [Key]
    [Column("category_id")]
        public int Category_id { get; set; }

    [Required, StringLength(45)]
    [Column("category_name")]
        public required string Category_name {get; set;}

    [Required, StringLength(45)]
    [Column("category_description")]
        public required string Category_description {get; set;}

    // constructor
    public Category(
        int categoryId,
        string categoryName,
        string categoryDescription
    )
    {
        Category_id = categoryId;
        Category_name = categoryName;
        Category_description = categoryDescription;
    }

    // parameterless constructor for EF Core

    public Category (){}

    }
}