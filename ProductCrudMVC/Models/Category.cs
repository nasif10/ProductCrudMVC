using System.ComponentModel.DataAnnotations;

namespace ProductCrudMVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public DateTime Createdat { get; set; } = DateTime.Now;
    }
}
