using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile()
        {
            CreateMap<UpdateRestaurantCommand, Restaurant>();
            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(des => des.Address, opt => opt.MapFrom(src => new Address()
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode
                }));
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(destination => destination.City, options => options.MapFrom(source => source.Address.City))
                .ForMember(des => des.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(des => des.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(des => des.Dishes, opt => opt.MapFrom(src => src.Dishes));
        }
    }
}
