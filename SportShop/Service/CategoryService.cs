using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportShop.Interface;
using SportShop.Models;
using SportShop.ViewModels.Category;

namespace SportShop.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(DatabaseDbContext context)
        {
            _context = context;
            
        }
        public async Task Add(CategoryViewModel model)
        {
            var obj = new Category()
            {
                Name = model.Name,
            };
            _context.Category.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            try
            {
                var item = await _context.Category.FindAsync(id);
                if (item == null)
                {
                    throw new Exception("Không tìm thấy danh mục"); 
                }
                _context.Category.Remove(item);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw new Exception("Lỗi không thể xóa được danh mục");
            }
            

        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            return await _context.Category.Select(x => new CategoryViewModel()
            { 
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
           
        }

        public async Task<CategoryViewModel> GetById(int? id)
        {
            return await _context.Category.Select(x=> new CategoryViewModel() { Id = x.Id,Name =x.Name}).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task Update(CategoryViewModel model)
        {
            var obj = new Category()
            {
                Id = model.Id,
                Name = model.Name,
            };
            _context.Category.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
