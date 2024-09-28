using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetAllFromRestaurant
{
    public class GetAllFromRestaurantQueryHandler(ILogger<GetAllFromRestaurantQueryHandler> _logger, IRestaurantsRepository restaurantRepository, IMapper _mapper) : IRequestHandler<GetAllFromRestaurantQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetAllFromRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving All Dishes From Restaurant with id: {restaurantId}", request.RestaurantId);

            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dishes = _mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return dishes;
        }
    }
}
