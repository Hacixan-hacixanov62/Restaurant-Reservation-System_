using AutoMapper;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;

namespace Restaurant_Reservation_System_.Service.Profiles
{
    public class MapperProfiles:Profile
    {
        public MapperProfiles()
        {
            // Category Prosfiles 

            //CreateMap<Category, CategoryCreateDto>().ReverseMap();
            //CreateMap<Category, CategoryGetDto>()
            //                        .ForMember(x => x.Name, x => x.MapFrom(x => x.CategoryDetails.FirstOrDefault() != null ? x.CategoryDetails.FirstOrDefault()!.Name : string.Empty));

            //CreateMap<Category, CategoryUpdateDto>().ReverseMap();
        }




    }
}
