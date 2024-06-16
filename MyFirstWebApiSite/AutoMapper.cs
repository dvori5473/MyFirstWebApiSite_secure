using AutoMapper;
using DTOs;
using Entities;

namespace MyFirstWebApiSite
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Product, ProductDto>().ForMember(dest=>dest.CategoryName,opts=>opts.MapFrom(src=>src.Category.CategoryName)).ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email)).ReverseMap();
            CreateMap<UserDto,User>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
