using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportShop.Models;
using SportShop.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShop.ViewModels.Product
{
    public class DetailProductViewModel
    {
        [Key]

        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Required]
        public string? DVT { get; set; }
        public string? Image { get; set; }
        [Required]
        public CategoryViewModel? Category { get; set; }
        
    }
}
