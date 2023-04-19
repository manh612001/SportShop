using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShop.ViewModels.Product
{
    public class ListProductViewModel
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
        public SportShop.Models.Category Category { get; set; }
    }
}
