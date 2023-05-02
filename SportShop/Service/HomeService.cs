using Microsoft.EntityFrameworkCore;
using SportShop.Interface;
using SportShop.Models;
using SportShop.ViewModels.Category;
using SportShop.ViewModels.HomeCustomer;
using SportShop.ViewModels.Product;

namespace SportShop.Service
{
    public class HomeService : IHomeService
    {
        private readonly DatabaseDbContext _context;
        public HomeService(DatabaseDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryViewModel>> GetCategories()
        {
            try
            {
                return await _context.Category
                            .Select(c => new CategoryViewModel
                            {
                                Id = c.Id,
                                Name = c.Name
                            })
                            .ToListAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<HomeCustomer> HomePage()
        {
            try
            {
                HomeCustomer home = new HomeCustomer();
                home.Categories = await _context.Category
                                        .Select(c => new CategoryViewModel
                                        {
                                            Id = c.Id,
                                            Name = c.Name
                                        })
                                        .ToListAsync();
                home.NewProduct = await _context.Products.Select(p => new ProductViewModel
                                                                {
                                                                    Id = p.Id,
                                                                    Name = p.Name,
                                                                    Description = p.Description,
                                                                    Price = p.Price,
                                                                    Image = p.Image,
                                                                    CategoryId = p.categoryId,
                                                                    DVT =p.DVT,
                                                                    Quantity = p.Quantity
                                                                }).ToListAsync();
                return home;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
