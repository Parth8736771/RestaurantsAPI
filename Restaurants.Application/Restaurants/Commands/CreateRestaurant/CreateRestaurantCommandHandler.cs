using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommand> _logger, IMapper _mapper, IRestaurantsRepository restaurantsRepository, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();

            _logger.LogInformation("{UserEmail} [{UserId}] Creating new Restaurant {@Restaurant}", user.Email, user.Id, request);
            var restaurant = _mapper.Map<Restaurant>(request);
            restaurant.OwnerId = user.Id;
            int id = await restaurantsRepository.Create(restaurant);
            return id;
        }
    }
}
