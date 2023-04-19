using AutoMapper;
using SportShop.ViewModels.Product;

namespace SportShop.Models
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }

    }
}
