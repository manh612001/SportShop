using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SportShop.Interface;
using SportShop.Models;
using SportShop.ViewModels.Product;

namespace SportShop.Service
{
    public class ProductService : IProductService
    {
        private readonly DatabaseDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductService(DatabaseDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task Add(ProductViewModel model)
        {
            string imageName = "noimage.png";
            if (model.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                imageName = Guid.NewGuid().ToString() + "_" + model.ImageUpload.FileName;
                string filePath = Path.Combine(uploadsDir, imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await model.ImageUpload.CopyToAsync(fs);
                fs.Close();
            }
           
            var obj = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Image = imageName,
                DVT = model.DVT,
                Price = model.Price,
                Quantity = model.Quantity,
                categoryId = model.CategoryId,
            };
            _context.Products.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }
        }

        public async Task<List<ListProductViewModel>> GetAll()
        {
             return await _context.Products
                                .Include(c=>c.Category)
                                .Select(c=> new ListProductViewModel()
                                { 
                                    Id = c.Id,
                                    Name = c.Name,
                                    Image = c.Image,
                                    DVT = c.DVT,
                                    Price = c.Price,
                                    Quantity = c.Quantity,
                                    Category = c.Category,
                                }).ToListAsync();
                
        }
        public async Task<ProductViewModel> Edit(int? id)
        {
            return await _context.Products.
                Select(p=> new ProductViewModel 
                { 
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Image = p.Image,
                    Quantity = p.Quantity,
                    ImageUpload = p.ImageUpload,
                    DVT = p.DVT
                
                })
                .FirstOrDefaultAsync(x=>x.Id == id);
        }
        public async Task<DetailProductViewModel> GetById(int? id)
        {
            var item =  await _context.Products
                                .Include(c => c.Category)
                                .FirstOrDefaultAsync(x => x.Id == id);
            var category = await _context.Category.FindAsync(item.categoryId);
            return new DetailProductViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Image = item.Image,
                DVT = item.DVT,
                Price = item.Price,
                Description = item.Description,
                Category = new ViewModels.Category.CategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                },
            };
        }

        public async Task<List<ProductViewModel>> GetProductsByCategory(int? id)
        {
            return await _context.Products
                .Include(c => c.Category)
                .Where(p=>p.categoryId == id)
                .Select(p=> new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    DVT= p.DVT,
                    Image= p.Image
                })
                .ToListAsync();
            
        }
        

        public async Task<List<ProductViewModel>> GetProductsByCaTegory(string? key)
        {
            return await _context.Products
                .Include(c => c.Category)
                .Where(p => p.Name == key)
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    DVT = p.DVT,
                    Image = p.Image
                })
                .ToListAsync();
        }

        public async Task Update(ProductViewModel model)
        {
            string imageName = model.Image;
            if (model.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                imageName = Guid.NewGuid().ToString() + "_" + model.ImageUpload.FileName;
                string filePath = Path.Combine(uploadsDir, imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await model.ImageUpload.CopyToAsync(fs);
                fs.Close();
            }
            

            var obj = new Product()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Image = imageName,
               
                DVT = model.DVT,
                Price = model.Price,
                Quantity = model.Quantity,
                categoryId = model.CategoryId,
            };
            _context.Products.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
