
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Core.Enums;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Localizers;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Exceptions;
using Restaurant_Reservation_System_.Service.Extensions;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(AppDbContext context,ICategoryRepository categoryRepository, IMapper mapper)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _mapper = mapper;

        }

        public async Task CreateAsync(CategoryCreateDto categoryCreateDto)
        {

            Category category = _mapper.Map<Category>(categoryCreateDto);

            await  _categoryRepository.CreateAsync(category);
           await  _categoryRepository.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(s => s.Id == id);
            if (category == null)
            {
                throw new Exception("Category tapılmadı");
            }

            _categoryRepository.Delete(category);
        }

        public async Task<Category> DetailAsync(int id)
        {
            var category = await _categoryRepository.GetAll()
                 .Where(s => s.Id == id)
                 .Select(s => new Category
                 {
                     Id = s.Id,
                     Title = s.Title,
                     SubTitle = s.SubTitle,
                 })
                 .FirstOrDefaultAsync();

            if (category == null)
            {
                throw new Exception("Category tapılmadı");
            }

            return category;
        }

        public async Task EditAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(s => s.Id == id);

            if (category == null)
            {
                throw new Exception("Category tapılmadı");
            }

            CategoryUpdateDto dto = _mapper.Map<CategoryUpdateDto>(category);

            _categoryRepository.Update(category);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAll().ToListAsync();
            return categories.Select(s => new Category
            {
                Id = s.Id,
                Title = s.Title,
                SubTitle = s.SubTitle
            }).ToList();
        }




        ////public async Task<bool> CreateAsync(CategoryCreateDto dto, ModelStateDictionary ModelState)
        ////{
        ////    if (!ModelState.IsValid)
        ////        return false;

        ////    //foreach (var detail in dto.CategoryDetails)
        ////    //{
        ////    //    var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

        ////    //    if (!isExistLanguage)
        ////    //    {
        ////    //        ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
        ////    //        return false;
        ////    //    }

        ////    //    isExistLanguage = dto.CategoryDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

        ////    //    if (isExistLanguage)
        ////    //    {
        ////    //        ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
        ////    //        return false;
        ////    //    }
        ////    //}

        ////    var category = _mapper.Map<Category>(dto);


        ////    await _categoryRepository.CreateAsync(category);
        ////    await _categoryRepository.SaveChangesAsync();

        ////    return true;
        ////}

        ////public async Task DeleteAsync(int id)
        ////{
        ////    var category = await _categoryRepository.GetAsync(id, x => x.Include(x => x.Products));

        ////    if (category is null)
        ////        throw new NotFoundException();



        ////    //if (category.Products.Count > 0)
        ////    //    throw new InvalidInputException();


        ////    _categoryRepository.Delete(category);
        ////    await _categoryRepository.SaveChangesAsync();
        ////}


        ////public async Task<CategoryCreateDto> GetCreateDtoAsync()
        ////{
        ////    var categories = await _categoryRepository.GetAll(x => x.Include(x => x.CategoryDetails)).ToListAsync();

        ////    var dtos = _mapper.Map<List<CategoryGetDto>>(categories);

        ////    var dto = new CategoryCreateDto() { Categories = dtos, CategoryDetails = [new(), new(), new()] };

        ////    return dto;
        ////}


        ////public async Task<CategoryCreateDto> GetCreateDtoAsync(CategoryCreateDto dto)
        ////{
        ////    var categories = await _categoryRepository.GetAll(x => x.Include(x => x.CategoryDetails)).ToListAsync();

        ////    var dtos = _mapper.Map<List<CategoryGetDto>>(categories);

        ////    dto.Categories = dtos;

        ////    return dto;
        ////}




        ////public async Task<CategoryUpdateDto> GetUpdatedDtoAsync(CategoryUpdateDto dto)
        ////{
        ////    var category = await _categoryRepository.GetAsync(dto.Id, x => x.Include(x => x.CategoryDetails));

        ////    if (category is null)
        ////        throw new NotFoundException();


        ////    var categories = await _categoryRepository.GetAll(x => x.Include(x => x.CategoryDetails)).ToListAsync();

        ////    var dtos = _mapper.Map<List<CategoryGetDto>>(categories);

        ////    dto.Categories = dtos;

        ////    return dto;
        ////}

        ////public async Task<CategoryUpdateDto> GetUpdatedDtoAsync(int id)
        ////{
        ////    var category = await _categoryRepository.GetAsync(id, x => x.Include(x => x.CategoryDetails));

        ////    if (category is null)
        ////        throw new NotFoundException();

        ////    var dto = _mapper.Map<CategoryUpdateDto>(category);



        ////    var categories = await _categoryRepository.GetAll(x => x.Include(x => x.CategoryDetails)).ToListAsync();

        ////    var dtos = _mapper.Map<List<CategoryGetDto>>(categories);

        ////    dto.Categories = dtos;

        ////    return dto;
        ////}



        ////public async Task<bool> IsExistAsync(int id)
        ////{
        ////    var isExist = await _categoryRepository.IsExistAsync(x => x.Id == id);
        ////    return isExist;
        ////}

        ////public async Task<bool> UpdateAsync(CategoryUpdateDto dto, ModelStateDictionary ModelState)
        ////{
        ////    if (!ModelState.IsValid)
        ////        return false;

        ////    var existCategory = await _categoryRepository.GetAsync(x => x.Id == dto.Id, x => x.Include(x => x.CategoryDetails).Include(x => x.Products));

        ////    if (existCategory is null)
        ////        throw new NotFoundException();



        ////    //foreach (var detail in dto.CategoryDetails)
        ////    //{
        ////    //    var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

        ////    //    if (!isExistLanguage)
        ////    //    {
        ////    //        ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
        ////    //        return false;
        ////    //    }

        ////    //    isExistLanguage = dto.CategoryDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

        ////    //    if (isExistLanguage)
        ////    //    {
        ////    //        ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
        ////    //        return false;
        ////    //    }
        ////    //}


        ////    existCategory = _mapper.Map(dto, existCategory);

        ////    _categoryRepository.Update(existCategory);
        ////    await _categoryRepository.SaveChangesAsync();

        ////    return true;
        ////}

        ////// Asagdaki iki method IGetServiceWithLanguage bu methodan gelir

        ////public async Task<List<CategoryGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
        ////{
        ////    LanguageHelper.CheckLanguageId(ref language);
        ////    var categories = await _categoryRepository.GetAll(x => x.Include(x => x.CategoryDetails.
        ////    Where(x => x.LanguageId == (int)language)).ThenInclude(x => x.Language).
        ////    Include(x => x.Products)).ToListAsync();

        ////    var dtos = _mapper.Map<List<CategoryGetDto>>(categories);

        ////    return dtos;
        ////}

        ////public async Task<CategoryGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan)
        ////{
        ////    LanguageHelper.CheckLanguageId(ref language);
        ////    var category = await _categoryRepository.GetAsync(id, x => x.Include(x => x.CategoryDetails.Where(x => x.LanguageId == (int)language)));

        ////    if (category is null)
        ////        throw new NotFoundException();

        ////    var dto = _mapper.Map<CategoryGetDto>(category);

        ////    return dto;
        ////}

        ////public async Task<List<CategoryGetDto>> GetCategoriesAsync(Languages language = Languages.Azerbaijan)
        ////{

        ////    var categories = await _categoryRepository.GetAll(_getIncludeFunc(Languages.Azerbaijan)).ToListAsync();

        ////    var dtos = _mapper.Map<List<CategoryGetDto>>(categories);

        ////    return dtos;
        ////}

        ////private Func<IQueryable<Category>, IIncludableQueryable<Category, object>> _getIncludeFunc(Languages language)
        ////{
        ////    LanguageHelper.CheckLanguageId(ref language);
        ////    return x => x.Include(x => x.CategoryDetails.Where(x => x.LanguageId == (int)language)).ThenInclude(x => x.Language);
        ////}
    }
}
