using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(IRestaurantsRepository restaurantRepository, ILogger<DeleteRestaurantCommandHandler> _logger) : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" Deleting Restaurant with id : {RestaurantId} and {@Restaurant}", request.Id, request);
            var restaurant = await restaurantRepository.GetByIdAsync(request.Id);
            if (restaurant is null)
            {
                return false;
            }
            await restaurantRepository.Delete(restaurant);
            return true;
        }
    }
}
