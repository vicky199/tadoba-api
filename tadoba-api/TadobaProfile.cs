using AutoMapper;
using tadoba_api.Entity;
using tadoba_api.Models;

namespace tadoba_api
{
    public class TadobaProfile : Profile
    {
        public TadobaProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<AppConfig, AppConfigModel>().ReverseMap();
            CreateMap<DropDownMaster, DropDownModel>().ReverseMap();
            CreateMap<UserAddress, UserAddressModel>().ReverseMap();
            CreateMap<Cart, CartModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => (src.Product.Name)))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => (src.Product.ShortDescription)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => (src.Product.Description)))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => (src.Product.Weight)))
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => (src.Product.ImagePath)))
                .ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Order, OrderModel>()
                 .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => (src.UserAddresses.AddressLine1)))
                 .ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => (src.UserAddresses.AddressLine2)))
                 .ForMember(dest => dest.AddressLine3, opt => opt.MapFrom(src => (src.UserAddresses.AddressLine3)))
                 .ForMember(dest => dest.City, opt => opt.MapFrom(src => (src.UserAddresses.City)))
                 .ForMember(dest => dest.State, opt => opt.MapFrom(src => (src.UserAddresses.State)))
                 .ForMember(dest => dest.Country, opt => opt.MapFrom(src => (src.UserAddresses.Country)))
                 .ForMember(dest => dest.Pincode, opt => opt.MapFrom(src => (src.UserAddresses.Pincode))).ReverseMap();
            CreateMap<OrderDetails, OrderDetailsModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => (src.Product.Name)))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => (src.Product.ShortDescription)))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => (src.Product.Weight)))
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => (src.Product.ImagePath))).ReverseMap();
        }
    }
}
