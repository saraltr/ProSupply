using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CSE_325_group_project.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int User_id { get; set; }

        [Required]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [Column("user_fname")]
        public string UserFname { get; set; }

        [Required]
        [Column("user_lastname")]
        public string UserLastName { get; set; }

        [Required]
        [Column("user_email")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one letter and one number.")]
        [Column("user_password")]
        public string UserPassword { get; set; }

        [Required]
        [Column("user_level")]
        public int UserLevel { get; set; }

        // Constructor to initialize required properties
        public User(int userId, 
        string username, 
        string userFname, 
        string userLastName, 
        string userEmail, 
        string userPassword, 
        int userLevel)
        {
            User_id = userId;
            Username = username;
            UserFname = userFname;
            UserLastName = userLastName;
            UserEmail = userEmail;
            UserPassword = userPassword;
            UserLevel = userLevel;
        }

        // parameterless constructor needed for entity framework
        public User()
        {}
    }
}