using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SportShop.Models;
using SportShop.Service;
using SportShop.ViewModels.Category;
using SportShop.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportShop.UnitTest
{
    public class ProductTest
    {
        private DatabaseDbContext _context;
        private ProductService _productService;
        private DbContextOptions<DatabaseDbContext> _options;
        private ProductViewModel _productTest;
        private List<ProductViewModel> _listProduct;
        private IWebHostEnvironment _webHostEnvironment;
        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DatabaseDbContext>()
                .UseInMemoryDatabase("TestSportShop")
                .Options;
            _context = new DatabaseDbContext(_options);
            _productService = new ProductService(_context,_webHostEnvironment);
            _productTest = new ProductViewModel()
            {
                Id = 1,
                Name = "Test",
                Description = "Test description",
                Price = 100,
                DVT = "chiếc",
                Image = "image.jpg",
                Quantity = 1,
                CategoryId = 1,

               

            };
            _listProduct = new List<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Id =1,
                    Name ="Test 1",
                    Description= "Test description 1",
                    Price = 100,
                    DVT = "chiếc",
                    Image = "image.jpg",
                    Quantity= 1,
                    CategoryId= 1,
                },
                new ProductViewModel()
                {
                    Id =2,
                    Name ="Test 2",
                    Description= "Test description 2",
                    Price = 100,
                    DVT = "chiếc",
                    Image = "image.jpg",
                    Quantity= 1,
                    CategoryId= 2,
                }
            };


        }
        [Test]
        
        public async Task Add_Test()
        {
            await _productService.Add(_productTest);
            var result = await _context.Products.FindAsync(_productTest.Id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(_productTest.Name));
        }
       
        [Test]
        public async Task Update_Test()
        {
            await _productService.Add(_productTest);
            _productTest.Name= "Test";
            var existingProduct = await _context.Products.FindAsync(_productTest.Id);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).State = EntityState.Detached;
            }

            await _productService.Update(_productTest);
            var result = await _context.Products.FindAsync(_productTest.Id);
            Assert.NotNull(result);
            Assert.That(result.Name, Is.EqualTo(_productTest.Name));


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
