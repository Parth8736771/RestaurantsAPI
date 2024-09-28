using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDishes
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> _logger, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository, IMapper _mapper) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" Creating new Dishes {@Dishes} in Restaurant with id: {restaurantId}", request, request.RestaurantId);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) 
            {
                throw new NotFoundException(nameof(Restaurants), request.RestaurantId.ToString());
            }

            var dish = _mapper.Map<Dish>(request);
            return await dishesRepository.Create(dish);
        }
    }
}
