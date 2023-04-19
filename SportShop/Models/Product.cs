using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        public string DVT { get; set; }
        public string Image { get; set; }
        [NotMapped]
        
        public IFormFile ImageUpload { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Bạn phải chọn danh mục")]
        public int categoryId { get; set; }
    }
}
