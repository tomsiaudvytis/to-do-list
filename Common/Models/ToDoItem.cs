using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    [Table("ToDoItem")]
    public class ToDoItem
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("AssignedToId")]
        public int? AssignedToId { get; set; }

        [Column("Name")]
        [Required]
        public string Name { get; set; }

        [Column("IsCompleted")]
        [Required]
        public bool IsComplete { get; set; }
    }
}
