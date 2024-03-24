using AutoMapper;
using Minimal.API.NET8.Models;
using Minimal.API.NET8.Models.DTO;

namespace Minimal.API.NET8;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Coupon, CouponCreateDTO>().ReverseMap();
        CreateMap<Coupon, CouponUpdateDTO>().ReverseMap();
        CreateMap<Coupon, CouponDTO>().ReverseMap();
        //CreateMap<LocalUser, UserDTO>().ReverseMap();
        //CreateMap<ApplicationUser, UserDTO>().ReverseMap();
    }
}