using Microsoft.EntityFrameworkCore;
using SportShop.Models;
using SportShop.Service;
using SportShop.ViewModels.Category;

namespace SportShop.UnitTest
{
    public class CategoryTest
    {
        private DatabaseDbContext _context;
        private CategoryService _categoryService;
        private DbContextOptions<DatabaseDbContext> _options;
        private CategoryViewModel categoryTest;
        private List<CategoryViewModel> _listCategory;
        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DatabaseDbContext>()
                .UseInMemoryDatabase("TestSportShop")
                .Options;
            _context = new DatabaseDbContext(_options);
            _categoryService = new CategoryService(_context);
            categoryTest = new CategoryViewModel()
            {
                Id = 1,
                Name = "Test",
            };
            _listCategory = new List<CategoryViewModel>()
            {
                new CategoryViewModel(){Id =1,Name="Test 1"},
                new CategoryViewModel(){Id =2,Name="Test 2"}
            };


        }

        [Test]
        public async Task Add_Test()
        {
            await _categoryService.Add(categoryTest);
            var result = await _context.Category.FindAsync(categoryTest.Id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(categoryTest.Name));
        }
        [Test]
        public async Task GetById_Test()
        {
            await _categoryService.Add(categoryTest);


            var result = await _categoryService.GetById(categoryTest.Id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(categoryTest.Id));
            Assert.That(result.Name, Is.EqualTo(categoryTest.Name));
        }
        [Test]
        public async Task GetAll()
        {
            foreach (var item in _listCategory)
            {
                await _categoryService.Add(item);
            }
            var result = await _categoryService.GetAll();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count == 2, Is.True);
            Assert.That(result[0].Name, Is.EqualTo(_listCategory[0].Name));
        }
        [Test]
        public async Task Update_Test()
        {
            await _categoryService.Add(categoryTest);
            categoryTest.Name = "Test 100";
            var existingCategory = await _context.Category.FindAsync(categoryTest.Id);
            if (existingCategory != null)
            {
                _context.Entry(existingCategory).State = EntityState.Detached;
            }
            
            await _categoryService.Update(categoryTest);
            var result = await _context.Category.FindAsync(categoryTest.Id);
            Assert.NotNull(result);
            Assert.That(result.Name, Is.EqualTo(categoryTest.Name));
            

        }

        [TearDown]
        public void TearDown()
        {
            using (var context = new DatabaseDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}