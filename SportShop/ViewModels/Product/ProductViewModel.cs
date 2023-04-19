using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SportShop.Models;
namespace SportShop.ViewModels.Product
{
    public class ProductViewModel
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
        [Required]
        public int CategoryId { get; set; }

    }
}
