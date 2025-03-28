using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.ProductVM;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductService(IWebHostEnvironment env, IProductRepository productRepository,AppDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(Product  product)
        {

            var files = product.Photos;

                
            product.ProductImages = new();

            foreach (var file in files)
            {
                ProductImage productImage = new();
                productImage.Product = product;
                productImage.ProductId = product.Id;
                productImage.Status = false;

                productImage.Name = file.SaveImage(_env.WebRootPath, "assets/images/home");
                if (files[0] == file)
                {
                    productImage.Status = true;
                }
                //productImage.Status = null;
                product.ProductImages.Add(productImage);
                _context.ProductImages.Add(productImage);

            }

          await   _productRepository.CreateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = _productRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (product == null)
            {
                throw new Exception("Product tapılmadı");
            }

            _productRepository.Delete(product);
        }

        public async Task<Product> DetailAsync(int id)
        {
           var product = _productRepository.GetAll()
                .Include(m=>m.Category)
                .Include(m=>m.ProductImages)
                .Include(m=>m.ProductDetails)
                .FirstOrDefault(m=> m.Id == id);
            if (product == null)
            {
                throw new Exception("Product tapılmadı");
            }
            return product;

        }


        public async Task EditAsync(int id, Product product)
        {
            var request = _productRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (request == null)
            {
                throw new Exception("Product tapılmadı");
            }

            var existProduct = _productRepository.GetAll()            
                .Include(m => m.ProductImages)
                .Include(m => m.ProductDetails)
                .FirstOrDefault(m => m.Id == product.Id);

            if (existProduct is null)
            {
                throw new Exception("Product Tapilmadi");
            }


            if (product.CategoryId != existProduct.CategoryId)
            {
                if (!_context.Categories.Any(g => g.Id == product.CategoryId))
                {
                    throw new Exception("Category not found");
                }
            }
              
                
            var files = product.Photos;
            if (files != null)
            {
                foreach (var file in files)
                {
                    ProductImage productImage = new();
                    if (file != null && productImage != null && product.ProductImages != null)
                    {
                        productImage.Name = file.SaveImage(_env.WebRootPath, "assets/images/home");
                        existProduct.ProductImages.Add(productImage);
                    }

                }
            }


            existProduct.Name = product.Name;
            existProduct.Price = product.Price;
            existProduct.Desc = product.Desc;
            existProduct.Delicious = product.Delicious;
            existProduct.CategoryId = product.CategoryId;

            _productRepository.Update(existProduct);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _productRepository.GetAll().ToListAsync();
            return products.Select(s => new Product
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                Desc = s.Desc,
                Delicious = s.Delicious,
                
                // CreatedDate = s.CreatedDate.ToString("yyyy-MM-dd")
            }).ToList();
        }
    }
}
