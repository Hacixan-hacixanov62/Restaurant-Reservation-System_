﻿using AutoMapper;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Dtos.AppUserDtos;
using Restaurant_Reservation_System_.Service.Dtos.BasketDtos;
using Restaurant_Reservation_System_.Service.Dtos.BlogCommentDtos;
using Restaurant_Reservation_System_.Service.Dtos.BlogDtos;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDetailDtos;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.ChefDtos;
using Restaurant_Reservation_System_.Service.Dtos.CommentDtos;
using Restaurant_Reservation_System_.Service.Dtos.OrderDtos;
using Restaurant_Reservation_System_.Service.Dtos.OrderItemDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDetailDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Dtos.ReservationDtos;
using Restaurant_Reservation_System_.Service.Dtos.SliderDtos;
using Restaurant_Reservation_System_.Service.Dtos.SubscribeDtos;
using Restaurant_Reservation_System_.Service.Dtos.TableDtos;
using Restaurant_Reservation_System_.Service.Dtos.TopicDtos;

namespace Restaurant_Reservation_System_.Service.Profiles
{
    public class MapperProfiles:Profile
    {
        public MapperProfiles()
        {
            //// Category Prosfiles 

            //CreateMap<Category, CategoryCreateDto>().ReverseMap();
            //CreateMap<Category, CategoryGetDto>()
            //                        .ForMember(x => x.Name, x => x.MapFrom(x => x.CategoryDetails.FirstOrDefault() != null ? x.CategoryDetails.FirstOrDefault()!.Name : string.Empty));

            //CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            //// CategoryDetails Profiles

            //CreateMap<CategoryDetail, CategoryDetailCreateDto>().ReverseMap();
            //CreateMap<CategoryDetail, CategoryDetailUpdateDto>().ReverseMap();

            ////Product Profiles 
            //CreateMap<Product, ProductCreateDto>().ReverseMap();
            //CreateMap<Product, ProductUpdateDto>()
            //    .ForMember(x => x.MainImagePath, x => x.MapFrom(x => x.ProductImages.FirstOrDefault(x => x.Status) != null ? x.ProductImages.FirstOrDefault(x => x.Status)!.Path : string.Empty))
            //    .ForMember(x => x.ImagePaths, x => x.MapFrom(x => x.ProductImages.Where(x => !x.Status).Select(x => x.Path)))
            //    .ForMember(x => x.ImageIds, x => x.MapFrom(x => x.ProductImages.Where(x => !x.Status).Select(x => x.Id)))
            //    .ReverseMap();

            //CreateMap<Product, ProductGetDto>()
            //               .ForMember(x => x.Name, x => x.MapFrom(src => src.ProductDetails.FirstOrDefault() != null ? src.ProductDetails.FirstOrDefault()!.Name : string.Empty))
            //               .ForMember(x => x.Description, x => x.MapFrom(src => src.ProductDetails.FirstOrDefault() != null ? src.ProductDetails.FirstOrDefault()!.Description : string.Empty))
            //               .ForMember(x => x.MainImagePath, x => x.MapFrom(src => src.ProductImages.FirstOrDefault(img => img.Status) != null ? src.ProductImages.FirstOrDefault(img => img.Status)!.Path : string.Empty))
            //               .ForMember(x => x.ImagePaths, x => x.MapFrom(src => src.ProductImages.Where(x => !x.Status).Select(x => x.Path)));


            ////ProductDetail Profiles

            //CreateMap<ProductDetail, ProductDetailCreateDto>().ReverseMap();
            //CreateMap<ProductDetail, ProductDetailUpdateDto>().ReverseMap();



            ///Slider Profiless
            CreateMap<Slider, SliderCreateDto>().ReverseMap();
            CreateMap<Slider, SliderUpdateDto>().ReverseMap();

            //// Category Prosfiles 

            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();


            ////Product Profiles 
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductGetDto>()
    .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src =>
        src.ProductImages.FirstOrDefault(img => img.IsMain).Url))
    .ReverseMap();


            ////Chef Profiles 
            CreateMap<Chef, ChefCreateDto>().ReverseMap();
            CreateMap<Chef, ChefUpdateDto>().ReverseMap();

            /// Topic Profiles
            CreateMap<Topic, TopicCreateDto>().ReverseMap();
            CreateMap<Topic, TopicUpdateDto>().ReverseMap();

            /// Blog Profiles
            CreateMap<Blog, BlogCreateDto>().ReverseMap();
            CreateMap<Blog, BlogUpdateDto>().ReverseMap();

            ///Table Profiless
            CreateMap<Table, TableCreateDto>().ReverseMap();
            CreateMap<Table, TableGetDto>().ReverseMap();

            ///Reservation Profiless
            CreateMap<Reservation,ReservationCreateDto>().ReverseMap();
            CreateMap<Reservation,ReservationDto>().ReverseMap();

            ///Subscribe Profiles
            CreateMap<Subscribe, SubscribeCreateDto>().ReverseMap();
            CreateMap<Subscribe, SubscribeUpdateDto>().ReverseMap();
            CreateMap<Subscribe, SubscribeGetDto>().ReverseMap();

            ///Comment Profiles
            CreateMap<Comment, CommentCreateDto>().ReverseMap();
            CreateMap<Comment, CommentUpdateDto>().ReverseMap();
            CreateMap<Comment, CommentGetDto>().ReverseMap();
            CreateMap<Comment, CommentReplyDto>().ReverseMap();

            ///Basket Profiles
            CreateMap<CartItem, CartItemCreateDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();

            ///Order Profiles
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto>().ReverseMap();
            CreateMap<Order, OrderGetDto>().ReverseMap();

            CreateMap<AppUser, UserGetDto>().ReverseMap();

            ///OrderItem Profiles
            CreateMap<OrderItem, OrderItemCreateDto>().ReverseMap().ForMember(x => x.Product, x => x.Ignore()).ForMember(x => x.TotalPrice, x => x.MapFrom(x => x.Product.Price));
            CreateMap<OrderItem, OrderItemUpdateDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemGetDto>().ReverseMap();
            CreateMap<OrderItemCreateDto, CartItemDto>().ReverseMap();
            CreateMap<OrderItemGetDto, CartItemDto>().ReverseMap().ForMember(x => x.TotalPrice, x => x.MapFrom(x => x.Product.Price));

            ///BlogCommet Profiles
            CreateMap<BlogComment, BlogCommentCreateDto>().ReverseMap();
            CreateMap<BlogComment, BlogCommentUpdateDto>().ReverseMap();
            CreateMap<BlogComment, BlogCommentGetDto>().ReverseMap();
            CreateMap<BlogComment, BlogCommentReplyDto>().ReverseMap();

        }

    }

}
