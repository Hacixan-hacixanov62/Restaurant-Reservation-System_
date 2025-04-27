using AutoMapper;
using Azure.Core;
using CloudinaryDotNet;
using CloudinaryDotNet.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using MimeKit.Encodings;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Core.Enums;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.DataAccess.Localizers;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Exceptions;
using Restaurant_Reservation_System_.Service.Extensions;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.ProductVM;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        public ProductService(IWebHostEnvironment env, IProductRepository productRepository,AppDbContext context, ICategoryService categoryService, IConfiguration configuration ,ICloudinaryService cloudinaryService,IMapper mapper)
        {
            _productRepository = productRepository;
            _context = context;
            _env = env;
            _configuration = configuration;
            _categoryService = categoryService;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }


        public async Task CreateAsync(ProductCreateDto productCreateDto)
        {
         
            Product product = _mapper.Map<Product>(productCreateDto);

            product.ProductImages = [];
            
            var mainFileName = await _cloudinaryService.FileCreateAsync(productCreateDto.MainFile);
            var mainProductImageCreate = CreateProductImage(mainFileName, true, product);
            product.ProductImages.Add(mainProductImageCreate);


            foreach (var file in productCreateDto.AdditionalFiles)
            {
                var filename = await _cloudinaryService.FileCreateAsync(file);
                var additionalProductImgs = CreateProductImage(filename, false, product);
                product.ProductImages.Add(additionalProductImgs);

            }

         

            await _productRepository.CreateAsync(product);
           await  _productRepository.SaveChangesAsync();
        }


        private ProductImage CreateProductImage(string url, bool isMain, Product product)
        {
            return new ProductImage
            {
                Url = url,
                IsMain = isMain,
                Product = product
            };
        }

        public async Task DeleteAsync(int id)
        {
            var product = _productRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (product == null)
            {
                throw new Exception("Product tapılmadı");
            }

           await _productRepository.Delete(product);
           await  _productRepository.SaveChangesAsync();
        }

        public async Task<Product> DetailAsync(int id)
        {
            var product = _productRepository.GetAll()
                 .Include(m => m.Category)
                 .Include(m => m.ProductImages)
                 .Include(m => m.ProductDetails)
                 .FirstOrDefault(m => m.Id == id);
            await _productRepository.SaveChangesAsync();
            if (product == null)
            {
                throw new Exception("Product tapılmadı");
            }
            return product;

        }



        public async Task EditAsync(int id, ProductUpdateDto productUpdateDto)
        {
            var request = _productRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (request == null)
            {
                throw new Exception("Product tapılmadı");
            }

            var existProduct = _productRepository.GetAll()
                .Include(m => m.ProductImages)
                .Include(m => m.ProductDetails)
                .FirstOrDefault(m => m.Id ==id);

            if (existProduct is null)
            {
                throw new Exception("Product Tapilmadi");
            }


            if (productUpdateDto.CategoryId != existProduct.CategoryId)
            {
                if (!_context.Categories.Any(g => g.Id == productUpdateDto.CategoryId))
                {
                    throw new Exception("Category not found");
                }
            }


            var ExistedImages = existProduct.ProductImages.Where(x => !x.IsMain).Select(x => x.Id).ToList();
            if (productUpdateDto.ImageIds is not null)
            {
                ExistedImages = ExistedImages.Except(productUpdateDto.ImageIds).ToList();

            }
            if (ExistedImages.Count > 0)
            {
                foreach (var imageId in ExistedImages)
                {
                    var deletedImage = existProduct.ProductImages.FirstOrDefault(x => x.Id == imageId);
                    if (deletedImage is not null)
                    {

                        existProduct.ProductImages.Remove(deletedImage);

                        deletedImage.Url.DeleteFile(_env.WebRootPath, "assets/images/home");
                    }

                }
            }


            foreach (var file in productUpdateDto.AdditionalFiles)
            {
                var filename = await _cloudinaryService.FileCreateAsync(file);
                var additionalProductImages = new ProductImage() { IsMain = false, Product = existProduct, Url = filename };
                existProduct.ProductImages.Add(additionalProductImages);

            }


            if (productUpdateDto.MainFile is not null)
            {
                var existMainImage = existProduct.ProductImages.FirstOrDefault(x => x.IsMain);
                //remove exist image
                if (existMainImage is not null)
                {
                    existProduct.ProductImages.Remove(existMainImage);

                }



                var filename = await _cloudinaryService.FileCreateAsync(productUpdateDto.MainFile);
                ProductImage image = new ProductImage() { IsMain = true, Product = existProduct, Url = filename };
                existProduct.ProductImages.Add(image);

            }


            existProduct = _mapper.Map(productUpdateDto, existProduct);

            _productRepository.Update(existProduct);
           await _productRepository.SaveChangesAsync();
        }

            public async Task<List<Product>> GetAllAsync()
            {
                var products = await _productRepository.GetAll(include : x=>x.Include(c=>c.ProductImages)).ToListAsync();

                return products;
                //return products.Select(s => new Product
                //{
                //    Id = s.Id,
                //    Name = s.Name,
                //    Price = s.Price,
                //    Desc = s.Desc,
                //    Porsion = s.Porsion,
                //    Discount = s.Discount,
                //    Weight = s.Weight,
                //    Delicious = s.Delicious,
                //   ProductImages =s.ProductImages

                //    // CreatedDate = s.CreatedDate.ToString("yyyy-MM-dd")
                //}).ToList();
            }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _productRepository.IsExistAsync(x => x.Id == id);
        }

        public async Task<ProductGetDto> GetByIdAsync(int productId)
        {
            var product = await _productRepository.GetAsync(x => x.Id == productId,
                                                      x => x.Include(p => p.ProductImages));
            if (product == null) return null;

            var dto = _mapper.Map<ProductGetDto>(product);
            return dto;
        }

        public async Task<List<Product>> LoadMoreAsync(int page)
        {
            int totalProducts = await _context.Products.CountAsync();
            int pageCount = (int)Math.Ceiling((decimal)totalProducts / 4);

            page = Math.Clamp(page, 1, pageCount == 0 ? 1 : pageCount);

            var products = await _context.Products
                .Include(x => x.ProductImages)
                .Skip((page - 1) * 4)
                .Take(4)
                .ToListAsync();

            foreach (var product in products)
            {
                foreach (var img in product.ProductImages)
                {
                    img.Product = null; // prevent circular reference
                }
            }

            return products;
        }
    }
}
