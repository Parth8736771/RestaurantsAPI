using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommandHandler(ILogger<DeleteDishCommandHandler> _logger, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository) : IRequestHandler<DeleteDishCommand>
    {
        public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting All Dishes from Restaurant with id: {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            await dishesRepository.Delete(restaurant.Dishes);
        }
    }
}
