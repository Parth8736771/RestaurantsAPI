using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetByIdFromRestaurant
{
    public class GetByIdFromRestaurantQueryHandler(ILogger<GetByIdFromRestaurantQueryHandler> _logger, IRestaurantsRepository restaurantRepository, IMapper _mapper) : IRequestHandler<GetByIdFromRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetByIdFromRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retriving Dish with id: {DishId} From Restaurant with id: {RestaurantId}", request.DishId, request.RestaurantId);

            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = restaurant.Dishes.FirstOrDefault(dish => dish.Id == request.DishId);
            if (dish == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }
    }
}
