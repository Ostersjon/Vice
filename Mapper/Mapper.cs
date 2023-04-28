using AutoMapper;
using Venna.Dtos;
using Venna.Models;
using System.Text;

namespace Venna.Mapper;

public class Mapper:Profile
{
    public Mapper()
    {
        CreateMap<Regester, User>().ForMember(x => x.Password,options => options
        .MapFrom(r => Encoding.UTF8.GetBytes(r.Password)));

        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, UserDTO>();

        CreateMap<Cart, CartDTO>();
        CreateMap<CartDTO, Cart>();

        CreateMap<CartitemsDTO, Cartitems>();
        CreateMap<Cartitems, CartitemsDTO>();

        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>()
          .ForMember(x => x.ProductPhoto, opt => opt.Ignore())
          .ForMember(x => x.Productinnerphotos, opt => opt.Ignore());
     
        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDTO, Order>();

        CreateMap<Brand, BrandDTO>();
        CreateMap<BrandDTO, Brand>();

        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>()
            .ForMember(x => x.photo, opt => opt.Ignore())
            .ForMember(x => x.Id, opt => opt.MapFrom(e => 0));

        CreateMap<Orderitems, OrderitemsDTO>();
        CreateMap<OrderitemsDTO, Orderitems>();

        CreateMap<MainImage, MainImageDTO>();
        CreateMap<MainImageDTO, MainImage>();

        CreateMap<Product, ProductEndDTO>();
        CreateMap<ProductEndDTO, Product>();
        
        CreateMap<Product, ProductPUT>();
        CreateMap<ProductPUT, Product>();

        CreateMap<SubCategoryDTO, SubCategory>()
          .ForMember(x => x.Photo, opt => opt.Ignore())
          .ForMember(x => x.id, opt => opt.MapFrom(e => 0));

        CreateMap<SubEndDTO, SubCategory>();
        CreateMap<SubCategory, SubEndDTO>();
    }
}

