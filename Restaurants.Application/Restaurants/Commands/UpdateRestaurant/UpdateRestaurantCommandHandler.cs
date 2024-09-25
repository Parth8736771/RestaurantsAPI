using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(IRestaurantsRepository restaurantRepository, ILogger<UpdateRestaurantCommandHandler> _logger, IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating restaurant with {id} and {@Request}", request.Id, request);
            var restaurant = await restaurantRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            mapper.Map(request, restaurant);
            //restaurant.Name = request.Name;
            //restaurant.Description = request.Description;
            //restaurant.HasDelivery = request.HasDelivery;

            await restaurantRepository.SaveChanges();
        }
    }
}
