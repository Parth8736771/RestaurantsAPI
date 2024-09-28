using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommand> _logger, IMapper _mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new Restaurant {@Restaurant}", request);
            var restaurant = _mapper.Map<Restaurant>(request);
            int id = await restaurantsRepository.Create(restaurant);
            return id;
        }
    }
}
