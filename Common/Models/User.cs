using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Models
{
    [Table("ToDoItems")]
    public class User
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("FirstName")]
        [Required]
        public string FirstName { get; set; }

        [Column("LastName")]
        [Required]
        public string LastName { get; set; }

        [Column("Email")]
        [Required]
        public string Email { get; set; }

        [Column("Role")]
        [Required]
        public string Role { get; set; }

        [JsonIgnore]
        [Column("Password")]
        [Required]
        public string Password { get; set; }
    }
}